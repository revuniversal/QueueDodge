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

namespace QueueDodge
{
    public class Ladder
    {
        private string apiKey;
        private BattleNetService<Leaderboard> leaderboardService;
        private IMemoryCache cache;
        private Func<string, Task> sendMessage;

        public IList<LadderEntry> Data { get; set; }

        public Ladder(
            string apiKey,
            BattleNetService<Leaderboard> requestor,
            IMemoryCache cache,
            Func<string, Task> sendMessage) // TODO:  This is too sketchy.
        {
            this.apiKey = apiKey;
            this.leaderboardService = requestor;
            this.cache = cache;
            this.sendMessage = sendMessage;
            this.Data = new List<LadderEntry>();
        }

        public async Task GetRecentActivity(string bracket, Locale locale, Region region)
        {
            var endpoint = new LeaderboardEndpoint(bracket, locale, apiKey);
            var data = leaderboardService.Get(endpoint, (BattleDotSwag.Region)region.ID).Result;

            foreach (var entry in data.Rows)
            {
                var ladderEntry = LadderEntry.Create(entry, bracket, region);
                Data.Add(ladderEntry);
                CompareWithCache(ladderEntry, bracket);
            };

            var key = new LadderKey(region.ToString(),bracket);
            cache.Set(key, Data);
        }

        private async Task CompareWithCache(LadderEntry entry, string bracket)
        {
            var realKey = entry.Character.Name + ":" + entry.Character.Realm.ID + ":" + entry.Character.Realm.Region.ToString() + ":" + bracket;

            var cachedEntry = default(LadderEntry);
            var cached = cache.TryGetValue(realKey, out cachedEntry);
            
            if (cached)
            {
                var change = new LadderChange(cachedEntry, entry);
                if (change.Changed()) BroadcastChange(change);
            }

            cache.Set(realKey, entry);
        }

        private async Task BroadcastChange(LadderChange change)
        {
            var serializerOptions = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(change, serializerOptions);
            await sendMessage(json);
        }
    }
}
