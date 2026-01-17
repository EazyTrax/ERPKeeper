using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class RenameEmployeeLeaveToEmployeeDailyRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename the table
            migrationBuilder.RenameTable(
                name: "EmployeeLeaves",
                newName: "EmployeeDailyRecords");

            // Rename the column
            migrationBuilder.RenameColumn(
                name: "LeaveDate",
                table: "EmployeeDailyRecords",
                newName: "RecordDate");

            // Drop old columns that don't exist in new model
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "EmployeeDailyRecords");

            // Add new columns
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "EmployeeDailyRecords",
                type: "nvarchar(max)",
                nullable: true);

            // Rename index
            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaves_EmployeeId",
                table: "EmployeeDailyRecords",
                newName: "IX_EmployeeDailyRecords_EmployeeId");

            // Rename primary key
            migrationBuilder.RenameIndex(
                name: "PK_EmployeeLeaves",
                table: "EmployeeDailyRecords",
                newName: "PK_EmployeeDailyRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse the changes
            migrationBuilder.RenameTable(
                name: "EmployeeDailyRecords",
                newName: "EmployeeLeaves");

            migrationBuilder.RenameColumn(
                name: "RecordDate",
                table: "EmployeeLeaves",
                newName: "LeaveDate");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "EmployeeLeaves");

            migrationBuilder.AddColumn<int>(
                name: "Reason",
                table: "EmployeeLeaves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeDailyRecords_EmployeeId",
                table: "EmployeeLeaves",
                newName: "IX_EmployeeLeaves_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "PK_EmployeeDailyRecords",
                table: "EmployeeLeaves",
                newName: "PK_EmployeeLeaves");
        }
    }
}
