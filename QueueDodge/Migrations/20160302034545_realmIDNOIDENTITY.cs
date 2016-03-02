using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace QueueDodge.Migrations
{
    public partial class realmIDNOIDENTITY : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Realm",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Realm",
                nullable: false)
                .Annotation("Npgsql:Serial", true);
        }
    }
}
