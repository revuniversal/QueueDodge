using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using QueueDodge.Data;
using BattleDotSwag;
using BattleDotSwag.Services;
using BattleDotSwag.WoW.PVP;
using Microsoft.Data.Entity;
using System.Linq;
using QueueDodge.Domain;

namespace QueueDodge
{
    public class Ladder
    {
        private string apiKey;
        private QueueDodgeDB queueDodge;
        private IMemoryCache cache;
        private Func<string, Task> sendMessage;

        // TODO:  This is too sketchy.
        public Ladder(string apiKey, QueueDodgeDB queueDodge, IMemoryCache cache, Func<string, Task> sendMessage)
        {
            this.apiKey = apiKey;
            this.queueDodge = queueDodge;
            this.cache = cache;
            this.sendMessage = sendMessage;
        }

        // TODO:  Some side effects are happening in here.
        public void DetectChanges(string bracket, Locale locale, BattleDotSwag.Region region)
        {
            var leaderboard = GetActivity(bracket, locale, region);

            // HACK:  Zyrith is appearing multiple times on the same ladder.  Keep track of who is processed and dont process the same character more than once.
            var processed = new List<Row>();

            foreach (var row in leaderboard.Rows)
            {
                var multiple = processed
                    .Where(p => p.Name == row.Name && p.RealmID == row.RealmID)
                    .Any();

                if (!multiple)
                {
                    processed.Add(row);
                    var entry = ConvertToLadderEntry(row, bracket, region);
                    var cachedEntry = Cached(entry);

                    if (cachedEntry != null)
                    {
                        var change = new LadderChange(cachedEntry, entry);
                        if (change.Changed())
                        {
                            var realm = AddOrUpdateRealm(change);
                            var character = AddOrUpdateCharacter(change);
                            var changeModel = new LadderChangeModel(change);
                            changeModel.CharacterID = character.ID;
                            queueDodge.LadderChanges.Add(changeModel, GraphBehavior.SingleObject);

                            // HACK:  This will interfere with detecting race and faction changes.
                            change.Current.Character = character;
                            change.Previous.Character = character;

                            BroadcastChange(change);
                        }
                    };
                    queueDodge.SaveChanges();
                }
            }
        }

        private Leaderboard GetActivity(string bracket, Locale locale, BattleDotSwag.Region region)
        {
            var service = new BattleNetService<Leaderboard>();
            var endpoint = new LeaderboardEndpoint(bracket, locale, apiKey);
            var leaderboard = service.Get(endpoint, region).Result;
            return leaderboard;
        }

        public LadderEntry ConvertToLadderEntry(Row row, string bracket, BattleDotSwag.Region region)
        {
            var _region = new Region((int)region, region.ToString());
            var factory = new LadderEntryFactory(queueDodge);
            var ladderEntry = factory.Create(row, bracket, _region);
            return ladderEntry;
        }

        private LadderEntry Cached(LadderEntry entry)
        {
            var realKey = entry.Character.Name + ":" + entry.Character.RealmID + ":" + entry.Bracket;

            var cachedEntry = default(LadderEntry);
            var cached = cache.TryGetValue(realKey, out cachedEntry);
            cache.Set(realKey, entry);

            if (cached)
            {
                return cachedEntry;
            }
            else {
                return null;
            }


        }

        private void BroadcastChange(LadderChange change)
        {
            var options = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(change, options);
            sendMessage(json);
        }
        private Realm AddOrUpdateRealm(LadderChange change)
        {
            var realmCheck = queueDodge
                .Realms
                .Where(p => p.ID == change.Current.Character.RealmID)
                .FirstOrDefault();

            if (realmCheck == null)
            {
                var realm = new Realm(change.Current.Character.RealmID,
                    change.Current.Character.Realm.Name,
                    change.Current.Character.Realm.Slug,
                    change.Current.Character.Realm.RegionID);

                var trackedRealm = queueDodge.Realms.Add(realm);
                queueDodge.SaveChanges();
                return trackedRealm.Entity;
            }
            else
            {
                return realmCheck;
            }
        }
        private Character AddOrUpdateCharacter(LadderChange change)
        {
            var characterCheck = queueDodge
                .Characters
                .Where(p => p.Name == change.Current.Character.Name
                && p.RealmID == change.Current.Character.RealmID)
                .FirstOrDefault();

            if (characterCheck == null)
            {
                var character = new Character(change.Current.Character.Name,
                    change.Current.Character.Gender,
                    change.Current.Character.RealmID,
                    change.Current.Character.RaceID,
                    change.Current.Character.ClassID,
                    change.Current.Character.SpecializationID);

                var attachedCharacter = queueDodge.Add(character).Entity;
                queueDodge.SaveChanges();
            }

            // HACK:  Totally lame, child objects aren't populated so I have to re-query what I just inserted.
            var filledCharacter = queueDodge
                .Characters
                .Include(p => p.Class)
                .Include(p => p.Realm)
                .Include(p => p.Specialization)
                .Include(p => p.Race)
                .Include(p => p.Realm.Region)
                .Include(p => p.Race.Faction)
                .Where(p => p.Name == change.Current.Character.Name &&
                p.RealmID == change.Current.Character.RealmID)
                .Single();

            return filledCharacter;
        }
    }
}
