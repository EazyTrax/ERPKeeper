using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB151 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WithDrawAccountId",
                table: "FundTransfers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfers_WithDrawAccountId",
                table: "FundTransfers",
                column: "WithDrawAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_FundTransfers_Accounts_WithDrawAccountId",
                table: "FundTransfers",
                column: "WithDrawAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FundTransfers_Accounts_WithDrawAccountId",
                table: "FundTransfers");

            migrationBuilder.DropIndex(
                name: "IX_FundTransfers_WithDrawAccountId",
                table: "FundTransfers");

            migrationBuilder.DropColumn(
                name: "WithDrawAccountId",
                table: "FundTransfers");
        }
    }
}
