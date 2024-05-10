using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1107 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Sales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "DiscountAccountId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Discount_Given_Expense_AccountId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Purchases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "IncomeAccount_DiscountTakenId",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_DiscountAccountId",
                table: "Sales",
                column: "DiscountAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_IncomeAccount_DiscountTakenId",
                table: "Purchases",
                column: "IncomeAccount_DiscountTakenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Accounts_IncomeAccount_DiscountTakenId",
                table: "Purchases",
                column: "IncomeAccount_DiscountTakenId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Accounts_DiscountAccountId",
                table: "Sales",
                column: "DiscountAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Accounts_IncomeAccount_DiscountTakenId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Accounts_DiscountAccountId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_DiscountAccountId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_IncomeAccount_DiscountTakenId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "DiscountAccountId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Discount_Given_Expense_AccountId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "IncomeAccount_DiscountTakenId",
                table: "Purchases");
        }
    }
}
