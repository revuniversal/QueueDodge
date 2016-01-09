using QueueDodge.Models;
using System.Collections.Generic;
using System.Linq;

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
