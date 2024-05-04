using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB160 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_SupplierPayments_SupplierPaymentId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_SupplierPaymentId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "SupplierPaymentId",
                table: "Purchases");

            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseId",
                table: "SupplierPayments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_PurchaseId",
                table: "SupplierPayments",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_Purchases_PurchaseId",
                table: "SupplierPayments",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_Purchases_PurchaseId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_SupplierPayments_PurchaseId",
                table: "SupplierPayments");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "SupplierPayments");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierPaymentId",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: true);

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
        }
    }
}
