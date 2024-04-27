using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB105 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Items_ItemGroupId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Brands");

            migrationBuilder.AlterColumn<string>(
                name: "WebSite",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ShotName",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ItemGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGroups", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemGroups_ItemGroupId",
                table: "Items",
                column: "ItemGroupId",
                principalTable: "ItemGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemGroups_ItemGroupId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ItemGroups");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Items",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "WebSite",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShotName",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Brands",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Items_ItemGroupId",
                table: "Items",
                column: "ItemGroupId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
