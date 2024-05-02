using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB143 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLedgers_Accounts_TransactionId",
                table: "TransactionLedgers");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLedgers_AccountId",
                table: "TransactionLedgers",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLedgers_Accounts_AccountId",
                table: "TransactionLedgers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLedgers_Accounts_AccountId",
                table: "TransactionLedgers");

            migrationBuilder.DropIndex(
                name: "IX_TransactionLedgers_AccountId",
                table: "TransactionLedgers");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLedgers_Accounts_TransactionId",
                table: "TransactionLedgers",
                column: "TransactionId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
