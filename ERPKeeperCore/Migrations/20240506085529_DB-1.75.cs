using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB175 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPosted",
                table: "EmployeePayments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "EmployeePayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FiscalYearId",
                table: "Transactions",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayments_TransactionId",
                table: "EmployeePayments",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayments_Transactions_TransactionId",
                table: "EmployeePayments",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_FiscalYears_FiscalYearId",
                table: "Transactions",
                column: "FiscalYearId",
                principalTable: "FiscalYears",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayments_Transactions_TransactionId",
                table: "EmployeePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_FiscalYears_FiscalYearId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FiscalYearId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePayments_TransactionId",
                table: "EmployeePayments");

            migrationBuilder.DropColumn(
                name: "IsPosted",
                table: "EmployeePayments");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "EmployeePayments");
        }
    }
}
