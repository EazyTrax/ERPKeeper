using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB193 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_Accounts_PayFromAccountId",
                table: "SupplierPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_Accounts_PayableAccountId",
                table: "SupplierPayments");

            migrationBuilder.RenameColumn(
                name: "PayableAccountId",
                table: "SupplierPayments",
                newName: "PayFrom_AssetAccountId");

            migrationBuilder.RenameColumn(
                name: "PayFromAccountId",
                table: "SupplierPayments",
                newName: "LiablityAccount_SupplierPayableId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierPayments_PayFromAccountId",
                table: "SupplierPayments",
                newName: "IX_SupplierPayments_LiablityAccount_SupplierPayableId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierPayments_PayableAccountId",
                table: "SupplierPayments",
                newName: "IX_SupplierPayments_PayFrom_AssetAccountId");

            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseAccount_BankFeeId",
                table: "SupplierPayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IncomeAccount_DiscountTakenId",
                table: "SupplierPayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_ExpenseAccount_BankFeeId",
                table: "SupplierPayments",
                column: "ExpenseAccount_BankFeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_IncomeAccount_DiscountTakenId",
                table: "SupplierPayments",
                column: "IncomeAccount_DiscountTakenId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_Accounts_ExpenseAccount_BankFeeId",
                table: "SupplierPayments",
                column: "ExpenseAccount_BankFeeId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_Accounts_IncomeAccount_DiscountTakenId",
                table: "SupplierPayments",
                column: "IncomeAccount_DiscountTakenId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_Accounts_LiablityAccount_SupplierPayableId",
                table: "SupplierPayments",
                column: "LiablityAccount_SupplierPayableId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_Accounts_PayFrom_AssetAccountId",
                table: "SupplierPayments",
                column: "PayFrom_AssetAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_Accounts_ExpenseAccount_BankFeeId",
                table: "SupplierPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_Accounts_IncomeAccount_DiscountTakenId",
                table: "SupplierPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_Accounts_LiablityAccount_SupplierPayableId",
                table: "SupplierPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_Accounts_PayFrom_AssetAccountId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_SupplierPayments_ExpenseAccount_BankFeeId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_SupplierPayments_IncomeAccount_DiscountTakenId",
                table: "SupplierPayments");

            migrationBuilder.DropColumn(
                name: "ExpenseAccount_BankFeeId",
                table: "SupplierPayments");

            migrationBuilder.DropColumn(
                name: "IncomeAccount_DiscountTakenId",
                table: "SupplierPayments");

            migrationBuilder.RenameColumn(
                name: "PayFrom_AssetAccountId",
                table: "SupplierPayments",
                newName: "PayableAccountId");

            migrationBuilder.RenameColumn(
                name: "LiablityAccount_SupplierPayableId",
                table: "SupplierPayments",
                newName: "PayFromAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierPayments_PayFrom_AssetAccountId",
                table: "SupplierPayments",
                newName: "IX_SupplierPayments_PayableAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierPayments_LiablityAccount_SupplierPayableId",
                table: "SupplierPayments",
                newName: "IX_SupplierPayments_PayFromAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_Accounts_PayFromAccountId",
                table: "SupplierPayments",
                column: "PayFromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_Accounts_PayableAccountId",
                table: "SupplierPayments",
                column: "PayableAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
