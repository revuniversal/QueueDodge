using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using QueueDodge;

namespace QueueDodge.Migrations
{
    [DbContext(typeof(QueueDodgeDB))]
    [Migration("20160612072835_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901");

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

                    b.HasIndex("ClassID");

                    b.HasIndex("RaceID");

                    b.HasIndex("RealmID");

                    b.HasIndex("SpecializationID");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("QueueDodge.Class", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("Name");

                    b.Property<string>("PowerType");

                    b.HasKey("ID");

                    b.ToTable("Class");
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

                    b.HasIndex("CharacterID");

                    b.ToTable("LadderChangeModel");
                });

            modelBuilder.Entity("QueueDodge.Faction", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Faction");
                });

            modelBuilder.Entity("QueueDodge.Race", b =>
                {
                    b.Property<int>("ID");

                    b.Property<int>("FactionID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("FactionID");

                    b.ToTable("Race");
                });

            modelBuilder.Entity("QueueDodge.Realm", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("Name");

                    b.Property<int>("RegionID");

                    b.Property<string>("Slug");

                    b.HasKey("ID");

                    b.HasIndex("RegionID");

                    b.ToTable("Realm");
                });

            modelBuilder.Entity("QueueDodge.Region", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("QueueDodge.Specialization", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Specialization");
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
