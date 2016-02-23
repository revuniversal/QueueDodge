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
        public WinLoss Season { get; }
        public WinLoss Weekly { get; }

        public LadderEntry(Character character, string bracket, int ranking, int rating, WinLoss season, WinLoss weekly)
        {
            Bracket = bracket;
            Character = character;
            Ranking = ranking;
            Rating = rating;
            Season = season;
            Weekly = weekly;
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
            var season = new WinLoss(row.SeasonWins, row.SeasonLosses);
            var weekly = new WinLoss(row.WeeklyWins, row.WeeklyLosses);

            var entry = new LadderEntry(character, bracket, row.Ranking, row.Rating, season, weekly);

            return entry;
        }
    }
}
