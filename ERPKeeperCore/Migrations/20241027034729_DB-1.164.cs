using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1164 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPosted",
                table: "RetentionGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PayDate",
                table: "RetentionGroups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PayFromAccountId",
                table: "RetentionGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostedDate",
                table: "RetentionGroups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "RetentionGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetentionGroups_PayFromAccountId",
                table: "RetentionGroups",
                column: "PayFromAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RetentionGroups_TransactionId",
                table: "RetentionGroups",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RetentionGroups_Accounts_PayFromAccountId",
                table: "RetentionGroups",
                column: "PayFromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RetentionGroups_Transactions_TransactionId",
                table: "RetentionGroups",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RetentionGroups_Accounts_PayFromAccountId",
                table: "RetentionGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_RetentionGroups_Transactions_TransactionId",
                table: "RetentionGroups");

            migrationBuilder.DropIndex(
                name: "IX_RetentionGroups_PayFromAccountId",
                table: "RetentionGroups");

            migrationBuilder.DropIndex(
                name: "IX_RetentionGroups_TransactionId",
                table: "RetentionGroups");

            migrationBuilder.DropColumn(
                name: "IsPosted",
                table: "RetentionGroups");

            migrationBuilder.DropColumn(
                name: "PayDate",
                table: "RetentionGroups");

            migrationBuilder.DropColumn(
                name: "PayFromAccountId",
                table: "RetentionGroups");

            migrationBuilder.DropColumn(
                name: "PostedDate",
                table: "RetentionGroups");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "RetentionGroups");
        }
    }
}
