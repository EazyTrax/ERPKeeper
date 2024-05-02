using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB135 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerQuotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    No = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LinesTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerQuotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerQuotes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerQuotes_TaxCodes_TaxCodeId",
                        column: x => x.TaxCodeId,
                        principalTable: "TaxCodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierQuotes",
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
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplierAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                name: "CustomerQuoteItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerQuoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerQuoteItems_CustomerQuotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "CustomerQuotes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerQuoteItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierQuoteItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierQuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_CustomerQuoteItems_ItemId",
                table: "CustomerQuoteItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuoteItems_QuoteId",
                table: "CustomerQuoteItems",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuotes_CustomerId",
                table: "CustomerQuotes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuotes_TaxCodeId",
                table: "CustomerQuotes",
                column: "TaxCodeId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerQuoteItems");

            migrationBuilder.DropTable(
                name: "SupplierQuoteItems");

            migrationBuilder.DropTable(
                name: "CustomerQuotes");

            migrationBuilder.DropTable(
                name: "SupplierQuotes");
        }
    }
}
