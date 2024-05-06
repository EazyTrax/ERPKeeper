using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB169 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "JournalEntryType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystem",
                table: "JournalEntryType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "JournalEntries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "JournalEntryType");

            migrationBuilder.DropColumn(
                name: "IsSystem",
                table: "JournalEntryType");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "JournalEntries");
        }
    }
}
