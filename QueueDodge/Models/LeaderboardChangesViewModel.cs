using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDodge.Models
{
    public class LeaderboardChangesViewModel
    {
        public IEnumerable<LadderChange> Changes { get; set; }
        public BattleDotSwag.Region Region { get; set; }
        public string Bracket { get; set; }
    }
}
