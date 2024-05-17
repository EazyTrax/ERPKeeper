using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1135 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "SaleQuotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProjectId",
                table: "Sales",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleQuotes_ProjectId",
                table: "SaleQuotes",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleQuotes_Projects_ProjectId",
                table: "SaleQuotes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Projects_ProjectId",
                table: "Sales",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleQuotes_Projects_ProjectId",
                table: "SaleQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Projects_ProjectId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ProjectId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_SaleQuotes_ProjectId",
                table: "SaleQuotes");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "SaleQuotes");
        }
    }
}
