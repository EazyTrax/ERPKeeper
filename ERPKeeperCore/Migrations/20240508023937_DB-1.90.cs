using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB190 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_Accounts_BankFeeAccountId",
                table: "ReceivePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_Accounts_ReceivableAccountId",
                table: "ReceivePayments");

            migrationBuilder.DropIndex(
                name: "IX_ReceivePayments_BankFeeAccountId",
                table: "ReceivePayments");

            migrationBuilder.RenameColumn(
                name: "ReceivableAccountId",
                table: "ReceivePayments",
                newName: "Receivable_Asset_AccountId");

            migrationBuilder.RenameColumn(
                name: "BankFeeAccountId",
                table: "ReceivePayments",
                newName: "Discount_Given_Expense_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceivePayments_ReceivableAccountId",
                table: "ReceivePayments",
                newName: "IX_ReceivePayments_Receivable_Asset_AccountId");

            migrationBuilder.AddColumn<Guid>(
                name: "BankFee_Expense_AccountId",
                table: "ReceivePayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePayments_BankFee_Expense_AccountId",
                table: "ReceivePayments",
                column: "BankFee_Expense_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_Accounts_BankFee_Expense_AccountId",
                table: "ReceivePayments",
                column: "BankFee_Expense_AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_Accounts_Receivable_Asset_AccountId",
                table: "ReceivePayments",
                column: "Receivable_Asset_AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_Accounts_BankFee_Expense_AccountId",
                table: "ReceivePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_Accounts_Receivable_Asset_AccountId",
                table: "ReceivePayments");

            migrationBuilder.DropIndex(
                name: "IX_ReceivePayments_BankFee_Expense_AccountId",
                table: "ReceivePayments");

            migrationBuilder.DropColumn(
                name: "BankFee_Expense_AccountId",
                table: "ReceivePayments");

            migrationBuilder.RenameColumn(
                name: "Receivable_Asset_AccountId",
                table: "ReceivePayments",
                newName: "ReceivableAccountId");

            migrationBuilder.RenameColumn(
                name: "Discount_Given_Expense_AccountId",
                table: "ReceivePayments",
                newName: "BankFeeAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceivePayments_Receivable_Asset_AccountId",
                table: "ReceivePayments",
                newName: "IX_ReceivePayments_ReceivableAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePayments_BankFeeAccountId",
                table: "ReceivePayments",
                column: "BankFeeAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_Accounts_BankFeeAccountId",
                table: "ReceivePayments",
                column: "BankFeeAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_Accounts_ReceivableAccountId",
                table: "ReceivePayments",
                column: "ReceivableAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
