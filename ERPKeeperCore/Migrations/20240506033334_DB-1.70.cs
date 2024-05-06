using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB170 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lends",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    No = table.Column<int>(type: "int", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayingAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LendingAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AmountLend = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountReturn = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lends_Accounts_LendingAccountId",
                        column: x => x.LendingAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lends_Accounts_PayingAccountId",
                        column: x => x.PayingAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lends_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    No = table.Column<int>(type: "int", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecevingAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LoanAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AmountLoan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountReturn = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Accounts_LoanAccountId",
                        column: x => x.LoanAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Loans_Accounts_RecevingAccountId",
                        column: x => x.RecevingAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Loans_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LendReturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LendId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReturnToAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LendReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LendReturns_Accounts_ReturnToAccountId",
                        column: x => x.ReturnToAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LendReturns_Lends_LendId",
                        column: x => x.LendId,
                        principalTable: "Lends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LendReturns_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanReturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReturnFromAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanReturns_Accounts_ReturnFromAccountId",
                        column: x => x.ReturnFromAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanReturns_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanReturns_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LendReturns_LendId",
                table: "LendReturns",
                column: "LendId");

            migrationBuilder.CreateIndex(
                name: "IX_LendReturns_ReturnToAccountId",
                table: "LendReturns",
                column: "ReturnToAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LendReturns_TransactionId",
                table: "LendReturns",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Lends_LendingAccountId",
                table: "Lends",
                column: "LendingAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Lends_PayingAccountId",
                table: "Lends",
                column: "PayingAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Lends_TransactionId",
                table: "Lends",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LoanReturns_LoanId",
                table: "LoanReturns",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanReturns_ReturnFromAccountId",
                table: "LoanReturns",
                column: "ReturnFromAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanReturns_TransactionId",
                table: "LoanReturns",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LoanAccountId",
                table: "Loans",
                column: "LoanAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_RecevingAccountId",
                table: "Loans",
                column: "RecevingAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_TransactionId",
                table: "Loans",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LendReturns");

            migrationBuilder.DropTable(
                name: "LoanReturns");

            migrationBuilder.DropTable(
                name: "Lends");

            migrationBuilder.DropTable(
                name: "Loans");
        }
    }
}
