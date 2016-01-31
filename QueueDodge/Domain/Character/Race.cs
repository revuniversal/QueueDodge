using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge.Models
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
