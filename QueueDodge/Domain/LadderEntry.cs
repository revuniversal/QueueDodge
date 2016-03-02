using BattleDotSwag.WoW.PVP;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge
{
    [NotMapped]
    public class LadderEntry
    {
        public int ID { get; set; }
        public Character Character { get; set; }
        public string Bracket { get; set; }
        public int Ranking { get; set; }
        public int Rating { get; set; }
        public int SeasonWins { get; set; }
        public int SeasonLosses { get; set; }
        public int WeeklyWins { get; set; }
        public int WeeklyLosses { get; set; }
        public LadderEntry() { }

        public LadderEntry(Character character, string bracket, int ranking, int rating, int seasonWins, int seasonLosses, int weeklyWins, int weeklyLosses)
        {
            Bracket = bracket;
            Character = character;
            Ranking = ranking;
            Rating = rating;
            SeasonWins = seasonWins;
            SeasonLosses = seasonLosses;
            WeeklyWins = weeklyWins;
            WeeklyLosses = weeklyLosses;
        }
    }
}
