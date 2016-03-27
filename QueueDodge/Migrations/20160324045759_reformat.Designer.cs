using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using QueueDodge;

namespace QueueDodge.Migrations
{
    [DbContext(typeof(QueueDodgeDB))]
    [Migration("20160324045759_reformat")]
    partial class reformat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("QueueDodge.Character", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClassID");

                    b.Property<int>("Gender");

                    b.Property<string>("Name");

                    b.Property<int>("RaceID");

                    b.Property<int>("RealmID");

                    b.Property<int>("SpecializationID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Class", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("Name");

                    b.Property<string>("PowerType");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Data.LadderChangeModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bracket");

                    b.Property<int>("CharacterID");

                    b.Property<int>("CurrentRanking");

                    b.Property<int>("CurrentRating");

                    b.Property<int>("CurrentSeasonLosses");

                    b.Property<int>("CurrentSeasonWins");

                    b.Property<int>("CurrentWeeklyLosses");

                    b.Property<int>("CurrentWeeklyWins");

                    b.Property<int>("PreviousRanking");

                    b.Property<int>("PreviousRating");

                    b.Property<int>("PreviousSeasonLosses");

                    b.Property<int>("PreviousSeasonWins");

                    b.Property<int>("PreviousWeeklyLosses");

                    b.Property<int>("PreviousWeeklyWins");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Faction", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("Name");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Race", b =>
                {
                    b.Property<int>("ID");

                    b.Property<int>("FactionID");

                    b.Property<string>("Name");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Realm", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("Name");

                    b.Property<int>("RegionID");

                    b.Property<string>("Slug");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Region", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("Name");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Specialization", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("Name");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Character", b =>
                {
                    b.HasOne("QueueDodge.Class")
                        .WithMany()
                        .HasForeignKey("ClassID");

                    b.HasOne("QueueDodge.Race")
                        .WithMany()
                        .HasForeignKey("RaceID");

                    b.HasOne("QueueDodge.Realm")
                        .WithMany()
                        .HasForeignKey("RealmID");

                    b.HasOne("QueueDodge.Specialization")
                        .WithMany()
                        .HasForeignKey("SpecializationID");
                });

            modelBuilder.Entity("QueueDodge.Data.LadderChangeModel", b =>
                {
                    b.HasOne("QueueDodge.Character")
                        .WithMany()
                        .HasForeignKey("CharacterID");
                });

            modelBuilder.Entity("QueueDodge.Race", b =>
                {
                    b.HasOne("QueueDodge.Faction")
                        .WithMany()
                        .HasForeignKey("FactionID");
                });

            modelBuilder.Entity("QueueDodge.Realm", b =>
                {
                    b.HasOne("QueueDodge.Region")
                        .WithMany()
                        .HasForeignKey("RegionID");
                });
        }
    }
}
