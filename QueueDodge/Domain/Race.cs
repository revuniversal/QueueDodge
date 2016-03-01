using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge
{
    public class Race
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int FactionID { get; set; }
        public virtual Faction Faction { get; set; }

        public Race() { }
        public Race(int id, string name, int factionId)
        {
            ID = id;
            Name = name;
            FactionID = factionId;
        }
    }
}
