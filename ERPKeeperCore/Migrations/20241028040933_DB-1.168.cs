using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1168 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PayFrom_CashEquivalent_AccountId",
                table: "IncomeTaxes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Tax_Liability_AccountId",
                table: "IncomeTaxes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Tax_Receiveable_AccountId",
                table: "IncomeTaxes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTaxes_PayFrom_CashEquivalent_AccountId",
                table: "IncomeTaxes",
                column: "PayFrom_CashEquivalent_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTaxes_Tax_Liability_AccountId",
                table: "IncomeTaxes",
                column: "Tax_Liability_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTaxes_Tax_Receiveable_AccountId",
                table: "IncomeTaxes",
                column: "Tax_Receiveable_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_Accounts_PayFrom_CashEquivalent_AccountId",
                table: "IncomeTaxes",
                column: "PayFrom_CashEquivalent_AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_Accounts_Tax_Liability_AccountId",
                table: "IncomeTaxes",
                column: "Tax_Liability_AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_Accounts_Tax_Receiveable_AccountId",
                table: "IncomeTaxes",
                column: "Tax_Receiveable_AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_Accounts_PayFrom_CashEquivalent_AccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_Accounts_Tax_Liability_AccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_Accounts_Tax_Receiveable_AccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropIndex(
                name: "IX_IncomeTaxes_PayFrom_CashEquivalent_AccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropIndex(
                name: "IX_IncomeTaxes_Tax_Liability_AccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropIndex(
                name: "IX_IncomeTaxes_Tax_Receiveable_AccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropColumn(
                name: "PayFrom_CashEquivalent_AccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropColumn(
                name: "Tax_Liability_AccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropColumn(
                name: "Tax_Receiveable_AccountId",
                table: "IncomeTaxes");
        }
    }
}
