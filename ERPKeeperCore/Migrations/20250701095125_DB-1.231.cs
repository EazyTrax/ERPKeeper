using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1231 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShipmentAddesssId",
                table: "Shipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ShipmentAddesssId",
                table: "Shipments",
                column: "ShipmentAddesssId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_ProfileAddresses_ShipmentAddesssId",
                table: "Shipments",
                column: "ShipmentAddesssId",
                principalTable: "ProfileAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_ProfileAddresses_ShipmentAddesssId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_ShipmentAddesssId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "ShipmentAddesssId",
                table: "Shipments");
        }
    }
}
