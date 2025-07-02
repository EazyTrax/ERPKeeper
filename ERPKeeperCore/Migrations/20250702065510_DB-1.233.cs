using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1233 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "Shipments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_TransactionId",
                table: "Shipments",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Sales_TransactionId",
                table: "Shipments",
                column: "TransactionId",
                principalTable: "Sales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Sales_TransactionId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_TransactionId",
                table: "Shipments");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "Shipments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
