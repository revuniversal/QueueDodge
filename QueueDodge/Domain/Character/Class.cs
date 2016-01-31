using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge.Models
{
    public class Class
    {
        public int ID { get; set; }

        public Class(int id)
        {
            ID = id;
        }
    }
}
