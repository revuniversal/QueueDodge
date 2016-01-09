using System.Collections.Generic;

namespace QueueDodge.Models
{
    public class LeaderboardChangesViewModel
    {
        public IEnumerable<LadderChange> Changes { get; set; }
        public BattleDotSwag.Region Region { get; set; }
        public string Bracket { get; set; }
    }
}
