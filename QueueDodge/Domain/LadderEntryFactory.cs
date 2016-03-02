using BattleDotSwag.WoW.PVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueDodge.Domain
{
    public class LadderEntryFactory
    {
        private QueueDodgeDB queueDodge;

        public LadderEntryFactory(QueueDodgeDB queueDodge)
        {
            this.queueDodge = queueDodge;
        }

        public LadderEntry Create(Row row, string bracket, Region region)
        {
            var realm = new Realm(row.RealmID, row.RealmName, row.RealmSlug, region.ID);
            var character = new Character(row.Name, row.GenderID, row.RealmID, row.RaceID, row.ClassID, row.SpecID);
            character.Realm = realm;

            var entry = new LadderEntry(character, bracket, row.Ranking, row.Rating, row.SeasonWins, row.SeasonLosses, row.WeeklyWins, row.WeeklyLosses);
            return entry;
        }
    }
}
