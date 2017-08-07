using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PlayCat.DataService.Migrations
{
    public partial class ordercolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "AudioPlaylists",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "UniqueIdentifier",
                table: "Audios",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Audios",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Extension",
                table: "Audios",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "AccessUrl",
                table: "Audios",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "AudioPlaylists");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueIdentifier",
                table: "Audios",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Audios",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Extension",
                table: "Audios",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "AccessUrl",
                table: "Audios",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150);
        }
    }
}
