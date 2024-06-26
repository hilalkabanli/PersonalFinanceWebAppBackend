using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceWebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    TransferID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderAccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverAccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.TransferID);
                    table.ForeignKey(
                        name: "FK_Transfers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_Transfers_Accounts_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserID",
                table: "Accounts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountID",
                table: "Transactions",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_AccountId",
                table: "Transfers",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_AccountId1",
                table: "Transfers",
                column: "AccountId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
