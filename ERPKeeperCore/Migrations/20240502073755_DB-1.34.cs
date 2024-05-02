using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB134 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReceivePaymentId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierPaymentId",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ReceivePaymentId",
                table: "Sales",
                column: "ReceivePaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_SupplierPaymentId",
                table: "Purchases",
                column: "SupplierPaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_SupplierPayments_SupplierPaymentId",
                table: "Purchases",
                column: "SupplierPaymentId",
                principalTable: "SupplierPayments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_ReceivePayments_ReceivePaymentId",
                table: "Sales",
                column: "ReceivePaymentId",
                principalTable: "ReceivePayments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_SupplierPayments_SupplierPaymentId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_ReceivePayments_ReceivePaymentId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ReceivePaymentId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_SupplierPaymentId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ReceivePaymentId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SupplierPaymentId",
                table: "Purchases");
        }
    }
}
