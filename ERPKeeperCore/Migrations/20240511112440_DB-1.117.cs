using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1117 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObsoleteAssets_Assets_Id",
                table: "ObsoleteAssets");

            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                table: "ObsoleteAssets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ObsoleteAssets_AssetId",
                table: "ObsoleteAssets",
                column: "AssetId",
                unique: true,
                filter: "[AssetId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ObsoleteAssets_Assets_AssetId",
                table: "ObsoleteAssets",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObsoleteAssets_Assets_AssetId",
                table: "ObsoleteAssets");

            migrationBuilder.DropIndex(
                name: "IX_ObsoleteAssets_AssetId",
                table: "ObsoleteAssets");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "ObsoleteAssets");

            migrationBuilder.AddForeignKey(
                name: "FK_ObsoleteAssets_Assets_Id",
                table: "ObsoleteAssets",
                column: "Id",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
