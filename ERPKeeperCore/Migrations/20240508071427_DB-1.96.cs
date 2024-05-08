using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB196 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetTypes_Accounts_PurchaseAccId",
                table: "AssetTypes");

            migrationBuilder.RenameColumn(
                name: "PurchaseAccId",
                table: "AssetTypes",
                newName: "Purchase_Asset_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetTypes_PurchaseAccId",
                table: "AssetTypes",
                newName: "IX_AssetTypes_Purchase_Asset_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTypes_Accounts_Purchase_Asset_AccountId",
                table: "AssetTypes",
                column: "Purchase_Asset_AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetTypes_Accounts_Purchase_Asset_AccountId",
                table: "AssetTypes");

            migrationBuilder.RenameColumn(
                name: "Purchase_Asset_AccountId",
                table: "AssetTypes",
                newName: "PurchaseAccId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetTypes_Purchase_Asset_AccountId",
                table: "AssetTypes",
                newName: "IX_AssetTypes_PurchaseAccId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTypes_Accounts_PurchaseAccId",
                table: "AssetTypes",
                column: "PurchaseAccId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
