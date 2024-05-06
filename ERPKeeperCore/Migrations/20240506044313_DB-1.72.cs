using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB172 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_Accounts_IncomeTaxExpenseAccountGuid",
                table: "IncomeTaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_Accounts_LiabilityAccountGuid",
                table: "IncomeTaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_FiscalYears_FiscalYearUid",
                table: "IncomeTaxes");

            migrationBuilder.RenameColumn(
                name: "TrDate",
                table: "IncomeTaxes",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "PayFromAccountGuid",
                table: "IncomeTaxes",
                newName: "TransactionId");

            migrationBuilder.RenameColumn(
                name: "LiabilityAccountGuid",
                table: "IncomeTaxes",
                newName: "LiabilityAccountId");

            migrationBuilder.RenameColumn(
                name: "IncomeTaxExpenseAccountGuid",
                table: "IncomeTaxes",
                newName: "IncomeTaxExpenseAccountId");

            migrationBuilder.RenameColumn(
                name: "FiscalYearUid",
                table: "IncomeTaxes",
                newName: "FiscalYearId");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "IncomeTaxes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeTaxes_LiabilityAccountGuid",
                table: "IncomeTaxes",
                newName: "IX_IncomeTaxes_LiabilityAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeTaxes_IncomeTaxExpenseAccountGuid",
                table: "IncomeTaxes",
                newName: "IX_IncomeTaxes_IncomeTaxExpenseAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeTaxes_FiscalYearUid",
                table: "IncomeTaxes",
                newName: "IX_IncomeTaxes_FiscalYearId");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "IncomeTaxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTaxes_TransactionId",
                table: "IncomeTaxes",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_Accounts_IncomeTaxExpenseAccountId",
                table: "IncomeTaxes",
                column: "IncomeTaxExpenseAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_Accounts_LiabilityAccountId",
                table: "IncomeTaxes",
                column: "LiabilityAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_FiscalYears_FiscalYearId",
                table: "IncomeTaxes",
                column: "FiscalYearId",
                principalTable: "FiscalYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_Transactions_TransactionId",
                table: "IncomeTaxes",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_Accounts_IncomeTaxExpenseAccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_Accounts_LiabilityAccountId",
                table: "IncomeTaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_FiscalYears_FiscalYearId",
                table: "IncomeTaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_Transactions_TransactionId",
                table: "IncomeTaxes");

            migrationBuilder.DropIndex(
                name: "IX_IncomeTaxes_TransactionId",
                table: "IncomeTaxes");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "IncomeTaxes");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "IncomeTaxes",
                newName: "PayFromAccountGuid");

            migrationBuilder.RenameColumn(
                name: "LiabilityAccountId",
                table: "IncomeTaxes",
                newName: "LiabilityAccountGuid");

            migrationBuilder.RenameColumn(
                name: "IncomeTaxExpenseAccountId",
                table: "IncomeTaxes",
                newName: "IncomeTaxExpenseAccountGuid");

            migrationBuilder.RenameColumn(
                name: "FiscalYearId",
                table: "IncomeTaxes",
                newName: "FiscalYearUid");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "IncomeTaxes",
                newName: "TrDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "IncomeTaxes",
                newName: "Uid");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeTaxes_LiabilityAccountId",
                table: "IncomeTaxes",
                newName: "IX_IncomeTaxes_LiabilityAccountGuid");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeTaxes_IncomeTaxExpenseAccountId",
                table: "IncomeTaxes",
                newName: "IX_IncomeTaxes_IncomeTaxExpenseAccountGuid");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeTaxes_FiscalYearId",
                table: "IncomeTaxes",
                newName: "IX_IncomeTaxes_FiscalYearUid");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_Accounts_IncomeTaxExpenseAccountGuid",
                table: "IncomeTaxes",
                column: "IncomeTaxExpenseAccountGuid",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_Accounts_LiabilityAccountGuid",
                table: "IncomeTaxes",
                column: "LiabilityAccountGuid",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_FiscalYears_FiscalYearUid",
                table: "IncomeTaxes",
                column: "FiscalYearUid",
                principalTable: "FiscalYears",
                principalColumn: "Id");
        }
    }
}
