using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB162 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountBankFee",
                table: "ReceivePayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountDiscount",
                table: "ReceivePayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountRetention",
                table: "ReceivePayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "DiscountAccountId",
                table: "ReceivePayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePayments_DiscountAccountId",
                table: "ReceivePayments",
                column: "DiscountAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_Accounts_DiscountAccountId",
                table: "ReceivePayments",
                column: "DiscountAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_Accounts_DiscountAccountId",
                table: "ReceivePayments");

            migrationBuilder.DropIndex(
                name: "IX_ReceivePayments_DiscountAccountId",
                table: "ReceivePayments");

            migrationBuilder.DropColumn(
                name: "AmountBankFee",
                table: "ReceivePayments");

            migrationBuilder.DropColumn(
                name: "AmountDiscount",
                table: "ReceivePayments");

            migrationBuilder.DropColumn(
                name: "AmountRetention",
                table: "ReceivePayments");

            migrationBuilder.DropColumn(
                name: "DiscountAccountId",
                table: "ReceivePayments");
        }
    }
}
