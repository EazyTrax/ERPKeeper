using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB155 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RetentionTypeId",
                table: "SupplierPayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RetentionTypeId",
                table: "ReceivePayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RetentionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RetentionToAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetentionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetentionTypes_Accounts_RetentionToAccountId",
                        column: x => x.RetentionToAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_RetentionTypeId",
                table: "SupplierPayments",
                column: "RetentionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePayments_RetentionTypeId",
                table: "ReceivePayments",
                column: "RetentionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RetentionTypes_RetentionToAccountId",
                table: "RetentionTypes",
                column: "RetentionToAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_RetentionTypes_RetentionTypeId",
                table: "ReceivePayments",
                column: "RetentionTypeId",
                principalTable: "RetentionTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_RetentionTypes_RetentionTypeId",
                table: "SupplierPayments",
                column: "RetentionTypeId",
                principalTable: "RetentionTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_RetentionTypes_RetentionTypeId",
                table: "ReceivePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_RetentionTypes_RetentionTypeId",
                table: "SupplierPayments");

            migrationBuilder.DropTable(
                name: "RetentionTypes");

            migrationBuilder.DropIndex(
                name: "IX_SupplierPayments_RetentionTypeId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_ReceivePayments_RetentionTypeId",
                table: "ReceivePayments");

            migrationBuilder.DropColumn(
                name: "RetentionTypeId",
                table: "SupplierPayments");

            migrationBuilder.DropColumn(
                name: "RetentionTypeId",
                table: "ReceivePayments");
        }
    }
}
