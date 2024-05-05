using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB158 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_Customers_CustomerId",
                table: "ReceivePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_Suppliers_SupplierId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_SupplierPayments_SupplierId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_ReceivePayments_CustomerId",
                table: "ReceivePayments");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "SupplierPayments");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ReceivePayments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "SupplierPayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "ReceivePayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_SupplierId",
                table: "SupplierPayments",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePayments_CustomerId",
                table: "ReceivePayments",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_Customers_CustomerId",
                table: "ReceivePayments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_Suppliers_SupplierId",
                table: "SupplierPayments",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }
    }
}
