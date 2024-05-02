using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB136 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeePositions_EmployeePositionId",
                table: "Employees");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeePositionId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeePositions_EmployeePositionId",
                table: "Employees",
                column: "EmployeePositionId",
                principalTable: "EmployeePositions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeePositions_EmployeePositionId",
                table: "Employees");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeePositionId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeePositions_EmployeePositionId",
                table: "Employees",
                column: "EmployeePositionId",
                principalTable: "EmployeePositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
