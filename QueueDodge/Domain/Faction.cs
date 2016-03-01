namespace QueueDodge
{
    public class Faction
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Faction() { }
        public Faction(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
