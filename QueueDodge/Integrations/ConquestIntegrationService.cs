using System;
using BattleDotSwag.Services;
using QueueDodge.Models;
using BattleDotSwag.PVP;
using QueueDodge.Services;

namespace QueueDodge.Integrations
{
    public class ConquestIntegrationService
    {
        private BattleNetService<ConquestCap> conquestCapService;
        private QueueDodgeDB data;
        private RequestService requests;

        public ConquestIntegrationService()
        {
            conquestCapService = new BattleNetService<ConquestCap>();
            data = new QueueDodgeDB();
            requests = new RequestService(data);
        }

        public ConquestCap GetConquestCap(BattleDotSwag.Region region, string host, BattleDotSwag.Game game, BattleDotSwag.Locale locale, int arenaRating, int bgRating)
        {
            var endpoint = new ConquestEndpoint(locale, arenaRating, bgRating);

            BattleNetRequest newRequest = new BattleNetRequest()
            {
                Locale = locale.ToString(),
                RegionID = (int)region,
                RequestDate = DateTime.Now,
                RequestType = "leaderboards",
                Bracket = "",
                Url = conquestCapService.GetUri(endpoint, host, region, game).ToString()
            };

           // BattleNetRequest request =
                data.BattleNetRequests.Add(newRequest);
            data.SaveChanges();
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
