using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1124 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BankFee",
                table: "FundTransfers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "BankFee_Expense_AccountId",
                table: "FundTransfers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Deposit_Asset_AccountId",
                table: "FundTransfers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfers_BankFee_Expense_AccountId",
                table: "FundTransfers",
                column: "BankFee_Expense_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfers_Deposit_Asset_AccountId",
                table: "FundTransfers",
                column: "Deposit_Asset_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_FundTransfers_Accounts_BankFee_Expense_AccountId",
                table: "FundTransfers",
                column: "BankFee_Expense_AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FundTransfers_Accounts_Deposit_Asset_AccountId",
                table: "FundTransfers",
                column: "Deposit_Asset_AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FundTransfers_Accounts_BankFee_Expense_AccountId",
                table: "FundTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_FundTransfers_Accounts_Deposit_Asset_AccountId",
                table: "FundTransfers");

            migrationBuilder.DropIndex(
                name: "IX_FundTransfers_BankFee_Expense_AccountId",
                table: "FundTransfers");

            migrationBuilder.DropIndex(
                name: "IX_FundTransfers_Deposit_Asset_AccountId",
                table: "FundTransfers");

            migrationBuilder.DropColumn(
                name: "BankFee",
                table: "FundTransfers");

            migrationBuilder.DropColumn(
                name: "BankFee_Expense_AccountId",
                table: "FundTransfers");

            migrationBuilder.DropColumn(
                name: "Deposit_Asset_AccountId",
                table: "FundTransfers");
        }
    }
}
