using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB176 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePaymentTypes_Accounts_AccountUid",
                table: "EmployeePaymentTypes");

            migrationBuilder.RenameColumn(
                name: "AccountUid",
                table: "EmployeePaymentTypes",
                newName: "ExpenseAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeePaymentTypes_AccountUid",
                table: "EmployeePaymentTypes",
                newName: "IX_EmployeePaymentTypes_ExpenseAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePaymentTypes_Accounts_ExpenseAccountId",
                table: "EmployeePaymentTypes",
                column: "ExpenseAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePaymentTypes_Accounts_ExpenseAccountId",
                table: "EmployeePaymentTypes");

            migrationBuilder.RenameColumn(
                name: "ExpenseAccountId",
                table: "EmployeePaymentTypes",
                newName: "AccountUid");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeePaymentTypes_ExpenseAccountId",
                table: "EmployeePaymentTypes",
                newName: "IX_EmployeePaymentTypes_AccountUid");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePaymentTypes_Accounts_AccountUid",
                table: "EmployeePaymentTypes",
                column: "AccountUid",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
