using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using QueueDodge;

namespace QueueDodge.Migrations
{
    [DbContext(typeof(QueueDodgeDB))]
    [Migration("20160120061946_omgwhoknows")]
    partial class omgwhoknows
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc2-16649")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QueueDodge.Models.BattleNetRequest", b =>
                {
                    b.ToTable("BattleNetRequest");

                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bracket");

                    b.Property<double>("Duration");

                    b.Property<string>("Locale");

                    b.Property<int>("RegionID");

                    b.Property<DateTime>("RequestDate");

                    b.Property<string>("RequestType");

                    b.Property<string>("Url");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Models.Character", b =>
                {
                    b.ToTable("Character");

                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CharacterName");

                    b.Property<int>("ClassID");

                    b.Property<int>("FactionID");

                    b.Property<int>("Gender");

                    b.Property<int>("RaceID");

                    b.Property<int>("RealmID");

                    b.Property<int>("RegionID");

                    b.Property<int>("SpecID");

                    b.HasKey("ID");

                    b.HasIndex("ClassID");

                    b.HasIndex("FactionID");

                    b.HasIndex("RaceID");

                    b.HasIndex("RealmID");

                    b.HasIndex("RegionID");

                    b.HasIndex("SpecID");
                });

            modelBuilder.Entity("QueueDodge.Models.Class", b =>
                {
                    b.ToTable("Class");

                    b.Property<int>("ID");

                    b.Property<string>("Name");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Models.Faction", b =>
                {
                    b.ToTable("Faction");

                    b.Property<int>("ID");

                    b.Property<string>("Name");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Models.LadderChange", b =>
                {
                    b.ToTable("LadderChange");

                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bracket");

                    b.Property<int>("CurrentRequestID");

                    b.Property<int>("DetectedClass");

                    b.Property<int>("DetectedFaction");

                    b.Property<int>("DetectedGenderID");

                    b.Property<int>("DetectedRace");

                    b.Property<int>("DetectedRanking");

                    b.Property<int>("DetectedRating");

                    b.Property<int>("DetectedSeasonLosses");

                    b.Property<int>("DetectedSeasonWins");

                    b.Property<int>("DetectedSpec");

                    b.Property<int>("DetectedWeeklyLosses");

                    b.Property<int>("DetectedWeeklyWins");

                    b.Property<string>("Name");

                    b.Property<int>("PreviousClass");

                    b.Property<int>("PreviousFaction");

                    b.Property<int>("PreviousGenderID");

                    b.Property<int>("PreviousRace");

                    b.Property<int>("PreviousRanking");

                    b.Property<int>("PreviousRating");

                    b.Property<int>("PreviousRequestID");

                    b.Property<int>("PreviousSeasonLosses");

                    b.Property<int>("PreviousSeasonWins");

                    b.Property<int>("PreviousSpec");

                    b.Property<int>("PreviousWeeklyLosses");

                    b.Property<int>("PreviousWeeklyWins");

                    b.Property<int?>("RealmID");

                    b.HasKey("ID");

                    b.HasIndex("CurrentRequestID");

                    b.HasIndex("PreviousRequestID");

                    b.HasIndex("RealmID");
                });

            modelBuilder.Entity("QueueDodge.Models.Race", b =>
                {
                    b.ToTable("Race");

                    b.Property<int>("ID");

                    b.Property<int>("FactionID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("FactionID");
                });

            modelBuilder.Entity("QueueDodge.Models.Realm", b =>
                {
                    b.ToTable("Realm");

                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("RegionID");

                    b.Property<string>("Slug");

                    b.HasKey("ID");

                    b.HasIndex("RegionID");
                });

            modelBuilder.Entity("QueueDodge.Models.Region", b =>
                {
                    b.ToTable("Region");

                    b.Property<int>("ID");

                    b.Property<string>("Name");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Models.Specialization", b =>
                {
                    b.ToTable("Specialization");

                    b.Property<int>("ID");

                    b.Property<int>("ClassID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("ClassID");
                });

            modelBuilder.Entity("QueueDodge.Models.Character", b =>
                {
                    b.HasOne("QueueDodge.Models.Class")
                        .WithMany()
                        .HasForeignKey("ClassID");

                    b.HasOne("QueueDodge.Models.Faction")
                        .WithMany()
                        .HasForeignKey("FactionID");

                    b.HasOne("QueueDodge.Models.Race")
                        .WithMany()
                        .HasForeignKey("RaceID");

                    b.HasOne("QueueDodge.Models.Realm")
                        .WithMany()
                        .HasForeignKey("RealmID");

                    b.HasOne("QueueDodge.Models.Region")
                        .WithMany()
                        .HasForeignKey("RegionID");

                    b.HasOne("QueueDodge.Models.Specialization")
                        .WithMany()
                        .HasForeignKey("SpecID");
                });

            modelBuilder.Entity("QueueDodge.Models.LadderChange", b =>
                {
                    b.HasOne("QueueDodge.Models.BattleNetRequest")
                        .WithMany()
                        .HasForeignKey("CurrentRequestID");

                    b.HasOne("QueueDodge.Models.BattleNetRequest")
                        .WithMany()
                        .HasForeignKey("PreviousRequestID");

                    b.HasOne("QueueDodge.Models.Realm")
                        .WithMany()
                        .HasForeignKey("RealmID");
                });

            modelBuilder.Entity("QueueDodge.Models.Race", b =>
                {
                    b.HasOne("QueueDodge.Models.Faction")
                        .WithMany()
                        .HasForeignKey("FactionID");
                });

            modelBuilder.Entity("QueueDodge.Models.Realm", b =>
                {
                    b.HasOne("QueueDodge.Models.Region")
                        .WithMany()
                        .HasForeignKey("RegionID");
                });

            modelBuilder.Entity("QueueDodge.Models.Specialization", b =>
                {
                    b.HasOne("QueueDodge.Models.Class")
                        .WithMany()
                        .HasForeignKey("ClassID");
                });
        }
    }
}
