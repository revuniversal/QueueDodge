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
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

//using System.Reactive.PlatformServices;
//using System.Reactive.Concurrency;
//using System.Reactive.Disposables;
//using System.Reactive.Joins;
//using System.Reactive.Subjects;
//using System.Reactive;

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

        public async Task GetRecentActivity(string bracket, BattleDotSwag.Locale locale, string key, IMemoryCache cache, BattleDotSwag.Region region)
        {
            var endpoint = new LeaderboardEndpoint(bracket, locale, key);
            var requestor = new BattleNetService();

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

            for (var x = 0; x<ladder.Rows.Count; x++)
            {
                var ladderEntry = new LadderEntry(ladder.Rows[x], addedRequest);
                Compare(ladderEntry, cache);             
            }
        }

        private async Task Compare(LadderEntry entry, IMemoryCache cache)
        {
            var key = entry.Request.RegionID + ":" + entry.RealmID + ":" + entry.Name;

            LadderEntry cachedEntry = null;

            var cached = cache.TryGetValue<LadderEntry>(key, out cachedEntry);

            cache.Set(key, entry);

            if (cachedEntry == null)
            { 
                await DetectChanges(cachedEntry, entry);
            }
        }

        private async Task DetectChanges(LadderEntry previous, LadderEntry detected)
        {
            var change = new LadderChange(previous, detected);

            if (change.Changed())
            {
                data.LadderChanges.Add(change);
                data.SaveChanges();

                await UpdateLadder(change);
            }
        }

        private async Task UpdateLadder(LadderChange change)
        {
            // update ladder and send web socket update
        }

        private int ValidateRealmID(string id)
        {
            int realmID = 999;
            int.TryParse(id, out realmID);
            return realmID;
        }
    }
}
