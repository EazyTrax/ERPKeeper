using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1130 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerQuoteItems");

            migrationBuilder.DropTable(
                name: "CustomerQuotes");

            migrationBuilder.CreateTable(
                name: "SaleQuotes",
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
                    table.PrimaryKey("PK_SaleQuotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleQuotes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleQuotes_TaxCodes_TaxCodeId",
                        column: x => x.TaxCodeId,
                        principalTable: "TaxCodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SaleQuoteItems",
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
                    table.PrimaryKey("PK_SaleQuoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleQuoteItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleQuoteItems_SaleQuotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "SaleQuotes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleQuoteItems_ItemId",
                table: "SaleQuoteItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleQuoteItems_QuoteId",
                table: "SaleQuoteItems",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleQuotes_CustomerId",
                table: "SaleQuotes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleQuotes_TaxCodeId",
                table: "SaleQuotes",
                column: "TaxCodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleQuoteItems");

            migrationBuilder.DropTable(
                name: "SaleQuotes");

            migrationBuilder.CreateTable(
                name: "CustomerQuotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    LinesTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    No = table.Column<int>(type: "int", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                name: "CustomerQuoteItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
        }
    }
}
