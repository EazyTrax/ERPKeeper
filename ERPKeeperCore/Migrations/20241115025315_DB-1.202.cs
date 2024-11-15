using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1202 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Documents",
                newName: "Note");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ProfileId",
                table: "Documents",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Profiles_ProfileId",
                table: "Documents",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Profiles_ProfileId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_ProfileId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Documents",
                newName: "Name");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Documents",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
