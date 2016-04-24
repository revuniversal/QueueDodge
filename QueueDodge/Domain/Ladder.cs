using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using QueueDodge.Data;
using BattleDotSwag;
using BattleDotSwag.Services;
using BattleDotSwag.WoW.PVP;
using Microsoft.Data.Entity;
using System.Linq;
using QueueDodge.Domain;

namespace QueueDodge
{
    public static class LadderExtensions
    {
        public static IEnumerable<Row> IsNotADuplicate(this IEnumerable<Row> source, IList<Row> tracking)
        {
            var distinctRows = source
            .Where(p => !tracking
                        .Where(o => o.Name == p.Name && o.RealmID == p.RealmID)
                        .Any());
            
            
            foreach(var row in distinctRows)
            {
                tracking.Add(row);
                yield return row;
            }
           
        }
        public static IEnumerable<LadderEntry> ConvertToLadderEntry(this IEnumerable<Row> source, string bracket, BattleDotSwag.Region region)
        {
            var factory = new LadderEntryFactory();
            
            foreach(var row in source)
            {
                var _region = new Region((int)region, region.ToString());
                var ladderEntry = factory.Create(row, bracket, _region);
                yield return ladderEntry; 
            }
        }     
        public static IEnumerable<LadderEntryPair> CheckCache(this IEnumerable<LadderEntry> source, IMemoryCache cache)
        {
            foreach(var entry in source)
            {
                var realKey = entry.Character.Name + ":" + entry.Character.RealmID + ":" + entry.Bracket;

                var cachedEntry = default(LadderEntry);
                var cached = cache.TryGetValue(realKey, out cachedEntry);
                cache.Set(realKey, entry);

                if (cached)
                {
                    var pair = new LadderEntryPair(entry,cachedEntry);
                    yield return pair;
                }
            }
        }
        public static IEnumerable<LadderChange> Changed(this IEnumerable<LadderEntryPair> source)
        {
            foreach(var pair in source)
            {
                var change = new LadderChange(pair.Cached,pair.Current);
                if(change.Changed())
                {
                    yield return change;
                }
            }
        }
        public static IEnumerable<LadderChange> BroadcastChange(this IEnumerable<LadderChange> source, Func<string,Task> sendMessage)
        {
            var options = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                
            foreach(var change in source)
            {
               var json = JsonConvert.SerializeObject(change, options);
                sendMessage(json);
                yield return change;
            }
        }
    }

    public class Ladder
    {
        private string apiKey;
        private QueueDodgeDB queueDodge;
        private IMemoryCache cache;
        private Func<string, Task> sendMessage;

        // TODO:  This is too sketchy.
        public Ladder(string apiKey, QueueDodgeDB queueDodge, IMemoryCache cache, Func<string, Task> sendMessage)
        {
            this.apiKey = apiKey;
            this.queueDodge = queueDodge;
            this.cache = cache;
            this.sendMessage = sendMessage;
        }
        
        public void DetectChanges(string bracket, Locale locale, BattleDotSwag.Region region)
        {
            var leaderboard = GetActivity(bracket, locale, region);
            
            var processed = new List<Row>();

           var changes = leaderboard.Rows
            .IsNotADuplicate(processed)
            .ConvertToLadderEntry(bracket,region)
            .CheckCache(cache)
            .Changed()
            .BroadcastChange(sendMessage);

            foreach(var change in changes)
            {
                var realm = AddOrUpdateRealm(change);
                var character = AddOrUpdateCharacter(change);
                var changeModel = new LadderChangeModel(change);
                changeModel.CharacterID = character.ID;
                queueDodge.LadderChanges.Add(changeModel, GraphBehavior.SingleObject);

                change.Current.Character = character;
                change.Previous.Character = character;

                queueDodge.SaveChanges();
            }       
        }
        private Leaderboard GetActivity(string bracket, Locale locale, BattleDotSwag.Region region)
        {
            var service = new BattleNetService<Leaderboard>();
            var endpoint = new LeaderboardEndpoint(bracket, locale, apiKey);
            var leaderboard = service.Get(endpoint, region).Result;
            return leaderboard;
        }
        public IEnumerable<LadderEntry> ConvertToLadderEntry(Row row, string bracket, BattleDotSwag.Region region)
        {
            var _region = new Region((int)region, region.ToString());
            var ladderEntry = new LadderEntryFactory().Create(row,bracket,_region);
            yield return ladderEntry;
        }

        public Realm AddOrUpdateRealm(LadderChange change)
        {
            var realmExists = queueDodge
                .Realms
                .FirstOrDefault(p => p.ID == change.Current.Character.RealmID);

            if (realmExists != null)
            {
                var realm = new Realm(change.Current.Character.RealmID,
                    change.Current.Character.Realm.Name,
                    change.Current.Character.Realm.Slug,
                    change.Current.Character.Realm.RegionID);

                var trackedRealm = queueDodge.Realms.Add(realm);
                queueDodge.SaveChanges();
                return trackedRealm.Entity;
            }
            else
            {
                return realmExists;
            }
        }
        public Character AddOrUpdateCharacter(LadderChange change)
        {
            var characterCheck = queueDodge
                .Characters
                .FirstOrDefault(p => p.Name == change.Current.Character.Name
                && p.RealmID == change.Current.Character.RealmID);

            if (characterCheck == null)
            {
                var character = new Character(change.Current.Character.Name,
                    change.Current.Character.Gender,
                    change.Current.Character.RealmID,
                    change.Current.Character.RaceID,
                    change.Current.Character.ClassID,
                    change.Current.Character.SpecializationID);

                var attachedCharacter = queueDodge.Add(character).Entity;
                queueDodge.SaveChanges();
            }

            // HACK:  Totally lame, child objects aren't populated so I have to re-query what I just inserted.
            var filledCharacter = queueDodge
                .Characters
                .Include(p => p.Class)
                .Include(p => p.Realm)
                .Include(p => p.Specialization)
                .Include(p => p.Race)
                .Include(p => p.Realm.Region)
                .Include(p => p.Race.Faction)
                .Where(p => p.Name == change.Current.Character.Name &&
                p.RealmID == change.Current.Character.RealmID)
                .Single();

            return filledCharacter;
        }
    }
}
