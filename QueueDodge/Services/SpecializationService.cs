using QueueDodge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDodge.Services
{
    public class SpecializationService
    {
        private QueueDodgeDB data { get; set; }

        public SpecializationService()
        {
            data = new QueueDodgeDB();
        }

        public IEnumerable<Specialization> GetSpecializations()
        {
            return data.Specializations.AsEnumerable();
        }
    }
}
