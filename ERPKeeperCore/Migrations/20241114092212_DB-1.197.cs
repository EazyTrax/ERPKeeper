using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1197 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_ShipmentProviders_ShipmentProviderId",
                table: "Shipments");

            migrationBuilder.DropTable(
                name: "ShipmentProviders");

            migrationBuilder.CreateTable(
                name: "LogisticProviders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticProviders", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_LogisticProviders_ShipmentProviderId",
                table: "Shipments",
                column: "ShipmentProviderId",
                principalTable: "LogisticProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_LogisticProviders_ShipmentProviderId",
                table: "Shipments");

            migrationBuilder.DropTable(
                name: "LogisticProviders");

            migrationBuilder.CreateTable(
                name: "ShipmentProviders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentProviders", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_ShipmentProviders_ShipmentProviderId",
                table: "Shipments",
                column: "ShipmentProviderId",
                principalTable: "ShipmentProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
