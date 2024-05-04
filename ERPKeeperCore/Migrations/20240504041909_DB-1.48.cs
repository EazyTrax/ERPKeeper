using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB148 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Accounts_ReceivableAccountId",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "ReceivableAccountId",
                table: "Purchases",
                newName: "PayableAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_ReceivableAccountId",
                table: "Purchases",
                newName: "IX_Purchases_PayableAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Accounts_PayableAccountId",
                table: "Purchases",
                column: "PayableAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Accounts_PayableAccountId",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "PayableAccountId",
                table: "Purchases",
                newName: "ReceivableAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_PayableAccountId",
                table: "Purchases",
                newName: "IX_Purchases_ReceivableAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Accounts_ReceivableAccountId",
                table: "Purchases",
                column: "ReceivableAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
