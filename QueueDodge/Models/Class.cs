using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge.Models
{
    public class Class
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual List<Specialization> Specializations { get; set; }
    }
}
