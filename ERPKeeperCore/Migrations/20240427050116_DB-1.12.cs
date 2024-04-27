using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB112 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "TrnDate",
                table: "Sales",
                newName: "Date");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PostStatus = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_TransactionId",
                table: "Sales",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Transaction_TransactionId",
                table: "Sales",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Transaction_TransactionId",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_TransactionId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Sales",
                newName: "TrnDate");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Sales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
