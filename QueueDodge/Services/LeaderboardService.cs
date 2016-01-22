//using System.Collections.Generic;
//using System.Linq;
//using QueueDodge.Models;
//using Microsoft.Data.Entity;
//using Microsoft.Extensions.Caching.Memory;
//using BattleDotSwag;
//using BattleDotSwag.PVP;

//namespace QueueDodge.Services
//{
//    public class LeaderboardService
//    {
//        private QueueDodgeDB data { get; set; }
//        private IMemoryCache cache { get; set; }
//        public LeaderboardService(IMemoryCache cache)
//        {
//            data = new QueueDodgeDB();
//            this.cache = cache;
//        }

//        public LeaderboardViewModel GetLeaderboard(LeaderboardFilter filter)
//        {
//            var key = ((BattleDotSwag.Region)filter.Region).ToString() + ":" + filter.Bracket;
//            IEnumerable<LadderEntry> leaderboard = null;
//            var found = cache.TryGetValue(key, out leaderboard);
            
//            int count = GetCount(filter, leaderboard);
//            int pageCount = count / filter.ItemsPerPage;
//            BattleDotSwag.Region region = (BattleDotSwag.Region)filter.Region;

//            var filteredLeaderboard =
//                leaderboard
//                    .Where(p => filter.Classes.Count == 0 || filter.Classes.Contains(p.ClassID))
//                    .Where(p => filter.Specs.Count == 0 || filter.Specs.Contains(p.SpecID))
//                    .Where(p => filter.Realm == 0 || p.RealmID == filter.Realm);

//            var pagedLeaderboard = GetPagedLeaderboard(filter, filteredLeaderboard);

//            var vm = new LeaderboardViewModel()
//            {
//                Leaderboard = pagedLeaderboard,
//                PageCount = pageCount,
//                ItemsPerPage = filter.ItemsPerPage,
//                TotalItemCount = count,
//                ClassRepresentation = GetClassRepresentation(filter, filteredLeaderboard,count),
//                SpecializationRepresentation = GetSpecializationRepresentation(filter, filteredLeaderboard, count),
//                RaceRepresentation = GetRaceRepresentation(filter, filteredLeaderboard, count),
//                FactionRepresentation = GetFactionRepresentation(filter, filteredLeaderboard, count),
//                RealmRepresentation = GetRealmRepresentation(filter, filteredLeaderboard, count),
//                RegionRepresentation = GetRegionRepresentation(filter, filteredLeaderboard, count)
//            };

//            return vm;
//        }

//        public IEnumerable<Representation<int>> GetClassRepresentation(LeaderboardFilter filter, IEnumerable<LadderEntry> leaderboard, int count)
//        {
//            var representation = leaderboard
//                                    .GroupBy(p => p.ClassID)
//                                    .Select(group => new Representation<int>
//                                    {
//                                        Data = group.Key,
//                                        Count = group.Count(),
//                                        Total = count
//                                    })
//                                    .OrderByDescending(p => p.Count)
//                                    .AsEnumerable();


//            return representation;
//        }
//        public IEnumerable<Representation<object>> GetSpecializationRepresentation(LeaderboardFilter filter, IEnumerable<LadderEntry> leaderboard, int count)
//        {
//            var representation = leaderboard
//                                    .GroupBy(p => new { classID = p.ClassID, specID = p.SpecID })  // TODO:  Make a type for this.
//                                    .Select(group => new Representation<object>
//                                    {
//                                        Data = group.Key,
//                                        Count = group.Count(),
//                                        Total = count
//                                    })
//                                    .OrderByDescending(p => p.Count)
//                                    .Take(15)
//                                    .ToList();

//            return representation;
//        }
//        public IEnumerable<Representation<int>> GetRaceRepresentation(LeaderboardFilter filter, IEnumerable<LadderEntry> leaderboard, int count)
//        {
//            var representation = leaderboard
//                                    .GroupBy(p => p.RaceID)
//                                    .Select(group => new Representation<int>
//                                    {
//                                        Data = group.Key,
//                                        Count = group.Count(),
//                                        Total = count
//                                    })
//                                    .OrderByDescending(p => p.Count)
//                                    .ToList();

//            return representation;
//        }
//        public IEnumerable<Representation<object>> GetRealmRepresentation(LeaderboardFilter filter, IEnumerable<LadderEntry> leaderboard, int count)
//        {
//            var representation = leaderboard
//                                    .GroupBy(p => new { region = p.Request.RegionID, realm = p.RealmName })  // TODO:  Make a type for this.
//                                    .Select(group => new Representation<object>
//                                    {
//                                        Data = group.Key,
//                                        Count = group.Count(),
//                                        Total = count
//                                    })
//                                    .OrderByDescending(p => p.Count)
//                                    .OrderByDescending(p => p.Count)
//                                    .Take(15)
//                                    .ToList();

//            return representation;
//        }
//        public IEnumerable<Representation<int>> GetFactionRepresentation(LeaderboardFilter filter, IEnumerable<LadderEntry> leaderboard, int count)
//        {
//            var representation = leaderboard
//                                    .GroupBy(p => p.FactionID)
//                                    .Select(group => new Representation<int>
//                                    {
//                                        Data = group.Key,
//                                        Count = group.Count(),
//                                        Total = count
//                                    })
//                                    .OrderByDescending(p => p.Count)
//                                    .ToList();

//            return representation;
//        }
//        public IEnumerable<Representation<int>> GetRegionRepresentation(LeaderboardFilter filter, IEnumerable<LadderEntry> leaderboard, int count)
//        {
//            var representation = leaderboard
//                                    .GroupBy(p => p.Request.RegionID)
//                                    .Select(group => new Representation<int>
//                                    {
//                                        Data = group.Key,
//                                        Count = group.Count(),
//                                        Total = count
//                                    })
//                                    .OrderBy(p => p.Count)
//                                    .ToList();

//            return representation;
//        }

//        private int GetCount(LeaderboardFilter filter, IEnumerable<LadderEntry> leaderboard)
//        {
//            filter.Classes = filter.Classes ?? new List<int>();
//            filter.Specs = filter.Specs ?? new List<int>();

//            return leaderboard
//                       .Where(p => filter.Classes.Count == 0 || filter.Classes.Contains(p.ClassID))
//                       .Where(p => filter.Specs.Count == 0 || filter.Specs.Contains(p.SpecID))
//                       .Where(p => filter.Realm == 0 || p.RealmID == filter.Realm)
//                       .Count();
//        }

//        private IEnumerable<LadderEntry> GetPagedLeaderboard(LeaderboardFilter filter, IEnumerable<LadderEntry> leaderboard)
//        {
//            filter.Page--;

//            if (filter.Page != 0)
//            {
//                leaderboard =
//                    leaderboard
//                        .OrderBy(p => p.Ranking)
//                        .Skip(filter.Page * filter.ItemsPerPage)
//                        .Take(filter.ItemsPerPage)
//                        .Select(p => p);
//            }
//            else
//            {
//                leaderboard =
//                    leaderboard
//                        .OrderBy(p => p.Ranking)
//                        .Take(filter.ItemsPerPage)
//                        .Select(p => p);
//            }

//            return leaderboard.AsEnumerable();
//        }
//    }
//}
