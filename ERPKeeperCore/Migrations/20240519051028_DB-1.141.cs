using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1141 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerItem_Customers_CustomerId",
                table: "CustomerItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerItem_Items_ItemId",
                table: "CustomerItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierItem_Items_ItemId",
                table: "SupplierItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierItem_Suppliers_SupplierId",
                table: "SupplierItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierItem",
                table: "SupplierItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerItem",
                table: "CustomerItem");

            migrationBuilder.RenameTable(
                name: "SupplierItem",
                newName: "SupplierItems");

            migrationBuilder.RenameTable(
                name: "CustomerItem",
                newName: "CustomerItems");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierItem_SupplierId",
                table: "SupplierItems",
                newName: "IX_SupplierItems_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierItem_ItemId",
                table: "SupplierItems",
                newName: "IX_SupplierItems_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerItem_ItemId",
                table: "CustomerItems",
                newName: "IX_CustomerItems_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerItem_CustomerId",
                table: "CustomerItems",
                newName: "IX_CustomerItems_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierItems",
                table: "SupplierItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerItems",
                table: "CustomerItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerItems_Customers_CustomerId",
                table: "CustomerItems",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerItems_Items_ItemId",
                table: "CustomerItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierItems_Items_ItemId",
                table: "SupplierItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierItems_Suppliers_SupplierId",
                table: "SupplierItems",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerItems_Customers_CustomerId",
                table: "CustomerItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerItems_Items_ItemId",
                table: "CustomerItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierItems_Items_ItemId",
                table: "SupplierItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierItems_Suppliers_SupplierId",
                table: "SupplierItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierItems",
                table: "SupplierItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerItems",
                table: "CustomerItems");

            migrationBuilder.RenameTable(
                name: "SupplierItems",
                newName: "SupplierItem");

            migrationBuilder.RenameTable(
                name: "CustomerItems",
                newName: "CustomerItem");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierItems_SupplierId",
                table: "SupplierItem",
                newName: "IX_SupplierItem_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierItems_ItemId",
                table: "SupplierItem",
                newName: "IX_SupplierItem_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerItems_ItemId",
                table: "CustomerItem",
                newName: "IX_CustomerItem_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerItems_CustomerId",
                table: "CustomerItem",
                newName: "IX_CustomerItem_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierItem",
                table: "SupplierItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerItem",
                table: "CustomerItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerItem_Customers_CustomerId",
                table: "CustomerItem",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerItem_Items_ItemId",
                table: "CustomerItem",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierItem_Items_ItemId",
                table: "SupplierItem",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierItem_Suppliers_SupplierId",
                table: "SupplierItem",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
