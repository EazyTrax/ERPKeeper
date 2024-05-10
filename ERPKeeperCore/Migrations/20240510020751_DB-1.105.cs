using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1105 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_FiscalYears_FiscalYearId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_FiscalYears_TransactionId",
                table: "FiscalYears");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TransactionLedgers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYears_TransactionId",
                table: "FiscalYears",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FiscalYears_TransactionId",
                table: "FiscalYears");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TransactionLedgers");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYears_TransactionId",
                table: "FiscalYears",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_FiscalYears_FiscalYearId",
                table: "Transactions",
                column: "FiscalYearId",
                principalTable: "FiscalYears",
                principalColumn: "Id");
        }
    }
}
