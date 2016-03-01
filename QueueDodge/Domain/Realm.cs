using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge
{
    public class Realm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int RegionID { get; set; }
        public virtual Region Region { get; set; }
        public Realm() { }
        public Realm(int id, string name, string slug, Region region)
        {
            ID = id;
            Name = name;
            Slug = slug;
            RegionID = region.ID;
            Region = region;
        }
    }
}
