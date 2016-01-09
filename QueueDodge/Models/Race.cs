using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge.Models
{
    public class Race
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public int FactionID { get; set; }
        public string Name { get; set; }

        public virtual Faction Faction { get; set; }
    }
}
