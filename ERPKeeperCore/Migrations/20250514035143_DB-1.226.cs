using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1226 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShipmentAddesssId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ShipmentAddesssId",
                table: "Sales",
                column: "ShipmentAddesssId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_ProfileAddresses_ShipmentAddesssId",
                table: "Sales",
                column: "ShipmentAddesssId",
                principalTable: "ProfileAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_ProfileAddresses_ShipmentAddesssId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ShipmentAddesssId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ShipmentAddesssId",
                table: "Sales");
        }
    }
}
