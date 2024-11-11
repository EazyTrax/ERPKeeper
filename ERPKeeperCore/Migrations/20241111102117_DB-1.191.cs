using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1191 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentTermId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentTermId",
                table: "SaleQuotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentTermId",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentTermId",
                table: "PurchaseQuotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PaymentTermId",
                table: "Sales",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleQuotes_PaymentTermId",
                table: "SaleQuotes",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PaymentTermId",
                table: "Purchases",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseQuotes_PaymentTermId",
                table: "PurchaseQuotes",
                column: "PaymentTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseQuotes_PaymentTerms_PaymentTermId",
                table: "PurchaseQuotes",
                column: "PaymentTermId",
                principalTable: "PaymentTerms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_PaymentTerms_PaymentTermId",
                table: "Purchases",
                column: "PaymentTermId",
                principalTable: "PaymentTerms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleQuotes_PaymentTerms_PaymentTermId",
                table: "SaleQuotes",
                column: "PaymentTermId",
                principalTable: "PaymentTerms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_PaymentTerms_PaymentTermId",
                table: "Sales",
                column: "PaymentTermId",
                principalTable: "PaymentTerms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseQuotes_PaymentTerms_PaymentTermId",
                table: "PurchaseQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_PaymentTerms_PaymentTermId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleQuotes_PaymentTerms_PaymentTermId",
                table: "SaleQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_PaymentTerms_PaymentTermId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_PaymentTermId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_SaleQuotes_PaymentTermId",
                table: "SaleQuotes");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_PaymentTermId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseQuotes_PaymentTermId",
                table: "PurchaseQuotes");

            migrationBuilder.DropColumn(
                name: "PaymentTermId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "PaymentTermId",
                table: "SaleQuotes");

            migrationBuilder.DropColumn(
                name: "PaymentTermId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PaymentTermId",
                table: "PurchaseQuotes");
        }
    }
}
