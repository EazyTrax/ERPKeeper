using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1112 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PayFrom_Amount",
                table: "IncomeTaxes",
                newName: "TaxReceivable_Amount");

            migrationBuilder.AddColumn<decimal>(
                name: "PayFrom_TaxReceiveableAccount_Amount",
                table: "IncomeTaxes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Ramain_TaxReceiveableAccount_Amount",
                table: "IncomeTaxes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayFrom_TaxReceiveableAccount_Amount",
                table: "IncomeTaxes");

            migrationBuilder.DropColumn(
                name: "Ramain_TaxReceiveableAccount_Amount",
                table: "IncomeTaxes");

            migrationBuilder.RenameColumn(
                name: "TaxReceivable_Amount",
                table: "IncomeTaxes",
                newName: "PayFrom_Amount");
        }
    }
}
