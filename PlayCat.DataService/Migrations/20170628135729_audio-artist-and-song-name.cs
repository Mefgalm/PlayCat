using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayCat.DataService.Migrations
{
    public partial class audioartistandsongname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "Audios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Song",
                table: "Audios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Audios");

            migrationBuilder.DropColumn(
                name: "Song",
                table: "Audios");
        }
    }
}
