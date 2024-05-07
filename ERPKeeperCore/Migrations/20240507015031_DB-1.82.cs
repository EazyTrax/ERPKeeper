using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB182 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeePayments_TransactionId",
                table: "EmployeePayments");

            migrationBuilder.AddColumn<int>(
                name: "PaymentCount",
                table: "EmployeePaymentPeriods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayments_TransactionId",
                table: "EmployeePayments",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeePayments_TransactionId",
                table: "EmployeePayments");

            migrationBuilder.DropColumn(
                name: "PaymentCount",
                table: "EmployeePaymentPeriods");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayments_TransactionId",
                table: "EmployeePayments",
                column: "TransactionId");
        }
    }
}
