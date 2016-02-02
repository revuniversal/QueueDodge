using System;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QueueDodge.Integrations;
using System.Threading.Tasks;
using BattleDotSwag.Services;
using BattleDotSwag.WoW.PVP;

namespace QueueDodge.Api.Controllers
{
    [Route("api/leaderboard")]
    public class LeaderboardController : Controller
    {
        private IMemoryCache cache { get;}

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

            Func<string, Task> socket = default(Func<string,Task>);
            if (region == "us")
            {
                switch (bracket.ToLower())
                {
                    case "2v2":
                        socket = us2v2.Broadcast;
                        break;
                    case "3v3":
                        socket = us3v3.Broadcast;
                        break;
                    case "5v5":
                        socket = us5v5.Broadcast;
                        break;
                    case "rbg":
                        socket = usRbg.Broadcast;
                        break;
                    default:
                        break;
                }
            }
            else if(region =="eu")
            {
                switch (bracket.ToLower())
                {
                    case "2v2":
                        socket = eu2v2.Broadcast;
                        break;
                    case "3v3":
                        socket = eu3v3.Broadcast;
                        break;
                    case "5v5":
                        socket = eu5v5.Broadcast;
                        break;
                    case "rbg":
                        socket = euRbg.Broadcast;
                        break;
                    default:
                        break;
                }
            }

            var service = new LeaderboardIntegrationService(key, battleNet, cache, socket);

            Task.WaitAll(socket("clear"));
            Task.WaitAll(service.GetRecentActivity(bracket, _locale, _region));
        }
    }
}
