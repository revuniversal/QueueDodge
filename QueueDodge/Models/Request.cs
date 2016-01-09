using System;

namespace QueueDodge.Models
{
    public class BattleNetRequest
    {
        public int ID { get; set; }
        public string RequestType { get; set; }
        public DateTime RequestDate { get; set; }
        public int RegionID { get; set; }
        public string Locale { get; set; }
        public string Url { get; set; }
        public double Duration { get; set; }
        public string Bracket { get; set; }
    }
}
