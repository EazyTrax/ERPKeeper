using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FundTransfers",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    No = table.Column<int>(type: "int", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountwithDraw = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WithDrawAccountGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepositAccountGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankFeeAccountGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundTransfers", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_FundTransfers_Accounts_BankFeeAccountGuid",
                        column: x => x.BankFeeAccountGuid,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FundTransfers_Accounts_DepositAccountGuid",
                        column: x => x.DepositAccountGuid,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FundTransfers_Accounts_WithDrawAccountGuid",
                        column: x => x.WithDrawAccountGuid,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FundTransferItems",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FundTransferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DebitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundTransferItems", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_FundTransferItems_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundTransferItems_FundTransfers_FundTransferId",
                        column: x => x.FundTransferId,
                        principalTable: "FundTransfers",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FundTransferItems_AccountId",
                table: "FundTransferItems",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransferItems_FundTransferId",
                table: "FundTransferItems",
                column: "FundTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfers_BankFeeAccountGuid",
                table: "FundTransfers",
                column: "BankFeeAccountGuid");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfers_DepositAccountGuid",
                table: "FundTransfers",
                column: "DepositAccountGuid");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfers_WithDrawAccountGuid",
                table: "FundTransfers",
                column: "WithDrawAccountGuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FundTransferItems");

            migrationBuilder.DropTable(
                name: "FundTransfers");
        }
    }
}
