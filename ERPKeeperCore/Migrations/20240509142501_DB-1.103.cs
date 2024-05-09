using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1103 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "FiscalYears",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYears_TransactionId",
                table: "FiscalYears",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalYears_Transactions_TransactionId",
                table: "FiscalYears",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FiscalYears_Transactions_TransactionId",
                table: "FiscalYears");

            migrationBuilder.DropIndex(
                name: "IX_FiscalYears_TransactionId",
                table: "FiscalYears");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "FiscalYears");
        }
    }
}
