using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge.Models
{
    public class Faction
    {
        public int ID { get; }
        public Faction(int id)
        {
            ID = id;
        }
    }
}
