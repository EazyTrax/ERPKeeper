using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB153 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "FundTransfers");

            migrationBuilder.DropColumn(
                name: "Credit",
                table: "FundTransferItems");

            migrationBuilder.RenameColumn(
                name: "Debit",
                table: "FundTransfers",
                newName: "WithDrawAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WithDrawAmount",
                table: "FundTransfers",
                newName: "Debit");

            migrationBuilder.AddColumn<decimal>(
                name: "Credit",
                table: "FundTransfers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Credit",
                table: "FundTransferItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
