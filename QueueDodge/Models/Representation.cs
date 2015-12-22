using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDodge.Models
{
    public class Representation <T>
    {
        public T Data { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
    }
}
