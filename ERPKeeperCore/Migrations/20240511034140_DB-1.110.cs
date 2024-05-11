using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1110 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PayFrom_Amount",
                table: "IncomeTaxes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "PayFrom_AssetAccountId",
                table: "IncomeTaxes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTaxes_PayFrom_AssetAccountId",
                table: "IncomeTaxes",
                column: "PayFrom_AssetAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_Accounts_PayFrom_AssetAccountId",
                table: "IncomeTaxes",
                column: "PayFrom_AssetAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_Accounts_PayFrom_AssetAccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropIndex(
                name: "IX_IncomeTaxes_PayFrom_AssetAccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropColumn(
                name: "PayFrom_Amount",
                table: "IncomeTaxes");

            migrationBuilder.DropColumn(
                name: "PayFrom_AssetAccountId",
                table: "IncomeTaxes");
        }
    }
}
