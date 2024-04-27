using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "ERP_Fiscal_Years",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ERP_Fiscal_Years");
        }
    }
}
