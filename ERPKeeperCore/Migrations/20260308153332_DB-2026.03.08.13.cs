using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB2026030813 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DevelopmentCourses_Employees_EmployeeId",
                table: "DevelopmentCourses");

            migrationBuilder.DropIndex(
                name: "IX_DevelopmentCourses_EmployeeId",
                table: "DevelopmentCourses");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "DevelopmentCourses");

            migrationBuilder.DropColumn(
                name: "FinishDate",
                table: "DevelopmentCourses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DevelopmentCourses");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "DevelopmentCourses");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DevelopmentCourses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Requried",
                table: "DevelopmentCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "DevelopmentCourses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CertificateAndLicenseId",
                table: "CertificatesAndLicenses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CertificateAndLicenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuingOrganization = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateAndLicenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDevelopmentCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DevelopmentCourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDevelopmentCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDevelopmentCourses_DevelopmentCourses_DevelopmentCourseId",
                        column: x => x.DevelopmentCourseId,
                        principalTable: "DevelopmentCourses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeDevelopmentCourses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CertificatesAndLicenses_CertificateAndLicenseId",
                table: "CertificatesAndLicenses",
                column: "CertificateAndLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDevelopmentCourses_DevelopmentCourseId",
                table: "EmployeeDevelopmentCourses",
                column: "DevelopmentCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDevelopmentCourses_EmployeeId",
                table: "EmployeeDevelopmentCourses",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificatesAndLicenses_CertificateAndLicenses_CertificateAndLicenseId",
                table: "CertificatesAndLicenses",
                column: "CertificateAndLicenseId",
                principalTable: "CertificateAndLicenses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificatesAndLicenses_CertificateAndLicenses_CertificateAndLicenseId",
                table: "CertificatesAndLicenses");

            migrationBuilder.DropTable(
                name: "CertificateAndLicenses");

            migrationBuilder.DropTable(
                name: "EmployeeDevelopmentCourses");

            migrationBuilder.DropIndex(
                name: "IX_CertificatesAndLicenses_CertificateAndLicenseId",
                table: "CertificatesAndLicenses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DevelopmentCourses");

            migrationBuilder.DropColumn(
                name: "Requried",
                table: "DevelopmentCourses");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "DevelopmentCourses");

            migrationBuilder.DropColumn(
                name: "CertificateAndLicenseId",
                table: "CertificatesAndLicenses");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "DevelopmentCourses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishDate",
                table: "DevelopmentCourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DevelopmentCourses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "DevelopmentCourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentCourses_EmployeeId",
                table: "DevelopmentCourses",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DevelopmentCourses_Employees_EmployeeId",
                table: "DevelopmentCourses",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
