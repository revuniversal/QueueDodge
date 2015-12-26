using Microsoft.AspNet.Mvc;
using QueueDodge.Integrations;
using QueueDodge.Models;

namespace QueueDodge.Api.Controllers
{
    [Route("api/conquest")]
    public class ConquestController : Controller
    {
        private ConquestIntegrationService conquestService;

        public ConquestController()
        {
            conquestService = new ConquestIntegrationService();
        }

        [HttpGet]
        [Route("conquest")]
        public ConquestCap Get(int arenaRating, int bgRating)
        {
            return conquestService.GetConquestCap(BattleDotSwag.Region.us, ".battle.net/", BattleDotSwag.Game.wow, BattleDotSwag.Locale.en_us, arenaRating, bgRating);
        }
    }
}
