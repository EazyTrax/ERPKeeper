using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1133 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierQuoteItems");

            migrationBuilder.DropTable(
                name: "SupplierQuotes");

            migrationBuilder.CreateTable(
                name: "PurchaseQuotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    No = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LinesTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplierAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseQuotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseQuotes_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseQuotes_TaxCodes_TaxCodeId",
                        column: x => x.TaxCodeId,
                        principalTable: "TaxCodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseQuoteItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplierQuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseQuoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseQuoteItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseQuoteItems_PurchaseQuotes_SupplierQuoteId",
                        column: x => x.SupplierQuoteId,
                        principalTable: "PurchaseQuotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseQuoteItems_ItemId",
                table: "PurchaseQuoteItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseQuoteItems_SupplierQuoteId",
                table: "PurchaseQuoteItems",
                column: "SupplierQuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseQuotes_SupplierId",
                table: "PurchaseQuotes",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseQuotes_TaxCodeId",
                table: "PurchaseQuotes",
                column: "TaxCodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseQuoteItems");

            migrationBuilder.DropTable(
                name: "PurchaseQuotes");

            migrationBuilder.CreateTable(
                name: "SupplierQuotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LinesTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    No = table.Column<int>(type: "int", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SupplierAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierQuotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierQuotes_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupplierQuotes_TaxCodes_TaxCodeId",
                        column: x => x.TaxCodeId,
                        principalTable: "TaxCodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierQuoteItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierQuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierQuoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierQuoteItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierQuoteItems_SupplierQuotes_SupplierQuoteId",
                        column: x => x.SupplierQuoteId,
                        principalTable: "SupplierQuotes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierQuoteItems_ItemId",
                table: "SupplierQuoteItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierQuoteItems_SupplierQuoteId",
                table: "SupplierQuoteItems",
                column: "SupplierQuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierQuotes_SupplierId",
                table: "SupplierQuotes",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierQuotes_TaxCodeId",
                table: "SupplierQuotes",
                column: "TaxCodeId");
        }
    }
}
