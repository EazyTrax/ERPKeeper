using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB161 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "ReceivePayments",
                newName: "Amount");

            migrationBuilder.AddColumn<Guid>(
                name: "PayFromAccountId",
                table: "SupplierPayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PayToAccountId",
                table: "ReceivePayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_PayFromAccountId",
                table: "SupplierPayments",
                column: "PayFromAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePayments_PayToAccountId",
                table: "ReceivePayments",
                column: "PayToAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_Accounts_PayToAccountId",
                table: "ReceivePayments",
                column: "PayToAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_Accounts_PayFromAccountId",
                table: "SupplierPayments",
                column: "PayFromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_Accounts_PayToAccountId",
                table: "ReceivePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_Accounts_PayFromAccountId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_SupplierPayments_PayFromAccountId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_ReceivePayments_PayToAccountId",
                table: "ReceivePayments");

            migrationBuilder.DropColumn(
                name: "PayFromAccountId",
                table: "SupplierPayments");

            migrationBuilder.DropColumn(
                name: "PayToAccountId",
                table: "ReceivePayments");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "ReceivePayments",
                newName: "Total");
        }
    }
}
