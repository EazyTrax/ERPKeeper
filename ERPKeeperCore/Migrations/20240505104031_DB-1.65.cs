using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB165 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PayableAccountId",
                table: "SupplierPayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReceivableAccountId",
                table: "ReceivePayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_PayableAccountId",
                table: "SupplierPayments",
                column: "PayableAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePayments_ReceivableAccountId",
                table: "ReceivePayments",
                column: "ReceivableAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_Accounts_ReceivableAccountId",
                table: "ReceivePayments",
                column: "ReceivableAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_Accounts_PayableAccountId",
                table: "SupplierPayments",
                column: "PayableAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_Accounts_ReceivableAccountId",
                table: "ReceivePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_Accounts_PayableAccountId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_SupplierPayments_PayableAccountId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_ReceivePayments_ReceivableAccountId",
                table: "ReceivePayments");

            migrationBuilder.DropColumn(
                name: "PayableAccountId",
                table: "SupplierPayments");

            migrationBuilder.DropColumn(
                name: "ReceivableAccountId",
                table: "ReceivePayments");
        }
    }
}
