using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB163 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayments_Accounts_PayFromAccountUid",
                table: "EmployeePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayments_EmployeePaymentPeriods_EmployeePaymentPeriodUid",
                table: "EmployeePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayments_Employees_EmployeeUid",
                table: "EmployeePayments");

            migrationBuilder.RenameColumn(
                name: "PayFromAccountUid",
                table: "EmployeePayments",
                newName: "PayFromAccountId");

            migrationBuilder.RenameColumn(
                name: "EmployeeUid",
                table: "EmployeePayments",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "EmployeePaymentPeriodUid",
                table: "EmployeePayments",
                newName: "EmployeePaymentPeriodId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeePayments_PayFromAccountUid",
                table: "EmployeePayments",
                newName: "IX_EmployeePayments_PayFromAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeePayments_EmployeeUid",
                table: "EmployeePayments",
                newName: "IX_EmployeePayments_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeePayments_EmployeePaymentPeriodUid",
                table: "EmployeePayments",
                newName: "IX_EmployeePayments_EmployeePaymentPeriodId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EmployeePayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayments_Accounts_PayFromAccountId",
                table: "EmployeePayments",
                column: "PayFromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayments_EmployeePaymentPeriods_EmployeePaymentPeriodId",
                table: "EmployeePayments",
                column: "EmployeePaymentPeriodId",
                principalTable: "EmployeePaymentPeriods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayments_Employees_EmployeeId",
                table: "EmployeePayments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayments_Accounts_PayFromAccountId",
                table: "EmployeePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayments_EmployeePaymentPeriods_EmployeePaymentPeriodId",
                table: "EmployeePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayments_Employees_EmployeeId",
                table: "EmployeePayments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "EmployeePayments");

            migrationBuilder.RenameColumn(
                name: "PayFromAccountId",
                table: "EmployeePayments",
                newName: "PayFromAccountUid");

            migrationBuilder.RenameColumn(
                name: "EmployeePaymentPeriodId",
                table: "EmployeePayments",
                newName: "EmployeePaymentPeriodUid");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "EmployeePayments",
                newName: "EmployeeUid");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeePayments_PayFromAccountId",
                table: "EmployeePayments",
                newName: "IX_EmployeePayments_PayFromAccountUid");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeePayments_EmployeePaymentPeriodId",
                table: "EmployeePayments",
                newName: "IX_EmployeePayments_EmployeePaymentPeriodUid");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeePayments_EmployeeId",
                table: "EmployeePayments",
                newName: "IX_EmployeePayments_EmployeeUid");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayments_Accounts_PayFromAccountUid",
                table: "EmployeePayments",
                column: "PayFromAccountUid",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayments_EmployeePaymentPeriods_EmployeePaymentPeriodUid",
                table: "EmployeePayments",
                column: "EmployeePaymentPeriodUid",
                principalTable: "EmployeePaymentPeriods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayments_Employees_EmployeeUid",
                table: "EmployeePayments",
                column: "EmployeeUid",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
