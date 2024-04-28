using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB120 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeprecateSchedules_Assets_AssetId",
                table: "DeprecateSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeprecateSchedules",
                table: "DeprecateSchedules");

            migrationBuilder.RenameTable(
                name: "DeprecateSchedules",
                newName: "AssetDeprecateSchedules");

            migrationBuilder.RenameIndex(
                name: "IX_DeprecateSchedules_AssetId",
                table: "AssetDeprecateSchedules",
                newName: "IX_AssetDeprecateSchedules_AssetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetDeprecateSchedules",
                table: "AssetDeprecateSchedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetDeprecateSchedules_Assets_AssetId",
                table: "AssetDeprecateSchedules",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetDeprecateSchedules_Assets_AssetId",
                table: "AssetDeprecateSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetDeprecateSchedules",
                table: "AssetDeprecateSchedules");

            migrationBuilder.RenameTable(
                name: "AssetDeprecateSchedules",
                newName: "DeprecateSchedules");

            migrationBuilder.RenameIndex(
                name: "IX_AssetDeprecateSchedules_AssetId",
                table: "DeprecateSchedules",
                newName: "IX_DeprecateSchedules_AssetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeprecateSchedules",
                table: "DeprecateSchedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeprecateSchedules_Assets_AssetId",
                table: "DeprecateSchedules",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");
        }
    }
}
