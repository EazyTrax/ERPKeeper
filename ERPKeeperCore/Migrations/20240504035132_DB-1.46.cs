using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB146 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SupplierPayments_TransactionId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntries_TransactionId",
                table: "JournalEntries");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Purchases");

            migrationBuilder.AddColumn<DateTime>(
                name: "PostedDate",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReceivableAccountId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReceivableAccountId",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_TransactionId",
                table: "SupplierPayments",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ReceivableAccountId",
                table: "Sales",
                column: "ReceivableAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ReceivableAccountId",
                table: "Purchases",
                column: "ReceivableAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_TransactionId",
                table: "JournalEntries",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Accounts_ReceivableAccountId",
                table: "Purchases",
                column: "ReceivableAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Accounts_ReceivableAccountId",
                table: "Sales",
                column: "ReceivableAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Accounts_ReceivableAccountId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Accounts_ReceivableAccountId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_SupplierPayments_TransactionId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ReceivableAccountId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ReceivableAccountId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntries_TransactionId",
                table: "JournalEntries");

            migrationBuilder.DropColumn(
                name: "PostedDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ReceivableAccountId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ReceivableAccountId",
                table: "Purchases");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Purchases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_TransactionId",
                table: "SupplierPayments",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_TransactionId",
                table: "JournalEntries",
                column: "TransactionId");
        }
    }
}
