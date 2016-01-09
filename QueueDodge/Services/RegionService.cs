using QueueDodge.Models;
using System.Collections.Generic;
using System.Linq;

namespace QueueDodge.Services
{
    public class RegionService
    {
        private QueueDodgeDB data { get; set; }

        public RegionService()
        {
            data = new QueueDodgeDB();
        }

        public IEnumerable<Region> GetRegions()
        {
            return  data.Regions.ToList();
        }
    }
}
