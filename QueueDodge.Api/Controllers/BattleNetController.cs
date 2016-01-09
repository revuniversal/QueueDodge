using BattleDotSwag;
using Microsoft.AspNet.Mvc;
using QueueDodge.Integrations;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Specialized;

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
            //var http = new HttpClient();

            //var param = new List<KeyValuePair<string, string>>();
            //param.Add(new KeyValuePair<string, string>("client_id", "REMOVED"));
            //param.Add(new KeyValuePair<string, string>("client_secret", "REMOVED"));
            //param.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
            //param.Add(new KeyValuePair<string, string>("redirect_uri", "https://localhost"));
            //param.Add(new KeyValuePair<string, string>("scope", "wow.profile"));
            //param.Add(new KeyValuePair<string, string>("code", code));

            //var content = new FormUrlEncodedContent(param);

            //HttpResponseMessage response = http.PostAsync("https://us.battle.net/oauth/token", content).Result;
            //string stuff = response.Content.ReadAsStringAsync().Result;
            //return stuff;

            var clientID = "heuemgj94eyv484cekut2a82d6crnskm";
            var secret = "Cw5V9sdXnxdvHj5gexXvJQbQ4g9MTcds";

            string pass = Convert.ToBase64String(Encoding.ASCII.GetBytes(clientID + ":" + secret));

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://us.battle.net/oauth/token"),
                Method = HttpMethod.Post,
            };


           // request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", pass);
          
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://us.battle.net/oauth/token");

                //var requestContent = string.Format("code={0}&grant_type=authorization_code&redirect_uri={1}&scope=wow.profile&client_id={2}&client_secret={3}",
                var requestContent = string.Format("code={0}&grant_type=authorization_code&redirect_uri={1}&scope=wow.profile",
                                                    code, "https://localhost/");

                var content = new StringContent(requestContent, Encoding.UTF8, "application/x-www-form-urlencoded");
                request.Content = content;
                //   var response = client.PostAsync(client.BaseAddress, content).Result;
                var response = client.SendAsync(request).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                return result;
            }


        }




     //   public object getToken(string authorizationCode)
        //{
        //    using (WebClient wc = new WebClient())
        //    {
        //        string pass = Convert.ToBase64String(Encoding.ASCII.GetBytes(clientID + ":" + clientSecret));
        //        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        //        wc.Headers[HttpRequestHeader.Authorization] = "Basic " + pass;

        //        var data = new NameValueCollection();
        //        data.Add("redirect_uri", "https://dev.battle.net/");
        //        data.Add("scope", "wow.profile");
        //        data.Add("grant_type", "authorization_code");
        //        data.Add("code", authorizationCode);

        //        string result = Encoding.UTF8.GetString(wc.UploadValues("https://us.battle.net/oauth/token", data));
        //        Console.WriteLine(result.ToString());

        //        return new BattleNetToken(
        //            BattleNetToken.getTokenFromJson(result),
        //            BattleNetToken.getTokenTypeFromJson(result),
        //            BattleNetToken.getTokenEndFromJson(result),
        //            BattleNetToken.getTokenScopeFromJson(result));
        //    }
        //}

    }


}

