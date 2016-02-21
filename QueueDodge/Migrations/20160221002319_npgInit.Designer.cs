using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using QueueDodge;

namespace QueueDodge.Migrations
{
    [DbContext(typeof(QueueDodgeDB))]
    [Migration("20160221002319_npgInit")]
    partial class npgInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("QueueDodge.Character", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClassID");

                    b.Property<int>("Gender");

                    b.Property<string>("Name");

                    b.Property<int?>("RaceID");

                    b.Property<int?>("RealmID");

                    b.Property<int?>("SpecializationID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Class", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Faction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.LadderChange", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CurrentID");

                    b.Property<int?>("PreviousID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.LadderEntry", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bracket");

                    b.Property<int?>("CharacterID");

                    b.Property<int>("Ranking");

                    b.Property<int>("Rating");

                    b.Property<int?>("SeasonID");

                    b.Property<int?>("WeeklyID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Race", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FactionID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Realm", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("RegionID");

                    b.Property<string>("Slug");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Region", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.Specialization", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");
                });

            modelBuilder.Entity("QueueDodge.WinLoss", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Losses");

                    b.Property<int>("Wins");

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

            modelBuilder.Entity("QueueDodge.LadderChange", b =>
                {
                    b.HasOne("QueueDodge.LadderEntry")
                        .WithMany()
                        .HasForeignKey("CurrentID");

                    b.HasOne("QueueDodge.LadderEntry")
                        .WithMany()
                        .HasForeignKey("PreviousID");
                });

            modelBuilder.Entity("QueueDodge.LadderEntry", b =>
                {
                    b.HasOne("QueueDodge.Character")
                        .WithMany()
                        .HasForeignKey("CharacterID");

                    b.HasOne("QueueDodge.WinLoss")
                        .WithMany()
                        .HasForeignKey("SeasonID");

                    b.HasOne("QueueDodge.WinLoss")
                        .WithMany()
                        .HasForeignKey("WeeklyID");
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
