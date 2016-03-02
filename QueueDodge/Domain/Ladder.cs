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
    /// <summary>
    /// TODO:  HACK:  SIDE EFFECTS GALORE!  Refactor this whole thing until it isn't so messy.
    /// </summary>
    public class Ladder
    {
        private string apiKey;
        private BattleNetService<Leaderboard> leaderboardService;
        private QueueDodgeDB queueDodge;
        private IMemoryCache cache;
        private Func<string, Task> sendMessage;
        private JsonSerializerSettings options;

        public IList<LadderEntry> Data { get; set; }

        public Ladder(
            string apiKey,
            BattleNetService<Leaderboard> requestor,
            QueueDodgeDB queueDodge,
            IMemoryCache cache,
            Func<string, Task> sendMessage) // TODO:  This is too sketchy.
        {
            this.apiKey = apiKey;
            this.leaderboardService = requestor;
            this.queueDodge = queueDodge;
            this.cache = cache;
            this.sendMessage = sendMessage;
            this.Data = new List<LadderEntry>();
            this.options = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        }

        public void GetActivity(string bracket, Locale locale, Region region)
        {
            var endpoint = new LeaderboardEndpoint(bracket, locale, apiKey);
            var data = leaderboardService.Get(endpoint, (BattleDotSwag.Region)region.ID).Result;
            var factory = new LadderEntryFactory(queueDodge);
            var realms = queueDodge.Realms.AsEnumerable();

            foreach (var entry in data.Rows)
            {
                var ladderEntry = factory.Create(entry, bracket, region);
                Data.Add(ladderEntry);
                CompareWithCache(ladderEntry, bracket, region);
            };

            var key = region.ToString() + ":" + bracket;
            cache.Set(key, Data);
            queueDodge.SaveChanges();
        }

        private void CompareWithCache(LadderEntry entry, string bracket, Region region)
        {
            var realKey = entry.Character.Name + ":" + entry.Character.RealmID + ":" + bracket;

            var cachedEntry = default(LadderEntry);
            var cached = cache.TryGetValue(realKey, out cachedEntry);

            if (cached)
            {
                var change = new LadderChange(cachedEntry, entry);
                if (change.Changed())
                {
                    var realm = GetRealm(entry, region);
                    var character = GetCharacter(entry, region);
                    var changeModel = new LadderChangeModel(change);
                    changeModel.CharacterID = character.ID;

                    queueDodge.LadderChanges.Add(changeModel, GraphBehavior.SingleObject);
                    BroadcastChange(change);
                }
            }

            cache.Set(realKey, entry);
        }

        private void BroadcastChange(LadderChange change)
        {
            var json = JsonConvert.SerializeObject(change, options);
            sendMessage(json);
        }
        private Realm GetRealm(LadderEntry entry, Region region)
        {
            var realmCheck = queueDodge
                .Realms
                .Where(p => p.ID == entry.Character.RealmID)
                .FirstOrDefault();

            if (realmCheck == null)
            {
                var realm = new Realm(entry.Character.RealmID, entry.Character.Realm.Name, entry.Character.Realm.Slug, region.ID);
                var trackedRealm = queueDodge.Realms.Add(realm);
                queueDodge.SaveChanges();
                return trackedRealm.Entity;
            }
            else
            {
                return realmCheck;
            }
        }
        private Character GetCharacter(LadderEntry entry, Region region)
        {
            var characterCheck = queueDodge
                .Characters
                .Where(p => p.Name == entry.Character.Name
                && p.RealmID == entry.Character.RealmID
                && p.Realm.RegionID == region.ID)
                .FirstOrDefault();

            if (characterCheck == null)
            {
                var character = new Character(entry.Character.Name, entry.Character.Gender, entry.Character.RealmID, entry.Character.RaceID, entry.Character.ClassID, entry.Character.SpecializationID);
                var attachedCharacter = queueDodge.Add(character).Entity;
                queueDodge.SaveChanges();
                return attachedCharacter;
            }
            else
            {
                return characterCheck;
            }


        }
    }
}
