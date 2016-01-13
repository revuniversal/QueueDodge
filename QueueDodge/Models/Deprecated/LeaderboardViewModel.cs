using System.Collections.Generic;

namespace QueueDodge.Models
{
    public class LeaderboardViewModel
    {
        public int PageCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItemCount { get; set; }
//        public IEnumerable<Leaderboard> Leaderboard { get; set; }

        public IEnumerable<Representation<int>> ClassRepresentation { get; set; }
        public IEnumerable<Representation<object>> SpecializationRepresentation { get; set; }
        public IEnumerable<Representation<int>> RaceRepresentation { get; set; }
        public IEnumerable<Representation<object>> RealmRepresentation { get; set; }
        public IEnumerable<Representation<int>> FactionRepresentation { get; set; }
        public IEnumerable<Representation<int>> RegionRepresentation { get; set; }
    }
}
