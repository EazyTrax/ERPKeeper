using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1224 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfileAddesssId",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileAddesssId",
                table: "PurchaseQuotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseId",
                table: "PurchaseQuotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ProfileAddesssId",
                table: "Purchases",
                column: "ProfileAddesssId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseQuotes_ProfileAddesssId",
                table: "PurchaseQuotes",
                column: "ProfileAddesssId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseQuotes_ProfileAddresses_ProfileAddesssId",
                table: "PurchaseQuotes",
                column: "ProfileAddesssId",
                principalTable: "ProfileAddresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_ProfileAddresses_ProfileAddesssId",
                table: "Purchases",
                column: "ProfileAddesssId",
                principalTable: "ProfileAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseQuotes_ProfileAddresses_ProfileAddesssId",
                table: "PurchaseQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_ProfileAddresses_ProfileAddesssId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ProfileAddesssId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseQuotes_ProfileAddesssId",
                table: "PurchaseQuotes");

            migrationBuilder.DropColumn(
                name: "ProfileAddesssId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ProfileAddesssId",
                table: "PurchaseQuotes");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "PurchaseQuotes");
        }
    }
}
