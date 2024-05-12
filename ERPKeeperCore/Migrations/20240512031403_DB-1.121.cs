using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1121 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosedCredit",
                table: "FiscalYearAccountBalances");

            migrationBuilder.DropColumn(
                name: "ClosedDebit",
                table: "FiscalYearAccountBalances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ClosedCredit",
                table: "FiscalYearAccountBalances",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ClosedDebit",
                table: "FiscalYearAccountBalances",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
