using System;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System.Linq;

using BattleDotSwag.Services;
using BattleDotSwag.WoW.PVP;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.OptionsModel;

namespace QueueDodge.Api.Controllers
{
    [Route("api/region/{region}/bracket/{bracket}")]
    public class LadderController : Controller
    {
        private IMemoryCache cache;
        private QueueDodgeDB queueDodge;
        private QueueDodgeApiOptions options;

        public LadderController(IMemoryCache cache, QueueDodgeDB queueDodge, IOptions<QueueDodgeApiOptions> options)
        {
            this.cache = cache;
            this.queueDodge = queueDodge;
            this.options = options.Value;
        }

        // TODO:  Think about adding a realm filter here so users can see where they stand on their realm.  Not that it matters.
        [HttpGet]
        public void GetActivity(string region, string bracket, string locale)
        {
            var key = options.apiKey;
            var _locale = (BattleDotSwag.Locale)Enum.Parse(typeof(BattleDotSwag.Locale), locale);
            var _regionEnum = (BattleDotSwag.Region)Enum.Parse(typeof(BattleDotSwag.Region), region);
            var _region = queueDodge.Regions.Where(r => r.ID == ((int)_regionEnum)).Single();
            var battleNet = new BattleNetService<Leaderboard>();

            var socket = GetSocket(_regionEnum, bracket);

            var ladder = new Ladder(key, battleNet, queueDodge, cache, socket);

            // TODO:  Replace this with an standardized message before it's too late.
            Task.WaitAll(socket("clear"));
            ladder.GetActivity(bracket, _locale, _region);
        }

        [HttpGet]
        [Route("recent")]
        public IEnumerable<LadderChange> GetRecentActivity(string region, string bracket)
        {
            // TODO:  magic strings are getting out of hand, in here and in the ladder file.
            var key = "changes:" + region + ":" + bracket;
            var activity = new List<LadderChange>();
            cache.TryGetValue(key, out activity);

            return activity;
        }
        private Func<string, Task> GetSocket(BattleDotSwag.Region region, string bracket)
        {
            // TODO:  This and everything below is a pile of crap.  How do I web socket?
            if (region == BattleDotSwag.Region.us)
            {
                switch (bracket.ToLower())
                {
                    case "2v2":
                        return us2v2.Broadcast;
                    case "3v3":
                        return us3v3.Broadcast;
                    case "5v5":
                        return us5v5.Broadcast;
                    case "rbg":
                        return usRbg.Broadcast;
                    default:
                        return null;
                }
            }
            else if (region == BattleDotSwag.Region.eu)
            {
                switch (bracket.ToLower())
                {
                    case "2v2":
                        return eu2v2.Broadcast;
                    case "3v3":
                        return eu3v3.Broadcast;
                    case "5v5":
                        return eu5v5.Broadcast;
                    case "rbg":
                        return euRbg.Broadcast;
                    default:
                        return null;
                }
            }
            else {
                return null;
            }
        }
    }
}
