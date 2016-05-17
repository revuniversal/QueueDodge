
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BattleDotSwag.WoW.PVP;
using Microsoft.EntityFrameworkCore;
using QueueDodge.Data;

namespace QueueDodge
{
    public static class LadderExtensions
    {
        public static IEnumerable<Row> Dedupe(this IEnumerable<Row> source, IList<Row> tracking)
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
        public static IEnumerable<LadderEntry> CacheLadder(this IEnumerable<LadderEntry> source, IMemoryCache cache, string bracket, BattleDotSwag.Region region)
        {
            var key = $"{bracket}:{region}";
            cache.Set(key,source);
            return source;
        }
        
        public static IEnumerable<LadderEntryPair> ExistsInCache(this IEnumerable<LadderEntry> source, IMemoryCache cache)
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
        public static IEnumerable<LadderChange> CharacterChanged(this IEnumerable<LadderEntryPair> source)
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
        public static IEnumerable<LadderChange> NotifyChanged(this IEnumerable<LadderChange> source, Func<string,Task> sendMessage)
        {
            var options = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                
            foreach(var change in source)
            {
            var json = JsonConvert.SerializeObject(change, options);
                sendMessage(json);
                yield return change;
            }
        }
        public static IEnumerable<LadderChange> SaveRealm(this IEnumerable<LadderChange> source, QueueDodgeDB queueDodge)        {
            foreach(var change in source)
            {
                var realmExists = queueDodge
                    .Realms
                    .Any(p => p.ID == change.Current.Character.RealmID);

                if (!realmExists)
                {
                    var realm = new Realm(change.Current.Character.RealmID,
                        change.Current.Character.Realm.Name,
                        change.Current.Character.Realm.Slug,
                        change.Current.Character.Realm.RegionID);

                    var trackedRealm = queueDodge.Realms.Add(realm);
                    queueDodge.SaveChanges();
                }

                yield return change;
            }
        }
        public static IEnumerable<LadderChange> SaveCharacter(this IEnumerable<LadderChange> source, QueueDodgeDB queueDodge)
        {
            foreach(var change in source)
            {
                var characterExists = queueDodge
                    .Characters
                    .Any(p => p.Name == change.Current.Character.Name
                    && p.RealmID == change.Current.Character.RealmID);

                if (!characterExists)
                {
                    var attachedCharacter = queueDodge.Add(change.Current.Character).Entity;
                    queueDodge.SaveChanges();
                }

                var character = queueDodge
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

                change.Current.Character = character;
                change.Previous.Character = character;

                yield return change;
            }
        }
        public static IEnumerable<LadderChange> SaveLadderChange(this IEnumerable<LadderChange> source, QueueDodgeDB queueDodge)
        {
            foreach(var change in source)
            {
                var changeModel = new LadderChangeModel(change);
                changeModel.CharacterID = change.Current.Character.ID; 
                queueDodge.LadderChanges.Add(changeModel);
                  
                yield return change;
            }
        }
    }
}