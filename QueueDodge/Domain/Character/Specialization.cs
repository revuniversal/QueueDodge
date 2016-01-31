using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge.Models
{
    public class Specialization
    {
        public int ID { get; }
        public Specialization(int id)
        {
            ID = id;
        }
    }
}
