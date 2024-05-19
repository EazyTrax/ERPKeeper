using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1146 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseQuoteItems_PurchaseQuotes_SupplierQuoteId",
                table: "PurchaseQuoteItems");

            migrationBuilder.DropColumn(
                name: "QuoteId",
                table: "PurchaseQuoteItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplierQuoteId",
                table: "PurchaseQuoteItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseQuoteItems_PurchaseQuotes_SupplierQuoteId",
                table: "PurchaseQuoteItems",
                column: "SupplierQuoteId",
                principalTable: "PurchaseQuotes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseQuoteItems_PurchaseQuotes_SupplierQuoteId",
                table: "PurchaseQuoteItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplierQuoteId",
                table: "PurchaseQuoteItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "QuoteId",
                table: "PurchaseQuoteItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseQuoteItems_PurchaseQuotes_SupplierQuoteId",
                table: "PurchaseQuoteItems",
                column: "SupplierQuoteId",
                principalTable: "PurchaseQuotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
