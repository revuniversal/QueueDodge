using System;
using System.Collections.Generic;
using QueueDodge.Models;
using QueueDodge.Services;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QueueDodge.Integrations;
using System.Threading.Tasks;

namespace QueueDodge.Api.Controllers
{
    //   [RoutePrefix("api/region/{region}/leaderboard")]
    [Route("api/leaderboard")]
    public class LeaderboardController : Controller
    {
        private IMemoryCache cache { get; set; }
        private LeaderboardService leaderboards { get; set; }

        public LeaderboardController(IMemoryCache cache)
        {
            this.leaderboards = new LeaderboardService();
            this.cache = cache;
        }

        [HttpGet]
        public async Task GetRecentActivity(string bracket, string region, string locale)
        {
            // TODO:  Perform parameter checking here.
            var _locale = (BattleDotSwag.Locale)Enum.Parse(typeof(BattleDotSwag.Locale), locale);
            var _region = (BattleDotSwag.Region)Enum.Parse(typeof(BattleDotSwag.Region), locale);
            var key = "YOUR KEY HERE";
            var service = new LeaderboardIntegrationService(key);
            await service.GetRecentActivity(bracket, _locale, key, cache, _region);
        }
    }
}
