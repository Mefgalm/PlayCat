using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayCat.DataService.Migrations
{
    public partial class addaudiouploader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UploaderId",
                table: "Audios",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Audios_UploaderId",
                table: "Audios",
                column: "UploaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Audios_Users_UploaderId",
                table: "Audios",
                column: "UploaderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audios_Users_UploaderId",
                table: "Audios");

            migrationBuilder.DropIndex(
                name: "IX_Audios_UploaderId",
                table: "Audios");

            migrationBuilder.DropColumn(
                name: "UploaderId",
                table: "Audios");
        }
    }
}
