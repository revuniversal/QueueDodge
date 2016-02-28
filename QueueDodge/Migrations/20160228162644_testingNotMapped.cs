using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace QueueDodge.Migrations
{
    public partial class testingNotMapped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_LadderEntry_WinLoss_SeasonID", table: "LadderEntry");
            migrationBuilder.DropForeignKey(name: "FK_LadderEntry_WinLoss_WeeklyID", table: "LadderEntry");
            migrationBuilder.DropColumn(name: "SeasonID", table: "LadderEntry");
            migrationBuilder.DropColumn(name: "WeeklyID", table: "LadderEntry");
            migrationBuilder.DropTable("WinLoss");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WinLoss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Losses = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinLoss", x => x.ID);
                });
            migrationBuilder.AddColumn<int>(
                name: "SeasonID",
                table: "LadderEntry",
                nullable: true);
            migrationBuilder.AddColumn<int>(
                name: "WeeklyID",
                table: "LadderEntry",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_LadderEntry_WinLoss_SeasonID",
                table: "LadderEntry",
                column: "SeasonID",
                principalTable: "WinLoss",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_LadderEntry_WinLoss_WeeklyID",
                table: "LadderEntry",
                column: "WeeklyID",
                principalTable: "WinLoss",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
