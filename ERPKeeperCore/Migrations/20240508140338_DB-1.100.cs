using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseTaxAccountId",
                table: "TaxPeriods",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SaleTaxAccountId",
                table: "TaxPeriods",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxPeriods_PurchaseTaxAccountId",
                table: "TaxPeriods",
                column: "PurchaseTaxAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxPeriods_SaleTaxAccountId",
                table: "TaxPeriods",
                column: "SaleTaxAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxPeriods_Accounts_PurchaseTaxAccountId",
                table: "TaxPeriods",
                column: "PurchaseTaxAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxPeriods_Accounts_SaleTaxAccountId",
                table: "TaxPeriods",
                column: "SaleTaxAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxPeriods_Accounts_PurchaseTaxAccountId",
                table: "TaxPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxPeriods_Accounts_SaleTaxAccountId",
                table: "TaxPeriods");

            migrationBuilder.DropIndex(
                name: "IX_TaxPeriods_PurchaseTaxAccountId",
                table: "TaxPeriods");

            migrationBuilder.DropIndex(
                name: "IX_TaxPeriods_SaleTaxAccountId",
                table: "TaxPeriods");

            migrationBuilder.DropColumn(
                name: "PurchaseTaxAccountId",
                table: "TaxPeriods");

            migrationBuilder.DropColumn(
                name: "SaleTaxAccountId",
                table: "TaxPeriods");
        }
    }
}
