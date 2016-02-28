using BattleDotSwag.WoW.PVP;

namespace QueueDodge
{
    public class LadderEntry
    {
        public int ID { get; set; }
        public Character Character { get; }
        public string Bracket { get; }
        public int Ranking { get; }
        public int Rating { get; }
        public int SeasonWins { get; }
        public int SeasonLosses { get; }
        public int WeeklyWins { get; }
        public int WeeklyLosses { get; }

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
            var faction = new Faction(row.FactionID);
            var race = new Race(row.RaceID, faction);
            var characterClass = new Class(row.ClassID);
            var specialization = new Specialization(row.SpecID);
            var character = new Character(row.Name, row.GenderID, realm, race, characterClass, specialization);

            var entry = new LadderEntry(character, bracket, row.Ranking, row.Rating, row.SeasonWins, row.SeasonLosses, row.WeeklyWins, row.WeeklyLosses);

            return entry;
        }
    }
}
