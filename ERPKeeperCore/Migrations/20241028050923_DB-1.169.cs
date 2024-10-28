using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1169 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_Accounts_LiabilityAccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_Accounts_PayFrom_AssetAccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_RetentionGroups_RetentionGroupId",
                table: "ReceivePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_RetentionGroups_Accounts_PayFromAccountId",
                table: "RetentionGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_RetentionGroups_RetentionTypes_RetentionTypeId",
                table: "RetentionGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_RetentionGroups_Transactions_TransactionId",
                table: "RetentionGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_RetentionGroups_RetentionGroupId",
                table: "SupplierPayments");

            migrationBuilder.DropIndex(
                name: "IX_IncomeTaxes_LiabilityAccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropIndex(
                name: "IX_IncomeTaxes_PayFrom_AssetAccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RetentionGroups",
                table: "RetentionGroups");

            migrationBuilder.DropColumn(
                name: "LiabilityAccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropColumn(
                name: "PayFrom_AssetAccountId",
                table: "IncomeTaxes");

            migrationBuilder.RenameTable(
                name: "RetentionGroups",
                newName: "RetentionPeriods");

            migrationBuilder.RenameIndex(
                name: "IX_RetentionGroups_TransactionId",
                table: "RetentionPeriods",
                newName: "IX_RetentionPeriods_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_RetentionGroups_RetentionTypeId",
                table: "RetentionPeriods",
                newName: "IX_RetentionPeriods_RetentionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_RetentionGroups_PayFromAccountId",
                table: "RetentionPeriods",
                newName: "IX_RetentionPeriods_PayFromAccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RetentionPeriods",
                table: "RetentionPeriods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_RetentionPeriods_RetentionGroupId",
                table: "ReceivePayments",
                column: "RetentionGroupId",
                principalTable: "RetentionPeriods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RetentionPeriods_Accounts_PayFromAccountId",
                table: "RetentionPeriods",
                column: "PayFromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RetentionPeriods_RetentionTypes_RetentionTypeId",
                table: "RetentionPeriods",
                column: "RetentionTypeId",
                principalTable: "RetentionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetentionPeriods_Transactions_TransactionId",
                table: "RetentionPeriods",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_RetentionPeriods_RetentionGroupId",
                table: "SupplierPayments",
                column: "RetentionGroupId",
                principalTable: "RetentionPeriods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_RetentionPeriods_RetentionGroupId",
                table: "ReceivePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_RetentionPeriods_Accounts_PayFromAccountId",
                table: "RetentionPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_RetentionPeriods_RetentionTypes_RetentionTypeId",
                table: "RetentionPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_RetentionPeriods_Transactions_TransactionId",
                table: "RetentionPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierPayments_RetentionPeriods_RetentionGroupId",
                table: "SupplierPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RetentionPeriods",
                table: "RetentionPeriods");

            migrationBuilder.RenameTable(
                name: "RetentionPeriods",
                newName: "RetentionGroups");

            migrationBuilder.RenameIndex(
                name: "IX_RetentionPeriods_TransactionId",
                table: "RetentionGroups",
                newName: "IX_RetentionGroups_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_RetentionPeriods_RetentionTypeId",
                table: "RetentionGroups",
                newName: "IX_RetentionGroups_RetentionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_RetentionPeriods_PayFromAccountId",
                table: "RetentionGroups",
                newName: "IX_RetentionGroups_PayFromAccountId");

            migrationBuilder.AddColumn<Guid>(
                name: "LiabilityAccountId",
                table: "IncomeTaxes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PayFrom_AssetAccountId",
                table: "IncomeTaxes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RetentionGroups",
                table: "RetentionGroups",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTaxes_LiabilityAccountId",
                table: "IncomeTaxes",
                column: "LiabilityAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTaxes_PayFrom_AssetAccountId",
                table: "IncomeTaxes",
                column: "PayFrom_AssetAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_Accounts_LiabilityAccountId",
                table: "IncomeTaxes",
                column: "LiabilityAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_Accounts_PayFrom_AssetAccountId",
                table: "IncomeTaxes",
                column: "PayFrom_AssetAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_RetentionGroups_RetentionGroupId",
                table: "ReceivePayments",
                column: "RetentionGroupId",
                principalTable: "RetentionGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RetentionGroups_Accounts_PayFromAccountId",
                table: "RetentionGroups",
                column: "PayFromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RetentionGroups_RetentionTypes_RetentionTypeId",
                table: "RetentionGroups",
                column: "RetentionTypeId",
                principalTable: "RetentionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetentionGroups_Transactions_TransactionId",
                table: "RetentionGroups",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierPayments_RetentionGroups_RetentionGroupId",
                table: "SupplierPayments",
                column: "RetentionGroupId",
                principalTable: "RetentionGroups",
                principalColumn: "Id");
        }
    }
}
