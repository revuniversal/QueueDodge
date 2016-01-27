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
using Newtonsoft.Json.Serialization;

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
            string apiKey,
            IMemoryCache cache,
            Func<string, Task> Send)
        {
            var endpoint = new LeaderboardEndpoint(bracket, locale, apiKey);
            var requestor = new BattleNetService();
            var leaderboard = new List<LadderEntry>();

            var json = requestor.Get(endpoint, region).Result;

            var ladder = JsonConvert.DeserializeObject<Leaderboard>(json);

            foreach (var entry in ladder.Rows)
            {
                var ladderEntry = new LadderEntry(entry, region, bracket);
                leaderboard.Add(ladderEntry);
                CompareWithCache(ladderEntry, cache, Send, region,bracket);
            };
            // HACK:  Make this a type.
            var key = region + ":" + bracket;
            cache.Set(key, leaderboard);
        }

        private async Task CompareWithCache(LadderEntry entry, IMemoryCache cache, Func<string, Task> Send, BattleDotSwag.Region region, string bracket)
        {
            var cachedEntry = default(LadderEntry);
            // HACK:  Make this a type.
            var key = (int)region + ":" + entry.RealmID + ":" + entry.Name + ":" + bracket;

            var cached = cache.TryGetValue(key, out cachedEntry);
            cache.Set(key, entry);

            if (cached)
            {
                var change = new LadderChange(cachedEntry, entry);
                if (change.Changed()) BroadcastChange(change, Send);
            }
        }

        private async Task BroadcastChange(LadderChange change, Func<string, Task> Send)
        {
            var serializerOptions = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(change, serializerOptions);
            await Send(json);
        }
    }
}
