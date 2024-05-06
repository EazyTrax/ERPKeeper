using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB174 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPosted",
                table: "TaxPeriods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "TaxPeriods",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxPeriods_TransactionId",
                table: "TaxPeriods",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxPeriods_Transactions_TransactionId",
                table: "TaxPeriods",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxPeriods_Transactions_TransactionId",
                table: "TaxPeriods");

            migrationBuilder.DropIndex(
                name: "IX_TaxPeriods_TransactionId",
                table: "TaxPeriods");

            migrationBuilder.DropColumn(
                name: "IsPosted",
                table: "TaxPeriods");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "TaxPeriods");
        }
    }
}
