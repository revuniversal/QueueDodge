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

namespace QueueDodge
{
    /// <summary>
    /// SIDE EFFECTS GALORE!
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

            foreach (var entry in data.Rows)
            {
                var ladderEntry = LadderEntry.Create(entry, bracket, region);
                Data.Add(ladderEntry);
                CompareWithCache(ladderEntry, bracket);
            };

            var key = new LadderKey(region.ToString(), bracket);
            cache.Set(key, Data);
            queueDodge.SaveChanges();
        }

        private void CompareWithCache(LadderEntry entry, string bracket)
        {
            var realKey = entry.Character.Name + ":" + entry.Character.Realm.ID + ":" + entry.Character.Realm.Region.ToString() + ":" + bracket;

            var cachedEntry = default(LadderEntry);
            var cached = cache.TryGetValue(realKey, out cachedEntry);

            if (cached)
            {
                var change = new LadderChange(cachedEntry, entry);
                if (change.Changed())
                {
                    var changeModel = new LadderChangeModel(change);

                    var realm = queueDodge.Realms.Where(p => p.Name == entry.Character.Realm.Name && p.RegionID == entry.Character.Realm.RegionID).FirstOrDefault();

                    if (realm == null)
                    {
                        queueDodge.Realms.Add(entry.Character.Realm);
                        queueDodge.SaveChanges();
                    }
 

                    var character = queueDodge.Characters.Where(p => p.Name == entry.Character.Name
                    && p.RealmID == entry.Character.RealmID
                    && p.Realm.RegionID == entry.Character.Realm.RegionID).FirstOrDefault();

                    if (character == null)
                    {
                        changeModel.Character = queueDodge.Characters.Add(entry.Character).Entity;
                        queueDodge.SaveChanges();
                    }
                    else {
                        changeModel.Character = queueDodge.Characters.Update(entry.Character).Entity;
                        queueDodge.SaveChanges();
                    }



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
    }
}
