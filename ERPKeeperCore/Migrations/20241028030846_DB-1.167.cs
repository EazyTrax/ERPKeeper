using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1167 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Ramain_TaxReceiveableAccount_Amount",
                table: "IncomeTaxes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Remain_Liability_Amount",
                table: "IncomeTaxes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ramain_TaxReceiveableAccount_Amount",
                table: "IncomeTaxes");

            migrationBuilder.DropColumn(
                name: "Remain_Liability_Amount",
                table: "IncomeTaxes");
        }
    }
}
