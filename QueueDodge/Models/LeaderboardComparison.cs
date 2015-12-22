using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDodge.Models
{
    public class LeaderboardComparison
    {
        public int ID { get; set; }
        
        public int CurrentRequestID { get; set; }
        public int PreviousRequestID { get; set; }

        public virtual BattleNetRequest CurrentRequest { get; set; }
        public virtual BattleNetRequest PreviousRequest { get; set; }
    }
}
