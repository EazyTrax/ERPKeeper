using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1138 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RetentionGroupId",
                table: "SupplierPayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RetentionGroupId",
                table: "ReceivePayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RetentionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RetentionTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountCommercial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountRetention = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    No = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetentionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetentionGroups_RetentionTypes_RetentionTypeId",
                        column: x => x.RetentionTypeId,
                        principalTable: "RetentionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_RetentionGroupId",
                table: "SupplierPayments",
                column: "RetentionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePayments_RetentionGroupId",
                table: "ReceivePayments",
                column: "RetentionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RetentionGroups_RetentionTypeId",
                table: "RetentionGroups",
                column: "RetentionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_RetentionGroups_RetentionGroupId",
                table: "ReceivePayments",
                column: "RetentionGroupId",
                principalTable: "RetentionGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_RetentionGroups_RetentionGroupId",
                table: "SupplierPayments",
                column: "RetentionGroupId",
                principalTable: "RetentionGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_RetentionGroups_RetentionGroupId",
                table: "ReceivePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_RetentionGroups_RetentionGroupId",
                table: "SupplierPayments");

            migrationBuilder.DropTable(
                name: "RetentionGroups");

            migrationBuilder.DropIndex(
                name: "IX_SupplierPayments_RetentionGroupId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_ReceivePayments_RetentionGroupId",
                table: "ReceivePayments");

            migrationBuilder.DropColumn(
                name: "RetentionGroupId",
                table: "SupplierPayments");

            migrationBuilder.DropColumn(
                name: "RetentionGroupId",
                table: "ReceivePayments");
        }
    }
}
