using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayCat.DataService.Migrations
{
    public partial class renamefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordSald",
                table: "Users",
                newName: "PasswordSalt");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AuthTokens",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AuthTokens");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "Users",
                newName: "PasswordSald");
        }
    }
}
