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
    [Route("api/leaderboard")]
    public class LeaderboardController : Controller
    {
        private IMemoryCache cache { get; set; }
       // private LeaderboardService leaderboard { get; set; }

        public LeaderboardController(IMemoryCache cache)
        {
           // this.leaderboard = new LeaderboardService(cache);
            this.cache = cache;
        }

        //[HttpGet]
        //public LeaderboardViewModel GetLeaderboard(LeaderboardFilter filter)
        //{
        //    //var vm = leaderboard.GetLeaderboard(filter);
        //   // return vm;
        //}

        [HttpGet]
        [Route("activity")]
        public void GetRecentActivity(string bracket, string region, string locale)
        {
            var key = "vftjkwdyvev3p4m9jrnfxgsdu2dz68yd";
            var _locale = (BattleDotSwag.Locale)Enum.Parse(typeof(BattleDotSwag.Locale), locale);
            var _region = (BattleDotSwag.Region)Enum.Parse(typeof(BattleDotSwag.Region), region);

            var service = new LeaderboardIntegrationService(key);

            if (region == "us")
            {
                if (bracket == "2v2")
                {
                    Task.WaitAll(service.GetRecentActivity(bracket, _locale, _region, key, cache, US2v2.Broadcast));
                }
                else if (bracket == "3v3")
                {
                    Task.WaitAll(service.GetRecentActivity(bracket, _locale, _region, key, cache, US3v3.Broadcast));
                }
            }

            
        }

    }
}
