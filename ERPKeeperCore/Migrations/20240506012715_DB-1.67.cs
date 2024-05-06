using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB167 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiabilityPayments_Accounts_PayFromAccountId",
                table: "LiabilityPayments");

            migrationBuilder.DropIndex(
                name: "IX_LiabilityPayments_PayFromAccountId",
                table: "LiabilityPayments");

            migrationBuilder.DropColumn(
                name: "PayFromAccountId",
                table: "LiabilityPayments");

            migrationBuilder.AddColumn<decimal>(
                name: "AmountDiscount",
                table: "LiabilityPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "LiabilityPaymentPayFromAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LiabilityPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiabilityPaymentPayFromAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiabilityPaymentPayFromAccount_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiabilityPaymentPayFromAccount_LiabilityPayments_LiabilityPaymentId",
                        column: x => x.LiabilityPaymentId,
                        principalTable: "LiabilityPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityPaymentPayFromAccount_AccountId",
                table: "LiabilityPaymentPayFromAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityPaymentPayFromAccount_LiabilityPaymentId",
                table: "LiabilityPaymentPayFromAccount",
                column: "LiabilityPaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LiabilityPaymentPayFromAccount");

            migrationBuilder.DropColumn(
                name: "AmountDiscount",
                table: "LiabilityPayments");

            migrationBuilder.AddColumn<Guid>(
                name: "PayFromAccountId",
                table: "LiabilityPayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityPayments_PayFromAccountId",
                table: "LiabilityPayments",
                column: "PayFromAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_LiabilityPayments_Accounts_PayFromAccountId",
                table: "LiabilityPayments",
                column: "PayFromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
