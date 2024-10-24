using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1148 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IncomeAccountId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_IncomeAccountId",
                table: "Sales",
                column: "IncomeAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Accounts_IncomeAccountId",
                table: "Sales",
                column: "IncomeAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Accounts_IncomeAccountId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_IncomeAccountId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IncomeAccountId",
                table: "Sales");
        }
    }
}
