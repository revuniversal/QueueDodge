using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

using BattleDotSwag;
using BattleDotSwag.Services;
using BattleDotSwag.WoW.PVP;

namespace QueueDodge
{
    public class LadderInspector
    {
        private string apiKey;
        private QueueDodgeDB queueDodge;
        private IMemoryCache cache;
        private Func<string, Task> sendMessage;

        public LadderInspector(string apiKey, QueueDodgeDB queueDodge, IMemoryCache cache, Func<string, Task> sendMessage)
        {
            this.apiKey = apiKey;
            this.queueDodge = queueDodge;
            this.cache = cache;
            this.sendMessage = sendMessage;
        }
        
        public async Task<IEnumerable<LadderChange>> DetectChanges(string bracket, Locale locale, BattleDotSwag.Region region)
        {
            var processed = new List<Row>();
            var leaderboard = await GetActivity(bracket, locale, region);

            var changes = leaderboard.Rows
                .Dedupe(processed)
                .ConvertToLadderEntry(bracket, region)
                .ExistsInCache(cache)
                .CharacterChanged()
                .SaveRealm(queueDodge)
                .SaveCharacter(queueDodge)
                .SaveLadderChange(queueDodge)
                .NotifyChanged(sendMessage)
                .ToList();
                
            queueDodge.SaveChanges();   
            
            return changes;
        }     
          
        public async Task<Leaderboard> GetActivity(string bracket, Locale locale, BattleDotSwag.Region region)
        {
            var service = new BattleNetService<Leaderboard>();
            var endpoint = new LeaderboardEndpoint(bracket, locale, apiKey);
            var leaderboard = await service.Get(endpoint, region);
            return leaderboard;
        }
        
        /*public async Task<IEnumerable<LadderEntry>> GetLadder(LadderFilter filter)
        {
            IEnumerable<LadderEntry> ladder;    
            var ladderExists = cache.TryGetValue("", out ladder);
            
            if(ladderExists)
            {
                return ladder;
            }
        }*/
    }
}
