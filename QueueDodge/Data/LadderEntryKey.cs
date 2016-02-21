namespace QueueDodge.Data
{
    public class LadderEntryKey
    {
        private int RegionID { get; set; }
        private int RealmID { get; set; }
        private string Name { get; set; }
        private string Bracket { get; set; }

        public LadderEntryKey(int regionID, int realmID, string name, string bracket)
        {
            RegionID = regionID;
            RealmID = realmID;
            Name = name;
            Bracket = bracket;
        }
    }
}
