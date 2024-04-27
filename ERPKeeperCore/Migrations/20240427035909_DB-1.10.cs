using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB110 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Profiles_Id",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItem_Items_ItemId",
                table: "PurchaseItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItem_Purchases_PurchaseId",
                table: "PurchaseItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Profiles_Id",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_TaxCodes_DefaultTaxCodeUid",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseItem",
                table: "PurchaseItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Supplier",
                newName: "Suppliers");

            migrationBuilder.RenameTable(
                name: "PurchaseItem",
                newName: "PurchaseItems");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_DefaultTaxCodeUid",
                table: "Suppliers",
                newName: "IX_Suppliers_DefaultTaxCodeUid");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseItem_PurchaseId",
                table: "PurchaseItems",
                newName: "IX_PurchaseItems_PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseItem_ItemId",
                table: "PurchaseItems",
                newName: "IX_PurchaseItems_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Profiles_Id",
                table: "Customers",
                column: "Id",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Items_ItemId",
                table: "PurchaseItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Purchases_PurchaseId",
                table: "PurchaseItems",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Profiles_Id",
                table: "Suppliers",
                column: "Id",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_TaxCodes_DefaultTaxCodeUid",
                table: "Suppliers",
                column: "DefaultTaxCodeUid",
                principalTable: "TaxCodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Profiles_Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Items_ItemId",
                table: "PurchaseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Purchases_PurchaseId",
                table: "PurchaseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Profiles_Id",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_TaxCodes_DefaultTaxCodeUid",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "Supplier");

            migrationBuilder.RenameTable(
                name: "PurchaseItems",
                newName: "PurchaseItem");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_DefaultTaxCodeUid",
                table: "Supplier",
                newName: "IX_Supplier_DefaultTaxCodeUid");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseItems_PurchaseId",
                table: "PurchaseItem",
                newName: "IX_PurchaseItem_PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseItems_ItemId",
                table: "PurchaseItem",
                newName: "IX_PurchaseItem_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseItem",
                table: "PurchaseItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Profiles_Id",
                table: "Customer",
                column: "Id",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItem_Items_ItemId",
                table: "PurchaseItem",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItem_Purchases_PurchaseId",
                table: "PurchaseItem",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Profiles_Id",
                table: "Supplier",
                column: "Id",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_TaxCodes_DefaultTaxCodeUid",
                table: "Supplier",
                column: "DefaultTaxCodeUid",
                principalTable: "TaxCodes",
                principalColumn: "Id");
        }
    }
}
