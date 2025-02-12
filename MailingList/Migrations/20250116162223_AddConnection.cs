using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MailingList.Migrations
{
    /// <inheritdoc />
    public partial class AddConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "EmailMessages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EmailMessages_ContactId",
                table: "EmailMessages",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailMessages_Contacts_ContactId",
                table: "EmailMessages",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailMessages_Contacts_ContactId",
                table: "EmailMessages");

            migrationBuilder.DropIndex(
                name: "IX_EmailMessages_ContactId",
                table: "EmailMessages");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "EmailMessages");
        }
    }
}
