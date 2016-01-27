using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge.Models
{
    public class Specialization
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int ClassID { get; set; }
    }
}
