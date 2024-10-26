using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1161 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountSold",
                table: "Items",
                newName: "SoldAmount");

            migrationBuilder.RenameColumn(
                name: "AmountPurchase",
                table: "Items",
                newName: "PurchaseAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "PurchaseValue",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SoldValue",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseValue",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SoldValue",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "SoldAmount",
                table: "Items",
                newName: "AmountSold");

            migrationBuilder.RenameColumn(
                name: "PurchaseAmount",
                table: "Items",
                newName: "AmountPurchase");
        }
    }
}
