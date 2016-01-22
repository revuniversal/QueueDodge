using System.ComponentModel.DataAnnotations;

namespace QueueDodge.Models
{
    public class LadderEntry
    {
        [Key]
        public int Ranking { get; set; }
        public int RegionID { get; set; }
        public string Bracket { get; set; }
        // public int RequestID { get; set; }
        public int Rating { get; set; }
        public string Name { get; set; }
        public int RealmID { get; set; }
        public string RealmName { get; set; }
        public string RealmSlug { get; set; }
        public int RaceID { get; set; }
        public int ClassID { get; set; }
        public int SpecID { get; set; }
        public int FactionID { get; set; }
        public int GenderID { get; set; }
        public int SeasonWins { get; set; }
        public int SeasonLosses { get; set; }
        public int WeeklyWins { get; set; }
        public int WeeklyLosses { get; set; }

        //  public BattleNetRequest Request { get; set; }

        public LadderEntry() { }
        public LadderEntry(BattleDotSwag.PVP.Row entry, BattleDotSwag.Region region, string bracket)
        {
            //  Request = request;
            Bracket = bracket;
            RegionID = (int)region;
            Ranking = entry.Ranking;
            Rating = entry.Rating;
            Name = entry.Name;
            RealmID = entry.RealmID;
            RealmName = entry.RealmName;
            RealmSlug = entry.RealmSlug;
            RaceID = entry.RaceID;
            ClassID = entry.ClassID;
            SpecID = entry.SpecID;
            FactionID = entry.FactionID;
            GenderID = entry.GenderID;
            SeasonWins = entry.SeasonWins;
            SeasonLosses = entry.SeasonLosses;
            WeeklyWins = entry.WeeklyWins;
            WeeklyLosses = entry.WeeklyLosses;
        }
    }
}
