using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1120 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseBalance",
                table: "FiscalYears");

            migrationBuilder.DropColumn(
                name: "IncomeBalance",
                table: "FiscalYears");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ExpenseBalance",
                table: "FiscalYears",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IncomeBalance",
                table: "FiscalYears",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
