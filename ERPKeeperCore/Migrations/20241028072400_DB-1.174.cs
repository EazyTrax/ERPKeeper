using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1174 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfileAddesssId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileAddesssId",
                table: "SaleQuotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProfileAddesssId",
                table: "Sales",
                column: "ProfileAddesssId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleQuotes_ProfileAddesssId",
                table: "SaleQuotes",
                column: "ProfileAddesssId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleQuotes_ProfileAddresses_ProfileAddesssId",
                table: "SaleQuotes",
                column: "ProfileAddesssId",
                principalTable: "ProfileAddresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_ProfileAddresses_ProfileAddesssId",
                table: "Sales",
                column: "ProfileAddesssId",
                principalTable: "ProfileAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleQuotes_ProfileAddresses_ProfileAddesssId",
                table: "SaleQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_ProfileAddresses_ProfileAddesssId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ProfileAddesssId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_SaleQuotes_ProfileAddesssId",
                table: "SaleQuotes");

            migrationBuilder.DropColumn(
                name: "ProfileAddesssId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ProfileAddesssId",
                table: "SaleQuotes");
        }
    }
}
