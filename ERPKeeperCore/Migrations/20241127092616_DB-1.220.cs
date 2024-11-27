using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1220 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DefaultProductItemId",
                table: "Suppliers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_DefaultProductItemId",
                table: "Suppliers",
                column: "DefaultProductItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Items_DefaultProductItemId",
                table: "Suppliers",
                column: "DefaultProductItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Items_DefaultProductItemId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_DefaultProductItemId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "DefaultProductItemId",
                table: "Suppliers");
        }
    }
}
