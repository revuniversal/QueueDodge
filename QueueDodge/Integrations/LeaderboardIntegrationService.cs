using System;
using System.Linq;
using System.Data.SqlClient;
using QueueDodge.Models;
using BattleDotSwag;
using BattleDotSwag.Services;
using BattleDotSwag.PVP;
using QueueDodge.Services;
using Microsoft.Data.Entity;

namespace QueueDodge.Integrations
{
    public class LeaderboardIntegrationService
    {
        private string apiKey;
        private BattleNetService leaderboard;
        private RequestService requests;
        private QueueDodgeDB data;

        public LeaderboardIntegrationService(QueueDodgeDB data, string apiKey)
        {
            this.apiKey = apiKey;
            this.data = data;
            leaderboard = new BattleNetService();
            requests = new RequestService(data);
        }

        public BattleNetRequest GetLeaderboard(string bracket, string host, BattleDotSwag.Region region, Game game, Locale locale)
        {
            var endpoint = new LeaderboardEndpoint(bracket, locale, apiKey);

            var newRequest = new BattleNetRequest()
            {
                Locale = locale.ToString(),
                RegionID = (int)region,
                RequestDate = DateTime.Now,
                RequestType = "leaderboards",
                Bracket = bracket,
                Url = leaderboard.GetUri(endpoint, host, region, game).ToString()
            };

            //BattleNetRequest request =
                data.BattleNetRequests.Add(newRequest);
            data.SaveChanges();

            // TODO:  Fix leaderboard request.

            //requests.Perform(10, 2, 5, request, () =>
            //{
            //    var json = leaderboard.Get(endpoint, host, region, game).Result;
            //    //BulkInsertLeaderboards(json, request.ID, bracket, region);
            //});

            //return request;
            return null;
        }

        //private void BulkInsertLeaderboards(string json, int requestID, string bracket, BattleDotSwag.Region region)
        //{
        //    BattleDotSwag.PVP.Leaderboard data = JsonConvert.DeserializeObject<BattleDotSwag.PVP.Leaderboard>(json);

        //    var dt = new DataTable("Leaderboards");

        //    dt.Columns.Add("ID", typeof(int));
        //    dt.Columns.Add("RequestID", typeof(int));
        //    dt.Columns.Add("Bracket");

        //    //dt.Columns.Add("RegionID", typeof(int));


        //    dt.Columns.Add("Ranking", typeof(int));
        //    dt.Columns.Add("Rating", typeof(int));
        //    dt.Columns.Add("Name");
        //    dt.Columns.Add("RealmID", typeof(int));
        //    dt.Columns.Add("RealmName");
        //    dt.Columns.Add("RealmSlug");
        //    dt.Columns.Add("RaceID", typeof(int));
        //    dt.Columns.Add("ClassID", typeof(int));
        //    dt.Columns.Add("SpecID", typeof(int));
        //    dt.Columns.Add("FactionID", typeof(int));
        //    dt.Columns.Add("GenderID", typeof(int));
        //    dt.Columns.Add("SeasonWins", typeof(int));
        //    dt.Columns.Add("SeasonLosses", typeof(int));
        //    dt.Columns.Add("WeeklyWins", typeof(int));
        //    dt.Columns.Add("WeeklyLosses", typeof(int));

        //    dt.Columns.Add("RegionID", typeof(int));

        //    foreach (var item in data.Rows)
        //    {
        //        DataRow dr = dt.NewRow();

        //        dr["RequestID"] = requestID;
        //        dr["Bracket"] = bracket;

        //        //dr["RegionID"] = (int)region;

        //        dr["Ranking"] = item.Ranking;
        //        dr["Rating"] = item.Rating;
        //        dr["Name"] = item.Name;
        //        dr["RealmID"] = ValidateRealmID(item.RealmID.ToString());  // this is stupid... but battle.net has bad data for some players.  aka player without a specified realm.
        //        dr["RealmName"] = item.RealmName;
        //        dr["RealmSlug"] = item.RealmSlug;
        //        dr["RaceID"] = item.RaceID;
        //        dr["ClassID"] = item.ClassID;
        //        dr["SpecID"] = item.SpecID;
        //        dr["FactionID"] = item.FactionID;
        //        dr["GenderID"] = item.GenderID;
        //        dr["SeasonWins"] = item.SeasonWins;
        //        dr["SeasonLosses"] = item.SeasonLosses;
        //        dr["WeeklyWins"] = item.WeeklyWins;
        //        dr["WeeklyLosses"] = item.WeeklyLosses;

        //        dr["RegionID"] = (int)region;

        //        dt.Rows.Add(dr);
        //    }

        //    var connection = System.Configuration.ConfigurationManager.ConnectionStrings["QueueDodge"].ConnectionString;

        //    using (SqlBulkCopy s = new SqlBulkCopy(connection))
        //    {
        //        s.DestinationTableName = "Leaderboards";
        //        s.WriteToServer(dt);
        //    }

        //    CompareLeaderboards(bracket, region);
        //}

        private int ValidateRealmID(string id)
        {
            int realmID = 999;
            int.TryParse(id, out realmID);
            return realmID;
        }

        private void CompareLeaderboards(string bracket, BattleDotSwag.Region region)
        {
            int currentRequestID =
                data
                .Leaderboards
                .Where(p => p.Bracket == bracket && p.RegionID == (int)region)
                .OrderByDescending(p => p.RequestID)
                .Select(p => p.RequestID)
                .FirstOrDefault();

            int previousRequestID =
                data
                .Leaderboards
                .Where(p => p.RequestID != currentRequestID && p.Bracket == bracket && p.RegionID == (int)region)
                .OrderByDescending(p => p.RequestID)
                .Select(p => p.RequestID)
                .FirstOrDefault();

            if (previousRequestID == 0)
            {
                previousRequestID = currentRequestID;
            }


            var comparison = new LeaderboardComparison()
            {
                CurrentRequestID = currentRequestID,
                PreviousRequestID = previousRequestID
            };

            data.LeaderboardComparisons.Add(comparison);
            data.SaveChanges();

            var bracketParam = new SqlParameter("@Bracket", bracket);
            var previousParam = new SqlParameter("@PreviousRequestID", previousRequestID);
            var currentParam = new SqlParameter("@CurrentRequestID", currentRequestID);
            var comparisonPAram = new SqlParameter("@LeaderboardComparisonID", comparison.ID);

            data.Database.ExecuteSqlCommand("exec usp_Leaderboards_DetectChanges @bracket, @PreviousRequestID, @currentRequestID, @LeaderboardComparisonID", bracketParam, previousParam, currentParam, comparisonPAram);

            //var oldData =
            //    data
            //    .Leaderboards
            //    .Where(p => p.RequestID != currentRequestID)
            //    .AsEnumerable();

            //data.Leaderboards.RemoveRange(oldData);
           // data.SaveChanges();
        }
    }
}
