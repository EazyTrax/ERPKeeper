using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB164 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "SupplierPayments",
                newName: "AmountRetention");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "SupplierPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountBankFee",
                table: "SupplierPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountDiscount",
                table: "SupplierPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "SupplierPayments");

            migrationBuilder.DropColumn(
                name: "AmountBankFee",
                table: "SupplierPayments");

            migrationBuilder.DropColumn(
                name: "AmountDiscount",
                table: "SupplierPayments");

            migrationBuilder.RenameColumn(
                name: "AmountRetention",
                table: "SupplierPayments",
                newName: "Total");
        }
    }
}
