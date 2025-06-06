using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1230 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentBalance",
                table: "TransactionLedgers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentBalance",
                table: "TransactionLedgers");
        }
    }
}
