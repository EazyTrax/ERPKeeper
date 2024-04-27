using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB118 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "SaleItems",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "SaleItems",
                newName: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SaleItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartNumber",
                table: "SaleItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Serial",
                table: "SaleItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "PartNumber",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "Serial",
                table: "SaleItems");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "SaleItems",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "SaleItems",
                newName: "UnitPrice");
        }
    }
}
