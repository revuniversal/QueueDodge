using System.Collections.Generic;

namespace QueueDodge.Models
{
    public class LeaderboardFilter
    {
        public string Bracket { get; set; }
        public int Region { get; set; }
        public List<int> Classes { get; set; }
        public List<int> Specs { get; set; }
        public int Realm { get; set; }
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
    }
}
