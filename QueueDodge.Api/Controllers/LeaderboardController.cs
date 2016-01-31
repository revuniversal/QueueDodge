using System;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QueueDodge.Integrations;
using System.Threading.Tasks;
using BattleDotSwag.Services;
using BattleDotSwag.WoW.PVP;
using QueueDodge.Api.Websockets;

namespace QueueDodge.Api.Controllers
{
    [Route("api/leaderboard")]
    public class LeaderboardController : Controller
    {
        private IMemoryCache cache { get; set; }

        public LeaderboardController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        [HttpGet]
        public void GetActivity(string region, string bracket, string locale)
        {
            var key = "vftjkwdyvev3p4m9jrnfxgsdu2dz68yd";
            var _locale = (BattleDotSwag.Locale)Enum.Parse(typeof(BattleDotSwag.Locale), locale);
            var _region = (BattleDotSwag.Region)Enum.Parse(typeof(BattleDotSwag.Region), region);

            var socketKey = new Tuple<string, string>(region, bracket);
            var battleNet = new BattleNetService<Leaderboard>();

            var socketServer = WebSocketMultiton.GetInstance(socketKey);
            var service = new LeaderboardIntegrationService(key, battleNet, cache, socketServer.Broadcast);

            Task.WaitAll(socketServer.Broadcast("clear"));
            Task.WaitAll(service.GetRecentActivity(bracket, _locale, _region));
        }
    }
}
