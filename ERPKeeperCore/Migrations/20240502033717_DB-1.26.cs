using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB126 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SumPurchaseBalance",
                table: "Suppliers",
                newName: "TotalPurchases");

            migrationBuilder.RenameColumn(
                name: "CountPurchases",
                table: "Suppliers",
                newName: "TotalPurchasesCount");

            migrationBuilder.RenameColumn(
                name: "CountBalance",
                table: "Suppliers",
                newName: "TotalBalanceCount");

            migrationBuilder.RenameColumn(
                name: "TotalSale",
                table: "Customers",
                newName: "TotalSales");

            migrationBuilder.RenameColumn(
                name: "CountSales",
                table: "Customers",
                newName: "TotalSalesCount");

            migrationBuilder.RenameColumn(
                name: "CountBalance",
                table: "Customers",
                newName: "TotalBalanceCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPurchasesCount",
                table: "Suppliers",
                newName: "CountPurchases");

            migrationBuilder.RenameColumn(
                name: "TotalPurchases",
                table: "Suppliers",
                newName: "SumPurchaseBalance");

            migrationBuilder.RenameColumn(
                name: "TotalBalanceCount",
                table: "Suppliers",
                newName: "CountBalance");

            migrationBuilder.RenameColumn(
                name: "TotalSalesCount",
                table: "Customers",
                newName: "CountSales");

            migrationBuilder.RenameColumn(
                name: "TotalSales",
                table: "Customers",
                newName: "TotalSale");

            migrationBuilder.RenameColumn(
                name: "TotalBalanceCount",
                table: "Customers",
                newName: "CountBalance");
        }
    }
}
