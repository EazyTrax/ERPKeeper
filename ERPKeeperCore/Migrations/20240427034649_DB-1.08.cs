using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB108 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Projects_ProjectId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ProjectId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "FiscalYearId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Projects",
                newName: "StartDate");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Projects",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<Guid>(
                name: "FiscalYearId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProjectId",
                table: "Sales",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Projects_ProjectId",
                table: "Sales",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
