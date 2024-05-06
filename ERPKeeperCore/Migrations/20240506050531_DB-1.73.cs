using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB173 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_Accounts_IncomeTaxExpenseAccountId",
                table: "IncomeTaxes");

            migrationBuilder.RenameColumn(
                name: "IncomeTaxExpenseAccountId",
                table: "IncomeTaxes",
                newName: "ExpenseAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeTaxes_IncomeTaxExpenseAccountId",
                table: "IncomeTaxes",
                newName: "IX_IncomeTaxes_ExpenseAccountId");

            migrationBuilder.AlterColumn<string>(
                name: "Memo",
                table: "IncomeTaxes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Memo",
                table: "EmployeePaymentItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_Accounts_ExpenseAccountId",
                table: "IncomeTaxes",
                column: "ExpenseAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTaxes_Accounts_ExpenseAccountId",
                table: "IncomeTaxes");

            migrationBuilder.RenameColumn(
                name: "ExpenseAccountId",
                table: "IncomeTaxes",
                newName: "IncomeTaxExpenseAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeTaxes_ExpenseAccountId",
                table: "IncomeTaxes",
                newName: "IX_IncomeTaxes_IncomeTaxExpenseAccountId");

            migrationBuilder.AlterColumn<string>(
                name: "Memo",
                table: "IncomeTaxes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Memo",
                table: "EmployeePaymentItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTaxes_Accounts_IncomeTaxExpenseAccountId",
                table: "IncomeTaxes",
                column: "IncomeTaxExpenseAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
