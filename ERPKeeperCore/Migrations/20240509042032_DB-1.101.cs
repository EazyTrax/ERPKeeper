using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaxPeriods_TransactionId",
                table: "TaxPeriods");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercent",
                table: "SaleItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercent",
                table: "PurchaseItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_TaxPeriods_TransactionId",
                table: "TaxPeriods",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaxPeriods_TransactionId",
                table: "TaxPeriods");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "PurchaseItems");

            migrationBuilder.CreateIndex(
                name: "IX_TaxPeriods_TransactionId",
                table: "TaxPeriods",
                column: "TransactionId");
        }
    }
}
