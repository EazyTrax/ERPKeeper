using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB159 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_ReceivePayments_ReceivePaymentId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ReceivePaymentId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ReceivePaymentId",
                table: "Sales");

            migrationBuilder.AddColumn<Guid>(
                name: "SaleId",
                table: "ReceivePayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePayments_SaleId",
                table: "ReceivePayments",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_Sales_SaleId",
                table: "ReceivePayments",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_Sales_SaleId",
                table: "ReceivePayments");

            migrationBuilder.DropIndex(
                name: "IX_ReceivePayments_SaleId",
                table: "ReceivePayments");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "ReceivePayments");

            migrationBuilder.AddColumn<Guid>(
                name: "ReceivePaymentId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ReceivePaymentId",
                table: "Sales",
                column: "ReceivePaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_ReceivePayments_ReceivePaymentId",
                table: "Sales",
                column: "ReceivePaymentId",
                principalTable: "ReceivePayments",
                principalColumn: "Id");
        }
    }
}
