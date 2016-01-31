using System;
using QueueDodge.Models;
using BattleDotSwag;
using BattleDotSwag.Services;
using BattleDotSwag.WoW.PVP;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using QueueDodge.Data.Cache;
using QueueDodge.Data;

namespace QueueDodge.Integrations
{
    public class LeaderboardIntegrationService
    {
        private string apiKey;
        private BattleNetService<Leaderboard> requestor; // TODO:  This name sucks.
        private IMemoryCache cache;
        private Func<string, Task> sendMessage;

        public LeaderboardIntegrationService(
            string apiKey,
            BattleNetService<Leaderboard> requestor,
            IMemoryCache cache,
            Func<string, Task> sendMessage) // TODO:  This is too sketchy.
        {
            this.apiKey = apiKey;
            this.requestor = requestor;
            this.cache = cache;
            this.sendMessage = sendMessage;
        }

        public async Task GetRecentActivity(string bracket, Locale locale, Region region)
        {
            var endpoint = new LeaderboardEndpoint(bracket, locale, apiKey);
            Leaderboard leaderboard = requestor.Get(endpoint, region).Result;
            var ladder = new List<LadderEntry>();

            foreach (var entry in leaderboard.Rows)
            {
                var ladderEntry = LadderEntry.Create(entry, bracket, region);
                ladder.Add(ladderEntry);
                CompareWithCache(ladderEntry, region, bracket);
            };

            var key = new LadderKey(region.ToString(),bracket);
            cache.Set(key, leaderboard);
        }

        private async Task CompareWithCache(LadderEntry entry, Region region, string bracket)
        {
            var cachedEntry = default(LadderEntry);
            var key = new LadderEntryKey((int)region,entry.Character.Realm.ID,entry.Character.Name,bracket);

            var cached = cache.TryGetValue(key, out cachedEntry);
            cache.Set(key, entry);

            if (cached)
            {
                var change = new LadderChange(cachedEntry, entry);
                if (change.Changed()) BroadcastChange(change);
            }
        }

        private async Task BroadcastChange(LadderChange change)
        {
            var serializerOptions = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(change, serializerOptions);
            await sendMessage(json);
        }
    }
}
