using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge.Models
{
    public class Realm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int RegionID { get; set; }

        public virtual Region Region { get; set; }
    }
}
