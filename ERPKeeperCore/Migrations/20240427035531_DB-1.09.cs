using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB109 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalSale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CountBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CountSales = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Profiles_Id",
                        column: x => x.Id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DefaultTaxCodeUid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CountPurchases = table.Column<int>(type: "int", nullable: false),
                    SumPurchaseBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CountBalance = table.Column<int>(type: "int", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditAgeLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplier_Profiles_Id",
                        column: x => x.Id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supplier_TaxCodes_DefaultTaxCodeUid",
                        column: x => x.DefaultTaxCodeUid,
                        principalTable: "TaxCodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_DefaultTaxCodeUid",
                table: "Supplier",
                column: "DefaultTaxCodeUid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
