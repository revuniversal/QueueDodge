namespace QueueDodge
{
    public class Race
    {
        public int ID { get; }
        public Faction Faction { get; }

        public Race(int id, Faction faction)
        {
            ID = id;
            Faction = faction;
        }
    }
}
