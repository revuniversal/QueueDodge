using System;
using System.Collections.Generic;
using QueueDodge.Models;
using QueueDodge.Services;
using Microsoft.AspNet.Mvc;

namespace QueueDodge.Api.Controllers
{
 //   [RoutePrefix("api/region/{region}/leaderboard")]
 [Route("api/leaderboard")]
    public class LeaderboardController : Controller
    {
        private LeaderboardService leaderboards { get; set; }
        public LeaderboardController() {
            leaderboards = new LeaderboardService();
        }

        [HttpGet]
        [Route("{bracket}")]
        public IEnumerable<LadderChange> GetRecentActivity(string bracket, string region)
        {
            BattleDotSwag.Region regionCode = (BattleDotSwag.Region)Enum.Parse(typeof(BattleDotSwag.Region), region);
            return leaderboards.GetRecentActivity(bracket, regionCode);
        }

        [HttpGet]
        public LeaderboardViewModel GetLeaderboard(LeaderboardFilter filter)
        {
            var leaderboard = leaderboards.GetLeaderboard(filter);
            return leaderboard;
        }
    }
}
