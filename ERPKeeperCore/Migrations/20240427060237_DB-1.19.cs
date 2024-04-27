using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB119 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "PurchaseItems");

            migrationBuilder.RenameColumn(
                name: "TrnDate",
                table: "Purchases",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "FiscalYearId",
                table: "Purchases",
                newName: "SupplyerId");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "PurchaseItems",
                newName: "Price");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplyerAddressId",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PurchaseItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartNumber",
                table: "PurchaseItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PurchaseItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Serial",
                table: "PurchaseItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplyerAddressId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "PartNumber",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "Serial",
                table: "PurchaseItems");

            migrationBuilder.RenameColumn(
                name: "SupplyerId",
                table: "Purchases",
                newName: "FiscalYearId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Purchases",
                newName: "TrnDate");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "PurchaseItems",
                newName: "UnitPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "PurchaseItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
