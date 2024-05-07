using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB188 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BankFeeAccountId",
                table: "ReceivePayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePayments_BankFeeAccountId",
                table: "ReceivePayments",
                column: "BankFeeAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_Accounts_BankFeeAccountId",
                table: "ReceivePayments",
                column: "BankFeeAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_Accounts_BankFeeAccountId",
                table: "ReceivePayments");

            migrationBuilder.DropIndex(
                name: "IX_ReceivePayments_BankFeeAccountId",
                table: "ReceivePayments");

            migrationBuilder.DropColumn(
                name: "BankFeeAccountId",
                table: "ReceivePayments");
        }
    }
}
