using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceWebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationships2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Accounts_AccountId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Accounts_AccountId1",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_AccountId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_AccountId1",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "Transfers");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ReceiverAccountID",
                table: "Transfers",
                column: "ReceiverAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_SenderAccountID",
                table: "Transfers",
                column: "SenderAccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Accounts_ReceiverAccountID",
                table: "Transfers",
                column: "ReceiverAccountID",
                principalTable: "Accounts",
                principalColumn: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Accounts_SenderAccountID",
                table: "Transfers",
                column: "SenderAccountID",
                principalTable: "Accounts",
                principalColumn: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Accounts_ReceiverAccountID",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Accounts_SenderAccountID",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_ReceiverAccountID",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_SenderAccountID",
                table: "Transfers");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Transfers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId1",
                table: "Transfers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_AccountId",
                table: "Transfers",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_AccountId1",
                table: "Transfers",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Accounts_AccountId",
                table: "Transfers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Accounts_AccountId1",
                table: "Transfers",
                column: "AccountId1",
                principalTable: "Accounts",
                principalColumn: "AccountId");
        }
    }
}
