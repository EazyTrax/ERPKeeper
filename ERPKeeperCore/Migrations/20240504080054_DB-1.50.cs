using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB150 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OutputTaxBalance",
                table: "TaxPeriods",
                newName: "SalesTaxBalance");

            migrationBuilder.RenameColumn(
                name: "OutputCommercialCount",
                table: "TaxPeriods",
                newName: "SalesCount");

            migrationBuilder.RenameColumn(
                name: "OutputBalance",
                table: "TaxPeriods",
                newName: "SalesBalance");

            migrationBuilder.RenameColumn(
                name: "InputTaxBalance",
                table: "TaxPeriods",
                newName: "PurchasesTaxBalance");

            migrationBuilder.RenameColumn(
                name: "InputCommercialCount",
                table: "TaxPeriods",
                newName: "PurchasesCount");

            migrationBuilder.RenameColumn(
                name: "InputBalance",
                table: "TaxPeriods",
                newName: "PuchasesBalance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalesTaxBalance",
                table: "TaxPeriods",
                newName: "OutputTaxBalance");

            migrationBuilder.RenameColumn(
                name: "SalesCount",
                table: "TaxPeriods",
                newName: "OutputCommercialCount");

            migrationBuilder.RenameColumn(
                name: "SalesBalance",
                table: "TaxPeriods",
                newName: "OutputBalance");

            migrationBuilder.RenameColumn(
                name: "PurchasesTaxBalance",
                table: "TaxPeriods",
                newName: "InputTaxBalance");

            migrationBuilder.RenameColumn(
                name: "PurchasesCount",
                table: "TaxPeriods",
                newName: "InputCommercialCount");

            migrationBuilder.RenameColumn(
                name: "PuchasesBalance",
                table: "TaxPeriods",
                newName: "InputBalance");
        }
    }
}
