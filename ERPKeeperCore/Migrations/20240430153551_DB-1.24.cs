using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB124 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sales_TransactionId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_TransactionId",
                table: "Purchases");

            migrationBuilder.AddColumn<Guid>(
                name: "FundTransferId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "JournalEntries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FundTransferId",
                table: "Transactions",
                column: "FundTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_TransactionId",
                table: "Sales",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_TransactionId",
                table: "Purchases",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_TransactionId",
                table: "JournalEntries",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntries_Transactions_TransactionId",
                table: "JournalEntries",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_FundTransfers_FundTransferId",
                table: "Transactions",
                column: "FundTransferId",
                principalTable: "FundTransfers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntries_Transactions_TransactionId",
                table: "JournalEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_FundTransfers_FundTransferId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FundTransferId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Sales_TransactionId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_TransactionId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntries_TransactionId",
                table: "JournalEntries");

            migrationBuilder.DropColumn(
                name: "FundTransferId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "JournalEntries");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_TransactionId",
                table: "Sales",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_TransactionId",
                table: "Purchases",
                column: "TransactionId");
        }
    }
}
