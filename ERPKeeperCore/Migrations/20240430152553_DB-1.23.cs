using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FundTransfers_Accounts_BankFeeAccountGuid",
                table: "FundTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_FundTransfers_Accounts_DepositAccountGuid",
                table: "FundTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_FundTransfers_Accounts_WithDrawAccountGuid",
                table: "FundTransfers");

            migrationBuilder.DropIndex(
                name: "IX_FundTransfers_BankFeeAccountGuid",
                table: "FundTransfers");

            migrationBuilder.DropIndex(
                name: "IX_FundTransfers_DepositAccountGuid",
                table: "FundTransfers");

            migrationBuilder.DropIndex(
                name: "IX_FundTransfers_WithDrawAccountGuid",
                table: "FundTransfers");

            migrationBuilder.DropColumn(
                name: "AmountFee",
                table: "FundTransfers");

            migrationBuilder.DropColumn(
                name: "AmountwithDraw",
                table: "FundTransfers");

            migrationBuilder.DropColumn(
                name: "BankFeeAccountGuid",
                table: "FundTransfers");

            migrationBuilder.DropColumn(
                name: "DepositAccountGuid",
                table: "FundTransfers");

            migrationBuilder.DropColumn(
                name: "TotalCredit",
                table: "FundTransfers");

            migrationBuilder.DropColumn(
                name: "TotalDebit",
                table: "FundTransfers");

            migrationBuilder.DropColumn(
                name: "WithDrawAccountGuid",
                table: "FundTransfers");

            migrationBuilder.DropColumn(
                name: "CreditAmount",
                table: "FundTransferItems");

            migrationBuilder.DropColumn(
                name: "DebitAmount",
                table: "FundTransferItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountFee",
                table: "FundTransfers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountwithDraw",
                table: "FundTransfers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "BankFeeAccountGuid",
                table: "FundTransfers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DepositAccountGuid",
                table: "FundTransfers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCredit",
                table: "FundTransfers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDebit",
                table: "FundTransfers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "WithDrawAccountGuid",
                table: "FundTransfers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CreditAmount",
                table: "FundTransferItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DebitAmount",
                table: "FundTransferItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfers_BankFeeAccountGuid",
                table: "FundTransfers",
                column: "BankFeeAccountGuid");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfers_DepositAccountGuid",
                table: "FundTransfers",
                column: "DepositAccountGuid");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfers_WithDrawAccountGuid",
                table: "FundTransfers",
                column: "WithDrawAccountGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_FundTransfers_Accounts_BankFeeAccountGuid",
                table: "FundTransfers",
                column: "BankFeeAccountGuid",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FundTransfers_Accounts_DepositAccountGuid",
                table: "FundTransfers",
                column: "DepositAccountGuid",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FundTransfers_Accounts_WithDrawAccountGuid",
                table: "FundTransfers",
                column: "WithDrawAccountGuid",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
