namespace QueueDodge.Models
{
    public class LadderChange
    {
        public int ID { get; set; }

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
        
        public virtual Realm Realm { get; set; }
        public virtual BattleNetRequest PreviousRequest { get; set; }
        public virtual BattleNetRequest CurrentRequest { get; set; }

        public LadderChange()
        { }
        public LadderChange(LadderEntry previous, LadderEntry detected)
        {
            Name = detected.Name;
            Bracket = detected.Request.Bracket;
            RegionID = detected.Request.RegionID;
            RealmID = detected.RealmID;
            PreviousRequestID = previous.Request.ID;
            PreviousClass = previous.ClassID;
            PreviousFaction = previous.FactionID;
            PreviousGenderID = previous.GenderID;
            PreviousRace = previous.RaceID;
            PreviousRanking = previous.Ranking;
            PreviousRating = previous.Rating;
            PreviousSpec = previous.SpecID;
            PreviousSeasonWins = previous.SeasonWins;
            PreviousSeasonLosses = previous.SeasonLosses;
            PreviousWeeklyWins = previous.WeeklyWins;
            PreviousWeeklyLosses = previous.WeeklyLosses;
            CurrentRequestID = detected.Request.ID;
            DetectedClass = detected.ClassID;
            DetectedFaction = detected.FactionID;
            DetectedGenderID = detected.GenderID;
            DetectedRace = detected.RaceID;
            DetectedRanking = detected.Ranking;
            DetectedRating = detected.Rating;
            DetectedSpec = detected.SpecID;
            DetectedSeasonWins = detected.SeasonWins;
            DetectedSeasonLosses = detected.SeasonLosses;
            DetectedWeeklyWins = detected.WeeklyWins;
            DetectedWeeklyLosses = detected.WeeklyLosses;


        }

        // TODO:  CHECK FOR MORE CHANGES!  MORE OPPORTUNITIES!
        public bool Changed() => (PreviousSeasonWins != DetectedSeasonWins) || (PreviousSeasonLosses != DetectedSeasonLosses);

    }
}
