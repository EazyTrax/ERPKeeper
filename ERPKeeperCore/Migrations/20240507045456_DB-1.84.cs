using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB184 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingCredit",
                table: "FiscalYearAccountBalances");

            migrationBuilder.DropColumn(
                name: "ClosingDebit",
                table: "FiscalYearAccountBalances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ClosingCredit",
                table: "FiscalYearAccountBalances",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ClosingDebit",
                table: "FiscalYearAccountBalances",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
