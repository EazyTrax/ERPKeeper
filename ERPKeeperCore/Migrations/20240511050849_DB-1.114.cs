using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1114 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WriteOff_TaxReceiveable_ExpenseAccountId",
                table: "IncomeTaxes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTaxes_WriteOff_TaxReceiveable_ExpenseAccountId",
                table: "IncomeTaxes",
                column: "WriteOff_TaxReceiveable_ExpenseAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_Accounts_WriteOff_TaxReceiveable_ExpenseAccountId",
                table: "IncomeTaxes",
                column: "WriteOff_TaxReceiveable_ExpenseAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_Accounts_WriteOff_TaxReceiveable_ExpenseAccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropIndex(
                name: "IX_IncomeTaxes_WriteOff_TaxReceiveable_ExpenseAccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropColumn(
                name: "WriteOff_TaxReceiveable_ExpenseAccountId",
                table: "IncomeTaxes");
        }
    }
}
