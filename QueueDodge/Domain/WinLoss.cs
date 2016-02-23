using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueDodge
{
    public class WinLoss
    {
        public int ID { get; set; }
        public int Wins { get; }
        public int Losses { get; }

        public WinLoss(int wins, int losses)
        {
            Wins = wins;
            Losses = losses;
        }
    }
}
