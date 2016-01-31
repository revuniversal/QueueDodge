using System;
using BattleDotSwag.Services;
using QueueDodge.Models;
using BattleDotSwag.PVP;

namespace QueueDodge.Integrations
{
    public class ConquestIntegrationService
    {
        private BattleNetService<ConquestCap> conquestCapService;
        private QueueDodgeDB data;

        public ConquestIntegrationService()
        {
            conquestCapService = new BattleNetService<ConquestCap>();
            data = new QueueDodgeDB();
        }

        public ConquestCap GetConquestCap(BattleDotSwag.Region region, string host, BattleDotSwag.Game game, BattleDotSwag.Locale locale, int arenaRating, int bgRating)
        {
            var endpoint = new ConquestEndpoint(locale, arenaRating, bgRating);
            ConquestCap cap = new ConquestCap();

            // TODO:  Fix Conquest Cap call.
            //requests.Perform(10, 2, 5, request, () =>
            //{
            //     cap = conquestCapService.Get(endpoint, host, region, game).Result;
            //});

            return cap;
        }
    }
}
