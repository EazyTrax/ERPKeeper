using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB125 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_FundTransfers_FundTransferId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FundTransferId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FundTransferId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TransactionLedgers");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "TransactionLedgers");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "FundTransfers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfers_TransactionId",
                table: "FundTransfers",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FundTransfers_Transactions_TransactionId",
                table: "FundTransfers",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FundTransfers_Transactions_TransactionId",
                table: "FundTransfers");

            migrationBuilder.DropIndex(
                name: "IX_FundTransfers_TransactionId",
                table: "FundTransfers");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "FundTransfers");

            migrationBuilder.AddColumn<Guid>(
                name: "FundTransferId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TransactionLedgers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "TransactionLedgers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FundTransferId",
                table: "Transactions",
                column: "FundTransferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_FundTransfers_FundTransferId",
                table: "Transactions",
                column: "FundTransferId",
                principalTable: "FundTransfers",
                principalColumn: "Id");
        }
    }
}
