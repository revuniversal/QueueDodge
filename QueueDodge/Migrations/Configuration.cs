
//namespace QueueDodge.Migrations
//{
//    internal sealed class Configuration : DbMigrationsConfiguration<QueueDodgeDB>
//    {
//        public Configuration()
//        {
//            AutomaticMigrationsEnabled = false;
//        }

//        //protected override void Seed(QueueDodgeDB context)
//        //{
//            //  This method will be called after migrating to the latest version.

//            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
//            //  to avoid creating duplicate seed data. E.g.
//            //
//            //    context.People.AddOrUpdate(
//            //      p => p.FullName,
//            //      new Person { FullName = "Andrew Peters" },
//            //      new Person { FullName = "Brice Lambson" },
//            //      new Person { FullName = "Rowan Miller" }
//            //    );
//            //

//            //context.Regions.AddOrUpdate(p => p.ID,
//            //    new Region() { ID = 0, Name = "us" },
//            //    new Region() { ID = 1, Name = "eu" },
//            //    new Region() { ID = 999, Name = "all" }
//            //    );

//            //#region factions

//            //context.Factions.AddOrUpdate(p => p.ID,
//            //    new Faction()
//            //    {
//            //        ID = 0,
//            //        Name = "Alliance"
//            //    });

//            //context.Factions.AddOrUpdate(p => p.ID,
//            //    new Faction()
//            //    {
//            //        ID = 1,
//            //        Name = "Horde"
//            //    });

//            //context.Factions.AddOrUpdate(p => p.ID, new Faction()
//            //{
//            //    ID = 2,
//            //    Name = "Neutral"
//            //});
//            //#endregion

//            //#region races

//            //// Alliance
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 1,
//            //    FactionID = 0,
//            //    Name = "Human"
//            //});
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 3,
//            //    FactionID = 0,
//            //    Name = "Dwarf"
//            //});
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 4,
//            //    FactionID = 0,
//            //    Name = "Night Elf"
//            //});
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 7,
//            //    FactionID = 0,
//            //    Name = "Gnome"
//            //});
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 11,
//            //    FactionID = 0,
//            //    Name = "Draenei"
//            //});
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 22,
//            //    FactionID = 0,
//            //    Name = "Worgen"
//            //});
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 24,
//            //    FactionID = 0,
//            //    Name = "Pandaren"
//            //});

//            //// Horde
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 2,
//            //    FactionID = 1,
//            //    Name = "Orc"
//            //});
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 5,
//            //    FactionID = 1,
//            //    Name = "Undead"
//            //});
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 6,
//            //    FactionID = 1,
//            //    Name = "Tauren"
//            //});
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 8,
//            //    FactionID = 1,
//            //    Name = "Troll"
//            //});
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 9,
//            //    FactionID = 1,
//            //    Name = "Goblin"
//            //});
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 10,
//            //    FactionID = 1,
//            //    Name = "Blood Elf"
//            //});
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 26,
//            //    FactionID = 1,
//            //    Name = "Pandaren"
//            //});

//            //// Neutral
//            //context.Races.AddOrUpdate(p => p.ID, new Race()
//            //{
//            //    ID = 25,
//            //    FactionID = 2,
//            //    Name = "Pandaren"
//            //});
//            //#endregion

//            //#region classes

//            //context.Classes.AddOrUpdate(p => p.ID, new Class()
//            //{
//            //    ID = 1,
//            //    Name = "Warrior"
//            //});
//            //context.Classes.AddOrUpdate(p => p.ID, new Class()
//            //{
//            //    ID = 2,
//            //    Name = "Paladin"
//            //});
//            //context.Classes.AddOrUpdate(p => p.ID, new Class()
//            //{
//            //    ID = 3,
//            //    Name = "Hunter"
//            //});
//            //context.Classes.AddOrUpdate(p => p.ID, new Class()
//            //{
//            //    ID = 4,
//            //    Name = "Rogue"
//            //});
//            //context.Classes.AddOrUpdate(p => p.ID, new Class()
//            //{
//            //    ID = 5,
//            //    Name = "Priest"
//            //});
//            //context.Classes.AddOrUpdate(p => p.ID, new Class()
//            //{
//            //    ID = 6,
//            //    Name = "Death Knight"
//            //});
//            //context.Classes.AddOrUpdate(p => p.ID, new Class()
//            //{
//            //    ID = 7,
//            //    Name = "Shaman"
//            //});
//            //context.Classes.AddOrUpdate(p => p.ID, new Class()
//            //{
//            //    ID = 8,
//            //    Name = "Mage"
//            //});
//            //context.Classes.AddOrUpdate(p => p.ID, new Class()
//            //{
//            //    ID = 9,
//            //    Name = "Warlock"
//            //});
//            //context.Classes.AddOrUpdate(p => p.ID, new Class()
//            //{
//            //    ID = 10,
//            //    Name = "Monk"
//            //});
//            //context.Classes.AddOrUpdate(p => p.ID, new Class()
//            //{
//            //    ID = 11,
//            //    Name = "Druid"
//            //});
//            //#endregion

//            //#region specializations


//            //// Mage
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 62,
//            //    ClassID = 8,
//            //    Name = "Arcane"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 63,
//            //    ClassID = 8,
//            //    Name = "Fire"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 64,
//            //    ClassID = 8,
//            //    Name = "Frost"
//            //});

//            //// Paladin
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 65,
//            //    ClassID = 2,
//            //    Name = "Holy"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 66,
//            //    ClassID = 2,
//            //    Name = "Protection"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 70,
//            //    ClassID = 2,
//            //    Name = "Retribution"
//            //});

//            //// Warrior
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 71,
//            //    ClassID = 1,
//            //    Name = "Arms"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 72,
//            //    ClassID = 1,
//            //    Name = "Fury"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 73,
//            //    ClassID = 1,
//            //    Name = "Protection"
//            //});

//            //// Druid
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 102,
//            //    ClassID = 11,
//            //    Name = "Balance"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 103,
//            //    ClassID = 11,
//            //    Name = "Guardian"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 104,
//            //    ClassID = 11,
//            //    Name = "Feral"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 105,
//            //    ClassID = 11,
//            //    Name = "Restoration"
//            //});

//            //// Death Knight
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 250,
//            //    ClassID = 6,
//            //    Name = "Blood"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 251,
//            //    ClassID = 6,
//            //    Name = "Frost"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 252,
//            //    ClassID = 6,
//            //    Name = "Unholy"
//            //});

//            //// Hunter
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 253,
//            //    ClassID = 3,
//            //    Name = "Beast Mastery"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 254,
//            //    ClassID = 3,
//            //    Name = "Marksmanship"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 255,
//            //    ClassID = 3,
//            //    Name = "Survival"
//            //});

//            //// Priest
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 256,
//            //    ClassID = 5,
//            //    Name = "Discipline"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 257,
//            //    ClassID = 5,
//            //    Name = "Holy"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 258,
//            //    ClassID = 5,
//            //    Name = "Shadow"
//            //});

//            //// Rogue
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 259,
//            //    ClassID = 4,
//            //    Name = "Assassination"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 260,
//            //    ClassID = 4,
//            //    Name = "Combat"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 261,
//            //    ClassID = 4,
//            //    Name = "Subtlety"
//            //});

//            //// Shaman
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 262,
//            //    ClassID = 7,
//            //    Name = "Elemental"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 263,
//            //    ClassID = 7,
//            //    Name = "Enhancement"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 264,
//            //    ClassID = 7,
//            //    Name = "Restoration"
//            //});

//            //// Warlock
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 265,
//            //    ClassID = 9,
//            //    Name = "Affliction"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 266,
//            //    ClassID = 9,
//            //    Name = "Demonology"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 267,
//            //    ClassID = 9,
//            //    Name = "Destruction"
//            //});

//            //// Monk
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 268,
//            //    ClassID = 10,
//            //    Name = "Brewmaster"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 269,
//            //    ClassID = 10,
//            //    Name = "Windwalker"
//            //});
//            //context.Specializations.AddOrUpdate(p => p.ID, new Specialization()
//            //{
//            //    ID = 270,
//            //    ClassID = 10,
//            //    Name = "Mistweaver"
//            //});
//            //#endregion  

//        //}

//    }
//}
