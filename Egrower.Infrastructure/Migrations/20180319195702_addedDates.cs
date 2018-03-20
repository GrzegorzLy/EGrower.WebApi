using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Egrower.Infrastructure.Migrations
{
    public partial class addedDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "EmailMessages",
                newName: "DateAddedToTheDataBase");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EmailMessages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedAt",
                table: "Atachments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EmailMessages");

            migrationBuilder.DropColumn(
                name: "AddedAt",
                table: "Atachments");

            migrationBuilder.RenameColumn(
                name: "DateAddedToTheDataBase",
                table: "EmailMessages",
                newName: "Date");
        }
    }
}
