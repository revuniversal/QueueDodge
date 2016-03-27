using Microsoft.Extensions.OptionsModel;
using System.Collections.Generic;
using System.Linq;
using BattleDotSwag;
using BattleDotSwag.Services;
using BattleDotSwag.WoW.PVP;

namespace QueueDodge.Data
{
    public class QueueDodgeSeed
    {
        private QueueDodgeOptions options;
        private QueueDodgeDB queueDodge;

        public QueueDodgeSeed(QueueDodgeDB queueDodge, IOptions<QueueDodgeOptions> options)
        {
            this.queueDodge = queueDodge;
            this.options = options.Value;
        }

        public void Seed()
        {
            Clear();
            Regions();
            Factions();
            Races();
            Classes();
            Specializations();
            Realms();
        }
        private void Clear()
        {
            queueDodge.LadderChanges.Clear();
            queueDodge.Characters.Clear();
            queueDodge.Regions.Clear();
            queueDodge.Classes.Clear();
            queueDodge.Specializations.Clear();
            queueDodge.Races.Clear();
            queueDodge.Factions.Clear();
            queueDodge.Realms.Clear();

            queueDodge.SaveChanges();
        }

        private void Regions()
        {
            queueDodge.Regions.AddRange(
                new Region(1, "us"),
                new Region(2, "eu"),
                new Region(3, "kr"),
                new Region(4, "tw"),
                new Region(5, "cn")
                );
            queueDodge.SaveChanges();
        }
        private void Factions()
        {
            queueDodge.Factions.AddRange(
                new Faction(1, "alliance"),
                new Faction(2, "horde"),
                new Faction(3, "neutral")
                );
        }
        private void Races()
        {
            queueDodge.Races.AddRange(
                new Race(1, "human", 1),
                new Race(2, "orc", 2),
                new Race(3, "dwarf", 1),
                new Race(4, "night elf", 1),
                new Race(5, "undead", 2),
                new Race(6, "tauren", 2),
                new Race(7, "gnome", 1),
                new Race(8, "troll", 2),
                new Race(9, "goblin", 2),
                new Race(10, "blood elf", 2),
                new Race(11, "draenei", 1),
                new Race(22, "worgen", 1),
                new Race(24, "pandaren", 3),
                new Race(25, "pandaren", 1),
                new Race(26, "pandaren", 2)
                );
            queueDodge.SaveChanges();
        }
        private void Realms()
        {
            var service = new BattleNetService<Leaderboard>();
            var endpoint = new LeaderboardEndpoint("3v3", Locale.en_us, options.apiKey);
            var data = service.Get(endpoint, BattleDotSwag.Region.us).Result;
            var region = queueDodge.Regions.Where(p => p.ID == 1).Single();

            var realms = new List<Realm>();

            foreach (var row in data.Rows)
            {
                var newRealm = realms.Where(p => p.ID == row.RealmID).Count() == 0;
               
                if (newRealm && row.RealmID > 0)
                {
                    var realm = new Realm(row.RealmID, row.RealmName, row.RealmSlug, region.ID);
                    realms.Add(realm);
                }
            }

            queueDodge.Realms.AddRange(realms);
            queueDodge.SaveChanges();
        }
        private void Specializations()
        {
            queueDodge.Specializations.AddRange(
                new Specialization(62, "arcane"),
                new Specialization(63, "fire"),
                new Specialization(64, "frost"),
                new Specialization(65, "holy"),
                new Specialization(66, "protection"),
                new Specialization(70, "retribution"),
                new Specialization(71, "arms"),
                new Specialization(72, "fury"),
                new Specialization(73, "protection"),
                new Specialization(102, "balance"),
                new Specialization(103, "feral"),
                new Specialization(104, "guardian"),
                new Specialization(105, "restoration"),
                new Specialization(250, "blood"),
                new Specialization(251, "frost"),
                new Specialization(252, "unholy"),
                new Specialization(253, "beast mastery"),
                new Specialization(254, "marksmanship"),
                new Specialization(255, "survival"),
                new Specialization(256, "discipline"),
                new Specialization(257, "holy"),
                new Specialization(258, "shadow"),
                new Specialization(259, "assassination"),
                new Specialization(260, "combat"),
                new Specialization(261, "subtlety"),
                new Specialization(262, "elemental"),
                new Specialization(263, "enhancement"),
                new Specialization(264, "restoration"),
                new Specialization(265, "affliction"),
                new Specialization(266, "demonology"),
                new Specialization(267, "destruction"),
                new Specialization(268, "brewmaster"),
                new Specialization(269, "windwalker"),
                new Specialization(270, "mistweaver")
                );
            queueDodge.SaveChanges();
        }
        private void Classes()
        {
            queueDodge.Classes.AddRange(
                new Class(1, "warrior", "rage"),
                new Class(2, "paladin", "mana"),
                new Class(3, "hunter", "focus"),
                new Class(4, "rogue", "energy"),
                new Class(5, "priest", "mana"),
                new Class(6, "death knight", "runic-power"),
                new Class(7, "shaman", "mana"),
                new Class(8, "mage", "mana"),
                new Class(9, "warlock", "mana"),
                new Class(10, "monk", "energy"),
                new Class(11, "druid", "mana")
                );

            queueDodge.SaveChanges();
        }
    }

}

