using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge.Models
{

    public class Region
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
