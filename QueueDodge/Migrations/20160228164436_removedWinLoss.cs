using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace QueueDodge.Migrations
{
    public partial class removedWinLoss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeasonLosses",
                table: "LadderEntry",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "SeasonWins",
                table: "LadderEntry",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "WeeklyLosses",
                table: "LadderEntry",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "WeeklyWins",
                table: "LadderEntry",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "SeasonLosses", table: "LadderEntry");
            migrationBuilder.DropColumn(name: "SeasonWins", table: "LadderEntry");
            migrationBuilder.DropColumn(name: "WeeklyLosses", table: "LadderEntry");
            migrationBuilder.DropColumn(name: "WeeklyWins", table: "LadderEntry");
        }
    }
}
