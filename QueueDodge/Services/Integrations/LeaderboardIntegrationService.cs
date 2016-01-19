using System;
using System.Linq;
using System.Data.SqlClient;
using QueueDodge.Models;
using BattleDotSwag;
using BattleDotSwag.Services;
using BattleDotSwag.PVP;
using QueueDodge.Services;
using Microsoft.Data.Entity;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace QueueDodge.Integrations
{
    public class LeaderboardIntegrationService
    {
        private string apiKey;
        private BattleNetService leaderboard;
        private RequestService requests;
        private QueueDodgeDB data;

        public LeaderboardIntegrationService(QueueDodgeDB data, string apiKey)
        {
            this.apiKey = apiKey;
            this.data = data;
            leaderboard = new BattleNetService();
            requests = new RequestService(data);
        }
        public LeaderboardIntegrationService(string apiKey)
        {
            this.apiKey = apiKey;
            leaderboard = new BattleNetService();
            requests = new RequestService(data);
            data = new QueueDodgeDB();
        }

        public async Task GetRecentActivity(
            string bracket,
            BattleDotSwag.Locale locale,
            BattleDotSwag.Region region,
            string key,
            IMemoryCache cache,
            Func<string, Task> Send)
        {
            var endpoint = new LeaderboardEndpoint(bracket, locale, key);
            var requestor = new BattleNetService();
            var leaderboard = new List<LadderEntry>();
            var request = new QueueDodge.Models.BattleNetRequest()
            {
                Bracket = bracket,
                Locale = locale.ToString(),
                RegionID = (int)region,
                RequestDate = DateTime.Now,
                RequestType = "leaderboard",
                Url = requestor.GetUri(endpoint, region).ToString(),
                Duration = 0
            };

            var addedRequest = data.BattleNetRequests.Add(request).Entity;
            data.SaveChanges();

            var json = requestor.Get(endpoint, region).Result;

            var ladder = JsonConvert.DeserializeObject<Leaderboard>(json);



            foreach(var entry in ladder.Rows)
            {
                var ladderEntry = new LadderEntry(entry, addedRequest);
                leaderboard.Add(ladderEntry);
                CompareWithCache(ladderEntry, cache, Send);
            };

            cache.Set(region + ":" + bracket, leaderboard);
        }

        private async Task CompareWithCache(LadderEntry entry, IMemoryCache cache, Func<string, Task> Send)
        {
            var key = entry.Request.RegionID + ":" + entry.RealmID + ":" + entry.Name;

            LadderEntry cachedEntry = default(LadderEntry);

            var cached = cache.TryGetValue(key, out cachedEntry);

            cache.Set(key, entry);

            if (cached)
            {
                var change = new LadderChange(cachedEntry, entry);

                if (change.Changed())
                {
                    SaveChange(change);
                    BroadcastChange(change, Send);              
                }
            }
        }

        private async Task BroadcastChange(LadderChange change, Func<string, Task> Send)
        {
            var json = JsonConvert.SerializeObject(change);
            await Send(json);
        }

        private async Task SaveChange(LadderChange change)
        {
            data.LadderChanges.Add(change);
            await data.SaveChangesAsync();
        }

        private int ValidateRealmID(string id)
        {
            int realmID = 999;
            int.TryParse(id, out realmID);
            return realmID;
        }
    }
}
