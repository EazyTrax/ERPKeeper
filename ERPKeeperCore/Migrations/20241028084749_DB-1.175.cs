using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1175 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerAddressId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CustomerAddressId",
                table: "SaleQuotes");

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDate",
                table: "SaleQuotes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDate",
                table: "PurchaseQuotes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "PurchaseQuotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ProjectId",
                table: "Purchases",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseQuotes_ProjectId",
                table: "PurchaseQuotes",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseQuotes_Projects_ProjectId",
                table: "PurchaseQuotes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Projects_ProjectId",
                table: "Purchases",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseQuotes_Projects_ProjectId",
                table: "PurchaseQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Projects_ProjectId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ProjectId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseQuotes_ProjectId",
                table: "PurchaseQuotes");

            migrationBuilder.DropColumn(
                name: "CloseDate",
                table: "SaleQuotes");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "CloseDate",
                table: "PurchaseQuotes");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "PurchaseQuotes");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerAddressId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerAddressId",
                table: "SaleQuotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
