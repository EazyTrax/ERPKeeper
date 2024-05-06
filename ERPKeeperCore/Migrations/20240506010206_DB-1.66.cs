using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB166 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LiabilityPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    No = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountBankFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayFromAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiabilityAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiabilityPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiabilityPayments_Accounts_LiabilityAccountId",
                        column: x => x.LiabilityAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LiabilityPayments_Accounts_PayFromAccountId",
                        column: x => x.PayFromAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LiabilityPayments_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityPayments_LiabilityAccountId",
                table: "LiabilityPayments",
                column: "LiabilityAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityPayments_PayFromAccountId",
                table: "LiabilityPayments",
                column: "PayFromAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityPayments_TransactionId",
                table: "LiabilityPayments",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LiabilityPayments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Transactions");
        }
    }
}
