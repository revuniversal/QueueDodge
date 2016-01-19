using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace QueueDodge.Migrations
{
    public partial class ladderstufftyeah : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LadderEntry",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClassID = table.Column<int>(nullable: false),
                    FactionID = table.Column<int>(nullable: false),
                    GenderID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RaceID = table.Column<int>(nullable: false),
                    Ranking = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    RealmID = table.Column<int>(nullable: false),
                    RealmName = table.Column<string>(nullable: true),
                    RealmSlug = table.Column<string>(nullable: true),
                    RequestID = table.Column<int>(nullable: false),
                    SeasonLosses = table.Column<int>(nullable: false),
                    SeasonWins = table.Column<int>(nullable: false),
                    SpecID = table.Column<int>(nullable: false),
                    WeeklyLosses = table.Column<int>(nullable: false),
                    WeeklyWins = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LadderEntry", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LadderEntry_BattleNetRequest_RequestID",
                        column: x => x.RequestID,
                        principalTable: "BattleNetRequest",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LadderEntry_RequestID",
                table: "LadderEntry",
                column: "RequestID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LadderEntry");
        }
    }
}
