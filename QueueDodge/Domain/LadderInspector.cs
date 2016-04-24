using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

using QueueDodge.Data;
using BattleDotSwag;
using BattleDotSwag.Services;
using BattleDotSwag.WoW.PVP;
using Microsoft.Data.Entity;
using System.Linq;
using QueueDodge.Domain;

namespace QueueDodge
{
    public class LadderInspector
    {
        private string apiKey;
        private QueueDodgeDB queueDodge;
        private IMemoryCache cache;
        private Func<string, Task> sendMessage;

        public LadderInspector(string apiKey, QueueDodgeDB queueDodge, IMemoryCache cache, Func<string, Task> sendMessage)
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
          
        public Leaderboard GetActivity(string bracket, Locale locale, BattleDotSwag.Region region)
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
