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
        // TODO:  Think about making a factory class for this.
        public static LadderEntry Create(Row row, string bracket, Region region)
        {
            var realm = new Realm(row.RealmID, row.RealmName, row.RealmSlug, region);
            var faction = new Faction(row.FactionID, row.FactionID == 0 ? "allaince" : "horde");
            var race = new Race(row.RaceID, 
                row.FactionID == 0 ? "allaince" : "horde",
                row.FactionID);
            var characterClass = new Class(row.ClassID);
            var specialization = new Specialization(row.SpecID);
            var character = new Character(row.Name, row.GenderID, realm, race, characterClass, specialization);

            var entry = new LadderEntry(character, bracket, row.Ranking, row.Rating, row.SeasonWins, row.SeasonLosses, row.WeeklyWins, row.WeeklyLosses);

            return entry;
        }
    }
}
