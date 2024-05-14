using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1127 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PreviusFiscalYearId",
                table: "FiscalYears",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYears_PreviusFiscalYearId",
                table: "FiscalYears",
                column: "PreviusFiscalYearId",
                unique: true,
                filter: "[PreviusFiscalYearId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalYears_FiscalYears_PreviusFiscalYearId",
                table: "FiscalYears",
                column: "PreviusFiscalYearId",
                principalTable: "FiscalYears",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FiscalYears_FiscalYears_PreviusFiscalYearId",
                table: "FiscalYears");

            migrationBuilder.DropIndex(
                name: "IX_FiscalYears_PreviusFiscalYearId",
                table: "FiscalYears");

            migrationBuilder.DropColumn(
                name: "PreviusFiscalYearId",
                table: "FiscalYears");
        }
    }
}
