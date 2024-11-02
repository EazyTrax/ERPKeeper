using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1185 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DefaultExpenseAccountId",
                table: "Suppliers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DefaultIncomeAccountId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DefaultTaxCodeUid",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_DefaultExpenseAccountId",
                table: "Suppliers",
                column: "DefaultExpenseAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DefaultIncomeAccountId",
                table: "Customers",
                column: "DefaultIncomeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DefaultTaxCodeUid",
                table: "Customers",
                column: "DefaultTaxCodeUid");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Accounts_DefaultIncomeAccountId",
                table: "Customers",
                column: "DefaultIncomeAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_TaxCodes_DefaultTaxCodeUid",
                table: "Customers",
                column: "DefaultTaxCodeUid",
                principalTable: "TaxCodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Accounts_DefaultExpenseAccountId",
                table: "Suppliers",
                column: "DefaultExpenseAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Accounts_DefaultIncomeAccountId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_TaxCodes_DefaultTaxCodeUid",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Accounts_DefaultExpenseAccountId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_DefaultExpenseAccountId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_DefaultIncomeAccountId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_DefaultTaxCodeUid",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DefaultExpenseAccountId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "DefaultIncomeAccountId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DefaultTaxCodeUid",
                table: "Customers");
        }
    }
}
