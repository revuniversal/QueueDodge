using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace QueueDodge.Migrations
{
    public partial class removedIDfromLadder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LadderEntry",
                table: "LadderEntry");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "LadderEntry");

            migrationBuilder.AlterColumn<int>(
                name: "Ranking",
                table: "LadderEntry",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LadderEntry",
                table: "LadderEntry",
                column: "Ranking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LadderEntry",
                table: "LadderEntry");

            migrationBuilder.AlterColumn<int>(
                name: "Ranking",
                table: "LadderEntry",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "LadderEntry",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LadderEntry",
                table: "LadderEntry",
                column: "ID");
        }
    }
}
