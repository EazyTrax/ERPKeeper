using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB116 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalDebit",
                table: "Transactions",
                newName: "Debit");

            migrationBuilder.RenameColumn(
                name: "TotalCredit",
                table: "Transactions",
                newName: "Credit");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Debit",
                table: "Transactions",
                newName: "TotalDebit");

            migrationBuilder.RenameColumn(
                name: "Credit",
                table: "Transactions",
                newName: "TotalCredit");
        }
    }
}
