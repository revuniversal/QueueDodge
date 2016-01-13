using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleDotSwag;
using BattleDotSwag.PVP;
using BattleDotSwag.Services;
using QueueDodge.Integrations;
namespace LeaderboardSync
{
    public class Program
    {
        // TODO:  Each endpoint(or aggregate root) should have an implementation of this abstracted away as an easy to use class. 
        public static void Main(string[] args)
        {
            try
            {

           //     Console.Read();
                var bracket = args[0];
                var key = args[1];
                var locale = (Locale)Enum.Parse(typeof(Locale), args[2]);
                var region = (Region)Enum.Parse(typeof(Region), args[3]);


             
                //Console.WriteLine("REQUEST:  " + DateTime.Now.ToString());

                //var endpoint = new LeaderboardEndpoint(bracket, locale, key);
                //var requestor = new BattleNetService<Leaderboard>();
                //var leaderboard = requestor.Get(endpoint, region).Result;

                //Console.WriteLine("RESPONSE:  " + DateTime.Now.ToString());

                //Console.WriteLine("LOG START:  " + DateTime.Now.ToString());

                //var request = new QueueDodge.Models.BattleNetRequest()
                //{
                //    Bracket = bracket,
                //    Locale = locale.ToString(),
                //    RegionID = (int)region,
                //    RequestDate = DateTime.Now,
                //    RequestType = "leaderboard",
                //    Url = requestor.GetUri(endpoint, region).ToString(),
                //    Duration = 0

                //};
                //var context = new QueueDodge.QueueDodgeDB();
                //var request2 = context.BattleNetRequests .Add(request).Entity;
                //context.SaveChanges();

                //Console.WriteLine("LOG FINISH:  " + DateTime.Now.ToString());

                //Console.WriteLine("INSERT START:  " + DateTime.Now.ToString());

                //var service = new LeaderboardIntegrationService(context,key);
                //service.InsertLeaderboards(leaderboard, request2, bracket, region);

                //Console.WriteLine("INSERT FINISH:  " + DateTime.Now.ToString());

                //Console.WriteLine("COMPARE START:  " + DateTime.Now.ToString());

                //service.CompareLeaderboards(bracket, region);

                //Console.WriteLine("COMPARE FINISH:  " + DateTime.Now.ToString());

            //    Console.Read();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());
                Console.Read();
            }
        }
    }
}
