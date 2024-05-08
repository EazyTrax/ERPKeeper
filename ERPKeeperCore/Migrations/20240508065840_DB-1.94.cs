using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB194 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lends_Accounts_LendingAccountId",
                table: "Lends");

            migrationBuilder.DropForeignKey(
                name: "FK_Lends_Accounts_PayingAccountId",
                table: "Lends");

            migrationBuilder.RenameColumn(
                name: "PayingAccountId",
                table: "Lends",
                newName: "PayFrom_AssetAccountId");

            migrationBuilder.RenameColumn(
                name: "LendingAccountId",
                table: "Lends",
                newName: "Lending_AssetAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Lends_PayingAccountId",
                table: "Lends",
                newName: "IX_Lends_PayFrom_AssetAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Lends_LendingAccountId",
                table: "Lends",
                newName: "IX_Lends_Lending_AssetAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lends_Accounts_Lending_AssetAccountId",
                table: "Lends",
                column: "Lending_AssetAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lends_Accounts_PayFrom_AssetAccountId",
                table: "Lends",
                column: "PayFrom_AssetAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lends_Accounts_Lending_AssetAccountId",
                table: "Lends");

            migrationBuilder.DropForeignKey(
                name: "FK_Lends_Accounts_PayFrom_AssetAccountId",
                table: "Lends");

            migrationBuilder.RenameColumn(
                name: "PayFrom_AssetAccountId",
                table: "Lends",
                newName: "PayingAccountId");

            migrationBuilder.RenameColumn(
                name: "Lending_AssetAccountId",
                table: "Lends",
                newName: "LendingAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Lends_PayFrom_AssetAccountId",
                table: "Lends",
                newName: "IX_Lends_PayingAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Lends_Lending_AssetAccountId",
                table: "Lends",
                newName: "IX_Lends_LendingAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lends_Accounts_LendingAccountId",
                table: "Lends",
                column: "LendingAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lends_Accounts_PayingAccountId",
                table: "Lends",
                column: "PayingAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
