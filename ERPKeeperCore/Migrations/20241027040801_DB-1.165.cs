using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1165 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RetentionGroups_TransactionId",
                table: "RetentionGroups");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RetentionGroups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "RetentionGroups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetentionGroups_TransactionId",
                table: "RetentionGroups",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RetentionGroups_TransactionId",
                table: "RetentionGroups");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RetentionGroups");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "RetentionGroups");

            migrationBuilder.CreateIndex(
                name: "IX_RetentionGroups_TransactionId",
                table: "RetentionGroups",
                column: "TransactionId");
        }
    }
}
