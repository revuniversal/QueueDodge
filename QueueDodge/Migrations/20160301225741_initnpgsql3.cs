using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace QueueDodge.Migrations
{
    public partial class initnpgsql3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PowerType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.ID);
                });
            migrationBuilder.CreateTable(
                name: "Faction",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faction", x => x.ID);
                });
            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.ID);
                });
            migrationBuilder.CreateTable(
                name: "Specialization",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialization", x => x.ID);
                });
            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    FactionID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Race_Faction_FactionID",
                        column: x => x.FactionID,
                        principalTable: "Faction",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Realm",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Name = table.Column<string>(nullable: true),
                    RegionID = table.Column<int>(nullable: false),
                    Slug = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Realm", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Realm_Region_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Region",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    ClassID = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RaceID = table.Column<int>(nullable: false),
                    RealmID = table.Column<int>(nullable: false),
                    SpecializationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Character_Class_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Class",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Character_Race_RaceID",
                        column: x => x.RaceID,
                        principalTable: "Race",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Character_Realm_RealmID",
                        column: x => x.RealmID,
                        principalTable: "Realm",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Character_Specialization_SpecializationID",
                        column: x => x.SpecializationID,
                        principalTable: "Specialization",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "LadderChangeModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Bracket = table.Column<string>(nullable: true),
                    CharacterID = table.Column<int>(nullable: false),
                    CurrentRanking = table.Column<int>(nullable: false),
                    CurrentRating = table.Column<int>(nullable: false),
                    CurrentSeasonLosses = table.Column<int>(nullable: false),
                    CurrentSeasonWins = table.Column<int>(nullable: false),
                    CurrentWeeklyLosses = table.Column<int>(nullable: false),
                    CurrentWeeklyWins = table.Column<int>(nullable: false),
                    PreviousRanking = table.Column<int>(nullable: false),
                    PreviousRating = table.Column<int>(nullable: false),
                    PreviousSeasonLosses = table.Column<int>(nullable: false),
                    PreviousSeasonWins = table.Column<int>(nullable: false),
                    PreviousWeeklyLosses = table.Column<int>(nullable: false),
                    PreviousWeeklyWins = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LadderChangeModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LadderChangeModel_Character_CharacterID",
                        column: x => x.CharacterID,
                        principalTable: "Character",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("LadderChangeModel");
            migrationBuilder.DropTable("Character");
            migrationBuilder.DropTable("Class");
            migrationBuilder.DropTable("Race");
            migrationBuilder.DropTable("Realm");
            migrationBuilder.DropTable("Specialization");
            migrationBuilder.DropTable("Faction");
            migrationBuilder.DropTable("Region");
        }
    }
}
