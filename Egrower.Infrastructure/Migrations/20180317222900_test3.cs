using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Egrower.Infrastructure.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_EmailMessages_EmailMessageId",
                table: "Attachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments");

            migrationBuilder.RenameTable(
                name: "Attachments",
                newName: "Atachments");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_EmailMessageId",
                table: "Atachments",
                newName: "IX_Atachments_EmailMessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Atachments",
                table: "Atachments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Atachments_EmailMessages_EmailMessageId",
                table: "Atachments",
                column: "EmailMessageId",
                principalTable: "EmailMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atachments_EmailMessages_EmailMessageId",
                table: "Atachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Atachments",
                table: "Atachments");

            migrationBuilder.RenameTable(
                name: "Atachments",
                newName: "Attachments");

            migrationBuilder.RenameIndex(
                name: "IX_Atachments_EmailMessageId",
                table: "Attachments",
                newName: "IX_Attachments_EmailMessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_EmailMessages_EmailMessageId",
                table: "Attachments",
                column: "EmailMessageId",
                principalTable: "EmailMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
