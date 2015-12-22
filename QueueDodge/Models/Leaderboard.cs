using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDodge.Models
{
    public class Leaderboard
    {
        public int ID { get; set; }
        public int RequestID { get; set; }
        public string Bracket { get; set; }
        public int RegionID { get; set; }
        public int Ranking { get; set; }
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

        //public virtual Request Request { get; set; }
        //public virtual Class Class { get; set; }
        //public virtual Race Race { get; set; }
        //public virtual Specialization Spec { get; set; }
        //public virtual Realm Realm { get; set; }
        //public virtual Faction Faction { get; set; }
        //public virtual Region Region { get; set; }
    }
}
