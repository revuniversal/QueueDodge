using BattleDotSwag;
using Microsoft.AspNet.Mvc;
using QueueDodge.Integrations;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace QueueDodge.Api.Controllers
{
    //[RoutePrefix("api/battlenet")]
    [Route("api/battlenet")]
    public class BattleNetController : Controller
    {
        private LeaderboardIntegrationService battleNetLeaderboardService { get; set; }

        public BattleNetController()
        {
            battleNetLeaderboardService = new LeaderboardIntegrationService(new QueueDodgeDB(), "heuemgj94eyv484cekut2a82d6crnskm");
        }

        [HttpGet]
        [Route("leaderboard/{regionCode}/{bracket}")]
        public async void Get(string regionCode, string bracket)
        {
            Region region = (Region)Enum.Parse(typeof(Region), regionCode, true);
            Locale locale;

            switch (region.ToString())
            {
                case "us":
                    locale = Locale.en_us;
                    break;
                case "eu":
                    locale = Locale.en_gb;
                    break;

                default:
                    locale = Locale.en_us;
                    throw new Exception("Region or locale not supported.");
            }

            // TODO:  Address the lack of signalR.
            //var hub = WebConfigurationManager.AppSettings["HubAddress"];

            //var connection = new HubConnection(hub);
            //IHubProxy myHub = connection.CreateHubProxy("LeaderboardHub");
            //connection.Start().Wait(); // not sure if you need this if you are simply posting to the hub
       
            //// myHub.Invoke("LeaderboardRequestStarted", bracket, (int)region, DateTime.Now);
            //battleNetLeaderboardService.GetLeaderboard(bracket, ".api.battle.net/", region, Game.wow, locale);
            //await myHub.Invoke("LeaderboardRequestComplete", bracket, (int)region, DateTime.Now);

        }

        [HttpGet]
        [Route("oauth/{code}")]
        public string GetToken(string code)
        {
            var http = new HttpClient();

            var param = new List<KeyValuePair<string, string>>();
            param.Add(new KeyValuePair<string, string>("client_id", "REMOVED"));
            param.Add(new KeyValuePair<string, string>("client_secret", "REMOVED"));
            param.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
            param.Add(new KeyValuePair<string, string>("redirect_uri", "https://localhost"));
            param.Add(new KeyValuePair<string, string>("scope", "wow.profile"));
            param.Add(new KeyValuePair<string, string>("code", code));

            var content = new FormUrlEncodedContent(param);

            HttpResponseMessage response = http.PostAsync("https://us.battle.net/oauth/token", content).Result;
            string stuff = response.Content.ReadAsStringAsync().Result;
            return stuff;

        }
    }


}

