using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB2026030811 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "leaveAmount",
                table: "LeaveRecords",
                newName: "LeaveAmount");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "WorkRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkAmount",
                table: "WorkRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "WorkRecords");

            migrationBuilder.DropColumn(
                name: "WorkAmount",
                table: "WorkRecords");

            migrationBuilder.RenameColumn(
                name: "LeaveAmount",
                table: "LeaveRecords",
                newName: "leaveAmount");
        }
    }
}
