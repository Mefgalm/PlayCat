using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayCat.DataService.Migrations
{
    public partial class rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_UserId",
                table: "Playlists");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Playlists",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_UserId",
                table: "Playlists",
                newName: "IX_Playlists_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_OwnerId",
                table: "Playlists",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_OwnerId",
                table: "Playlists");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Playlists",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_OwnerId",
                table: "Playlists",
                newName: "IX_Playlists_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_UserId",
                table: "Playlists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
