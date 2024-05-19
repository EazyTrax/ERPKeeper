using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1144 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseQuotes_Suppliers_SupplierId",
                table: "PurchaseQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleQuotes_Customers_CustomerId",
                table: "SaleQuotes");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "SaleQuotes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplierId",
                table: "PurchaseQuotes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseQuotes_Suppliers_SupplierId",
                table: "PurchaseQuotes",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleQuotes_Customers_CustomerId",
                table: "SaleQuotes",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseQuotes_Suppliers_SupplierId",
                table: "PurchaseQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleQuotes_Customers_CustomerId",
                table: "SaleQuotes");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "SaleQuotes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplierId",
                table: "PurchaseQuotes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseQuotes_Suppliers_SupplierId",
                table: "PurchaseQuotes",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleQuotes_Customers_CustomerId",
                table: "SaleQuotes",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
