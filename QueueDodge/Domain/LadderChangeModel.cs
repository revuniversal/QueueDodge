using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueDodge.Data
{
    // TODO:  having one copy of character doesnt allow tracking of race changes or faction changes etc.
    public class LadderChangeModel
    {
        public int ID { get; set; }
        public int CharacterID { get; set; }

        public virtual Character Character { get; set; }
        public string Bracket { get; set; }

        public int PreviousRanking { get; set; }
        public int PreviousRating { get; set; }
        public int PreviousSeasonWins { get; set; }
        public int PreviousSeasonLosses { get; set; }
        public int PreviousWeeklyWins { get; set; }
        public int PreviousWeeklyLosses { get; set; }

        public int CurrentRanking { get; set; }
        public int CurrentRating { get; set; }
        public int CurrentSeasonWins { get; set; }
        public int CurrentSeasonLosses { get; set; }
        public int CurrentWeeklyWins { get; set; }
        public int CurrentWeeklyLosses { get; set; }

        public LadderChangeModel() { }

        public LadderChangeModel(LadderChange ladderChange)
        {
            this.Character = ladderChange.Current.Character;
            Bracket = ladderChange.Current.Bracket;

            PreviousRanking = ladderChange.Previous.Ranking;
            PreviousRating = ladderChange.Previous.Rating;
            PreviousSeasonWins = ladderChange.Previous.SeasonWins;
            PreviousSeasonLosses = ladderChange.Previous.SeasonLosses;
            PreviousWeeklyWins = ladderChange.Previous.WeeklyWins;
            PreviousWeeklyLosses = ladderChange.Previous.WeeklyLosses;

            CurrentRanking = ladderChange.Current.Ranking;
            CurrentRating = ladderChange.Current.Rating;
            CurrentSeasonWins = ladderChange.Current.SeasonWins;
            CurrentSeasonLosses = ladderChange.Current.SeasonLosses;
            CurrentWeeklyWins = ladderChange.Current.WeeklyWins;
            CurrentWeeklyLosses = ladderChange.Current.WeeklyLosses;
        }
    }
}


