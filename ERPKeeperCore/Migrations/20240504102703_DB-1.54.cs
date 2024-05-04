using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB154 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfitBalance",
                table: "FiscalYears");

            migrationBuilder.AddColumn<Guid>(
                name: "FiscalYearId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FiscalYearId",
                table: "Transactions");

            migrationBuilder.AddColumn<decimal>(
                name: "ProfitBalance",
                table: "FiscalYears",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
