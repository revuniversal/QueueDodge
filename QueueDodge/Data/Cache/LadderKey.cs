using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueDodge.Data.Cache
{
    public class LadderKey
    {
        private string Bracket { get; set; }
        private string Region { get; set; }
        public LadderKey(string bracket, string region)
        {
            Bracket = bracket;
            Region = region;
        }

    }
}
