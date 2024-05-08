using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB198 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiabilityPaymentPayFromAccount_Accounts_AccountId",
                table: "LiabilityPaymentPayFromAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_LiabilityPaymentPayFromAccount_LiabilityPayments_LiabilityPaymentId",
                table: "LiabilityPaymentPayFromAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LiabilityPaymentPayFromAccount",
                table: "LiabilityPaymentPayFromAccount");

            migrationBuilder.RenameTable(
                name: "LiabilityPaymentPayFromAccount",
                newName: "LiabilityPaymentPayFromAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_LiabilityPaymentPayFromAccount_LiabilityPaymentId",
                table: "LiabilityPaymentPayFromAccounts",
                newName: "IX_LiabilityPaymentPayFromAccounts_LiabilityPaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_LiabilityPaymentPayFromAccount_AccountId",
                table: "LiabilityPaymentPayFromAccounts",
                newName: "IX_LiabilityPaymentPayFromAccounts_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LiabilityPaymentPayFromAccounts",
                table: "LiabilityPaymentPayFromAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LiabilityPaymentPayFromAccounts_Accounts_AccountId",
                table: "LiabilityPaymentPayFromAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LiabilityPaymentPayFromAccounts_LiabilityPayments_LiabilityPaymentId",
                table: "LiabilityPaymentPayFromAccounts",
                column: "LiabilityPaymentId",
                principalTable: "LiabilityPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiabilityPaymentPayFromAccounts_Accounts_AccountId",
                table: "LiabilityPaymentPayFromAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_LiabilityPaymentPayFromAccounts_LiabilityPayments_LiabilityPaymentId",
                table: "LiabilityPaymentPayFromAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LiabilityPaymentPayFromAccounts",
                table: "LiabilityPaymentPayFromAccounts");

            migrationBuilder.RenameTable(
                name: "LiabilityPaymentPayFromAccounts",
                newName: "LiabilityPaymentPayFromAccount");

            migrationBuilder.RenameIndex(
                name: "IX_LiabilityPaymentPayFromAccounts_LiabilityPaymentId",
                table: "LiabilityPaymentPayFromAccount",
                newName: "IX_LiabilityPaymentPayFromAccount_LiabilityPaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_LiabilityPaymentPayFromAccounts_AccountId",
                table: "LiabilityPaymentPayFromAccount",
                newName: "IX_LiabilityPaymentPayFromAccount_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LiabilityPaymentPayFromAccount",
                table: "LiabilityPaymentPayFromAccount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LiabilityPaymentPayFromAccount_Accounts_AccountId",
                table: "LiabilityPaymentPayFromAccount",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LiabilityPaymentPayFromAccount_LiabilityPayments_LiabilityPaymentId",
                table: "LiabilityPaymentPayFromAccount",
                column: "LiabilityPaymentId",
                principalTable: "LiabilityPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
