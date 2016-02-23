using System.Collections.Generic;

namespace QueueDodge
{
    public class LadderFilter
    {
        public string Bracket { get; set; }
        public Region Region { get; set; }
        public List<int> Classes { get; set; }
        public List<int> Specs { get; set; }
        public Realm Realm { get; set; }
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
    }
}
