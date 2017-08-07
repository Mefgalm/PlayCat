using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayCat.DataService.Migrations
{
    public partial class fixincrement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderValue",
                table: "Playlists",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderValue",
                table: "Playlists");
        }
    }
}
