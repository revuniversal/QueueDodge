using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueDodge.Models
{
    public class WinLoss
    {
        public int Wins { get; }
        public int Losses { get; }

        public WinLoss(int wins, int losses)
        {
            Wins = wins;
            Losses = losses;
        }
    }
}
