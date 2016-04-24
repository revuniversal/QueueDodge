
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BattleDotSwag.WoW.PVP;
using QueueDodge;
using QueueDodge.Domain;

public static class LadderExtensions
    {
        public static IEnumerable<Row> IsNotADuplicate(this IEnumerable<Row> source, IList<Row> tracking)
        {
            var distinctRows = source
            .Where(p => !tracking
                        .Where(o => o.Name == p.Name && o.RealmID == p.RealmID)
                        .Any());
                        
            foreach(var row in distinctRows)
            {
                tracking.Add(row);
                yield return row;
            }
           
        }
        public static IEnumerable<LadderEntry> ConvertToLadderEntry(this IEnumerable<Row> source, string bracket, BattleDotSwag.Region region)
        {
            var factory = new LadderEntryFactory();
            
            foreach(var row in source)
            {
                var _region = new Region((int)region, region.ToString());
                var ladderEntry = factory.Create(row, bracket, _region);
                yield return ladderEntry; 
            }
        }     
        public static IEnumerable<LadderEntryPair> CheckCache(this IEnumerable<LadderEntry> source, IMemoryCache cache)
        {
            foreach(var entry in source)
            {
                var realKey = entry.Character.Name + ":" + entry.Character.RealmID + ":" + entry.Bracket;

                var cachedEntry = default(LadderEntry);
                var cached = cache.TryGetValue(realKey, out cachedEntry);
                cache.Set(realKey, entry);

                if (cached)
                {
                    var pair = new LadderEntryPair(entry,cachedEntry);
                    yield return pair;
                }
            }
        }
        public static IEnumerable<LadderChange> Changed(this IEnumerable<LadderEntryPair> source)
        {
            foreach(var pair in source)
            {
                var change = new LadderChange(pair.Cached,pair.Current);
                if(change.Changed())
                {
                    yield return change;
                }
            }
        }
        public static IEnumerable<LadderChange> BroadcastChange(this IEnumerable<LadderChange> source, Func<string,Task> sendMessage)
        {
            var options = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                
            foreach(var change in source)
            {
               var json = JsonConvert.SerializeObject(change, options);
                sendMessage(json);
                yield return change;
            }
        }
    }