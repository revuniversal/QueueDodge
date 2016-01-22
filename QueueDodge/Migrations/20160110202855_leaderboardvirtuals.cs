using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace QueueDodge.Migrations
{
    public partial class leaderboardvirtuals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Leaderboard_ClassID",
                table: "Leaderboard",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Leaderboard_FactionID",
                table: "Leaderboard",
                column: "FactionID");

            migrationBuilder.CreateIndex(
                name: "IX_Leaderboard_RaceID",
                table: "Leaderboard",
                column: "RaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Leaderboard_RealmID",
                table: "Leaderboard",
                column: "RealmID");

            migrationBuilder.CreateIndex(
                name: "IX_Leaderboard_RegionID",
                table: "Leaderboard",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Leaderboard_RequestID",
                table: "Leaderboard",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_Leaderboard_SpecID",
                table: "Leaderboard",
                column: "SpecID");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaderboard_Class_ClassID",
                table: "Leaderboard",
                column: "ClassID",
                principalTable: "Class",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leaderboard_Faction_FactionID",
                table: "Leaderboard",
                column: "FactionID",
                principalTable: "Faction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leaderboard_Race_RaceID",
                table: "Leaderboard",
                column: "RaceID",
                principalTable: "Race",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leaderboard_Realm_RealmID",
                table: "Leaderboard",
                column: "RealmID",
                principalTable: "Realm",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leaderboard_Region_RegionID",
                table: "Leaderboard",
                column: "RegionID",
                principalTable: "Region",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leaderboard_BattleNetRequest_RequestID",
                table: "Leaderboard",
                column: "RequestID",
                principalTable: "BattleNetRequest",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leaderboard_Specialization_SpecID",
                table: "Leaderboard",
                column: "SpecID",
                principalTable: "Specialization",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leaderboard_Class_ClassID",
                table: "Leaderboard");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaderboard_Faction_FactionID",
                table: "Leaderboard");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaderboard_Race_RaceID",
                table: "Leaderboard");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaderboard_Realm_RealmID",
                table: "Leaderboard");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaderboard_Region_RegionID",
                table: "Leaderboard");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaderboard_BattleNetRequest_RequestID",
                table: "Leaderboard");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaderboard_Specialization_SpecID",
                table: "Leaderboard");

            migrationBuilder.DropIndex(
                name: "IX_Leaderboard_ClassID",
                table: "Leaderboard");

            migrationBuilder.DropIndex(
                name: "IX_Leaderboard_FactionID",
                table: "Leaderboard");

            migrationBuilder.DropIndex(
                name: "IX_Leaderboard_RaceID",
                table: "Leaderboard");

            migrationBuilder.DropIndex(
                name: "IX_Leaderboard_RealmID",
                table: "Leaderboard");

            migrationBuilder.DropIndex(
                name: "IX_Leaderboard_RegionID",
                table: "Leaderboard");

            migrationBuilder.DropIndex(
                name: "IX_Leaderboard_RequestID",
                table: "Leaderboard");

            migrationBuilder.DropIndex(
                name: "IX_Leaderboard_SpecID",
                table: "Leaderboard");
        }
    }
}
