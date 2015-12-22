using QueueDodge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDodge.Services
{
    public class ClassService
    {
        private QueueDodgeDB data { get; set; }

        public ClassService()
        {
            data = new QueueDodgeDB();
        }

        public IEnumerable<Class> GetClasses()
        {
            return data.Classes.AsEnumerable();
        }
    }
}
