using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayCat.DataService.Migrations
{
    public partial class authtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuthTokens_UserId",
                table: "AuthTokens");

            migrationBuilder.AlterColumn<string>(
                name: "VerificationCode",
                table: "Users",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AuthTokens_UserId",
                table: "AuthTokens",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuthTokens_UserId",
                table: "AuthTokens");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "VerificationCode",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_AuthTokens_UserId",
                table: "AuthTokens",
                column: "UserId");
        }
    }
}
