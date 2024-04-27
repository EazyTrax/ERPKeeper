using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB114 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrnDate",
                table: "TaxPeriods",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "isDefault",
                table: "TaxCodes",
                newName: "IsDefault");

            migrationBuilder.RenameColumn(
                name: "TaxRate",
                table: "TaxCodes",
                newName: "Rate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "TaxPeriods",
                newName: "TrnDate");

            migrationBuilder.RenameColumn(
                name: "IsDefault",
                table: "TaxCodes",
                newName: "isDefault");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "TaxCodes",
                newName: "TaxRate");
        }
    }
}
