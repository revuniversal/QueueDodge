namespace QueueDodge
{
    public class Realm
    {
        public int ID { get; }
        public string Name { get; }
        public string Slug { get; }
        public Region Region { get; }

        public Realm(int id, string name, string slug, Region region)
        {
            ID = id;
            Name = name;
            Slug = slug;
            Region = region;
        }
    }
}
