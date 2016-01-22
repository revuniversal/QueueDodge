using QueueDodge.Models;
using System.Collections.Generic;
using System.Linq;

namespace QueueDodge.Services
{
    public class RealmService
    {
        private QueueDodgeDB data { get; set; }

        public RealmService()
        {
            data = new QueueDodgeDB();
        }

        public IEnumerable<Realm> GetRealms(BattleDotSwag.Region region)
        {
            return data
                .Realms
                .Where(p => region == BattleDotSwag.Region.all || p.Region.ID == (int)region)
                .AsEnumerable();
        }
    }
}
