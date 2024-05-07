using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB183 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPosted",
                table: "AssetDeprecateSchedules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "AssetDeprecateSchedules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_TransactionId",
                table: "Assets",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDeprecateSchedules_TransactionId",
                table: "AssetDeprecateSchedules",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetDeprecateSchedules_Transactions_TransactionId",
                table: "AssetDeprecateSchedules",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Transactions_TransactionId",
                table: "Assets",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetDeprecateSchedules_Transactions_TransactionId",
                table: "AssetDeprecateSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Transactions_TransactionId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_TransactionId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_AssetDeprecateSchedules_TransactionId",
                table: "AssetDeprecateSchedules");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "IsPosted",
                table: "AssetDeprecateSchedules");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "AssetDeprecateSchedules");
        }
    }
}
