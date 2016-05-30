using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

using BattleDotSwag;
using BattleDotSwag.Services;
using BattleDotSwag.WoW.PVP;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using QueueDodge.Data;

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

            var activity = Dedupe(leaderboard.Rows, processed);
            var entries = ConvertToLadderEntry(activity, bracket, region);
            var cachedEntries = ExistsInCache(entries, cache);
            var changes = CharacterChanged(cachedEntries);

            foreach (var change in changes)
            {
                var change2 = await SaveRealm(change, queueDodge);
                var change3 = await SaveCharacter(change2, queueDodge);
                var fullChange = await SaveLadderChange(change3, queueDodge);

                await NotifyChanged(fullChange, sendMessage);
            }

            return changes.ToList();
        }
        public async Task<Leaderboard> GetActivity(string bracket, Locale locale, BattleDotSwag.Region region)
        {
            var service = new BattleNetService<Leaderboard>();
            var endpoint = new LeaderboardEndpoint(bracket, locale, apiKey);
            var leaderboard = await service.Get(endpoint, region);
            return leaderboard;
        }

        public IEnumerable<Row> Dedupe(IEnumerable<Row> source, IList<Row> tracking)
        {
            var distinctRows = source
            .Where(p => !tracking
                        .Where(o => o.Name == p.Name && o.RealmID == p.RealmID)
                        .Any());

            foreach (var row in distinctRows)
            {
                tracking.Add(row);
                yield return row;
            }

        }
        public IEnumerable<LadderEntry> ConvertToLadderEntry(IEnumerable<Row> source, string bracket, BattleDotSwag.Region region)
        {
            var factory = new LadderEntryFactory();

            foreach (var row in source)
            {
                var _region = new Region((int)region, region.ToString());
                var ladderEntry = factory.Create(row, bracket, _region);
                yield return ladderEntry;
            }
        }
        public IEnumerable<LadderEntry> CacheLadder(IEnumerable<LadderEntry> source, IMemoryCache cache, string bracket, BattleDotSwag.Region region)
        {
            var key = $"{bracket}:{region}";
            cache.Set(key, source);
            return source;
        }
        public IEnumerable<LadderEntryPair> ExistsInCache(IEnumerable<LadderEntry> source, IMemoryCache cache)
        {
            foreach (var entry in source)
            {
                var realKey = entry.Character.Name + ":" + entry.Character.RealmID + ":" + entry.Bracket;

                var cachedEntry = default(LadderEntry);
                var cached = cache.TryGetValue(realKey, out cachedEntry);
                cache.Set(realKey, entry);

                if (cached)
                {
                    var pair = new LadderEntryPair(entry, cachedEntry);
                    yield return pair;
                }
            }
        }
        public IEnumerable<LadderChange> CharacterChanged(IEnumerable<LadderEntryPair> source)
        {
            foreach (var pair in source)
            {
                var change = new LadderChange(pair.Cached, pair.Current);
                if (change.Changed())
                {
                    yield return change;
                }
            }
        }

        public async Task<LadderChange> NotifyChanged(LadderChange change, Func<string, Task> sendMessage)
        {
            var options = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            var json = JsonConvert.SerializeObject(change, options);
            await sendMessage(json);
            return change;

        }
        public async Task<LadderChange> SaveRealm(LadderChange change, QueueDodgeDB queueDodge)
        {
            var realmExists = await queueDodge
                .Realms
                .AnyAsync(p => p.ID == change.Current.Character.RealmID);

            if (!realmExists)
            {
                var realm = new Realm(change.Current.Character.RealmID,
                    change.Current.Character.Realm.Name,
                    change.Current.Character.Realm.Slug,
                    change.Current.Character.Realm.RegionID);

                var trackedRealm = queueDodge.Realms.Add(realm);
                await queueDodge.SaveChangesAsync();
            }

            return change;

        }
        public async Task<LadderChange> SaveCharacter(LadderChange change, QueueDodgeDB queueDodge)
        {
            var characterExists = await queueDodge
                .Characters
                .AnyAsync(p => p.Name == change.Current.Character.Name
                && p.RealmID == change.Current.Character.RealmID);

            if (!characterExists)
            {

                queueDodge.Entry(change.Current.Character).State = EntityState.Added;

                //var attachedCharacter = queueDodge.Add(change.Current.Character).Entity;
                await queueDodge.SaveChangesAsync();
            }

            var character = await queueDodge
                .Characters
                .Include(p => p.Class)
                .Include(p => p.Realm)
                .Include(p => p.Specialization)
                .Include(p => p.Race)
                .Include(p => p.Realm.Region)
                .Include(p => p.Race.Faction)
                .Where(p => p.Name == change.Current.Character.Name &&
                p.RealmID == change.Current.Character.RealmID)
                .SingleAsync();

            change.Current.Character = character;
            change.Previous.Character = character;

            return change;

        }
        public async Task<LadderChange> SaveLadderChange(LadderChange change, QueueDodgeDB queueDodge)
        {
            var changeModel = new LadderChangeModel(change);
            changeModel.CharacterID = change.Current.Character.ID;
            queueDodge.LadderChanges.Add(changeModel);
            await queueDodge.SaveChangesAsync();
            return change;
        }
    }
}
