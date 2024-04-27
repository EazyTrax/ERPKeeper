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
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntries_ERP_JournalEntry_Tpye_JournalEntryTypeId",
                table: "JournalEntries");

            migrationBuilder.DropTable(
                name: "ERP_JournalEntry_Tpye");

            migrationBuilder.CreateTable(
                name: "JournalEntryType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryType", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntries_JournalEntryType_JournalEntryTypeId",
                table: "JournalEntries",
                column: "JournalEntryTypeId",
                principalTable: "JournalEntryType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntries_JournalEntryType_JournalEntryTypeId",
                table: "JournalEntries");

            migrationBuilder.DropTable(
                name: "JournalEntryType");

            migrationBuilder.CreateTable(
                name: "ERP_JournalEntry_Tpye",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERP_JournalEntry_Tpye", x => x.Uid);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntries_ERP_JournalEntry_Tpye_JournalEntryTypeId",
                table: "JournalEntries",
                column: "JournalEntryTypeId",
                principalTable: "ERP_JournalEntry_Tpye",
                principalColumn: "Uid");
        }
    }
}
