using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1149 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseAccountId",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ExpenseAccountId",
                table: "Purchases",
                column: "ExpenseAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Accounts_ExpenseAccountId",
                table: "Purchases",
                column: "ExpenseAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Accounts_ExpenseAccountId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ExpenseAccountId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ExpenseAccountId",
                table: "Purchases");
        }
    }
}
