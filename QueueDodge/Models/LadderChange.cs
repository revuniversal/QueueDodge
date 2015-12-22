using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDodge.Models
{
    public class LadderChange
    {
        public int ID { get; set; }

        public int LadderComparisonID { get; set; }
        public int PreviousRequestID { get; set; }
        public int CurrentRequestID { get; set; }

        public string Bracket { get; set; }
        public string Name { get; set; }
        public int RealmID { get; set; }
        public int RegionID { get; set; }

        public int PreviousFaction { get; set; }
        public int DetectedFaction { get; set; }

        public int PreviousRace { get; set; }
        public int DetectedRace { get; set; }

        public int PreviousClass { get; set; }
        public int DetectedClass { get; set; }

        public int PreviousSpec { get; set; }
        public int DetectedSpec { get; set; }

        public int PreviousRanking { get; set; }
        public int PreviousRating { get; set; }

        public int DetectedRanking { get; set; }
        public int DetectedRating { get; set; }

        public int PreviousSeasonWins { get; set; }
        public int DetectedSeasonWins { get; set; }

        public int PreviousSeasonLosses { get; set; }
        public int DetectedSeasonLosses { get; set; }

        public int PreviousWeeklyLosses { get; set; }
        public int DetectedWeeklyLosses { get; set; }

        public int PreviousWeeklyWins { get; set; }
        public int DetectedWeeklyWins { get; set; }

        public int PreviousGenderID { get; set; }
        public int DetectedGenderID { get; set; }

        public virtual LeaderboardComparison LadderComparison { get; set; }
        public virtual Realm Realm { get; set; }
    }
}
