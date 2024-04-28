using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB114 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FiscalYearAccountBalance_Accounts_AccountId",
                table: "FiscalYearAccountBalance");

            migrationBuilder.DropForeignKey(
                name: "FK_FiscalYearAccountBalance_FiscalYears_FiscalYearId",
                table: "FiscalYearAccountBalance");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntryItems_Accounts_AccountUid",
                table: "JournalEntryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLedger_Accounts_TransactionId",
                table: "TransactionLedger");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLedger_Transactions_TransactionId",
                table: "TransactionLedger");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionLedger",
                table: "TransactionLedger");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FiscalYearAccountBalance",
                table: "FiscalYearAccountBalance");

            migrationBuilder.RenameTable(
                name: "TransactionLedger",
                newName: "TransactionLedgers");

            migrationBuilder.RenameTable(
                name: "FiscalYearAccountBalance",
                newName: "FiscalYearAccountBalances");

            migrationBuilder.RenameColumn(
                name: "AccountUid",
                table: "JournalEntryItems",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_JournalEntryItems_AccountUid",
                table: "JournalEntryItems",
                newName: "IX_JournalEntryItems_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionLedger_TransactionId",
                table: "TransactionLedgers",
                newName: "IX_TransactionLedgers_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_FiscalYearAccountBalance_FiscalYearId",
                table: "FiscalYearAccountBalances",
                newName: "IX_FiscalYearAccountBalances_FiscalYearId");

            migrationBuilder.RenameIndex(
                name: "IX_FiscalYearAccountBalance_AccountId",
                table: "FiscalYearAccountBalances",
                newName: "IX_FiscalYearAccountBalances_AccountId");

            migrationBuilder.AddColumn<bool>(
                name: "IsPosted",
                table: "Sales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPosted",
                table: "Purchases",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPosted",
                table: "JournalEntries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AmountOnHand",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AmountOnPurchaseOrder",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AmountOnQuotation",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AmountOnSaleOrder",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AmountPurchase",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AmountSold",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupItemId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ItemsCount",
                table: "ItemGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "ItemGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Memo",
                table: "FiscalYears",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExpenseBalance",
                table: "FiscalYears",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IncomeBalance",
                table: "FiscalYears",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsPosted",
                table: "FiscalYears",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ProfitBalance",
                table: "FiscalYears",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsPosted",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningCredit",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningDebit",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TransactionLedgers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "TransactionLedgers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ClosedCredit",
                table: "FiscalYearAccountBalances",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ClosedDebit",
                table: "FiscalYearAccountBalances",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionLedgers",
                table: "TransactionLedgers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FiscalYearAccountBalances",
                table: "FiscalYearAccountBalances",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EmployeePaymentPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalEarning = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayFromAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePaymentPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePaymentPeriods_Accounts_PayFromAccountId",
                        column: x => x.PayFromAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeePaymentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PayDirection = table.Column<int>(type: "int", nullable: false),
                    AccountUid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePaymentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePaymentTypes_Accounts_AccountUid",
                        column: x => x.AccountUid,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeePositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requried = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeTaxes",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    No = table.Column<int>(type: "int", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    TrDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FiscalYearUid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiabilityAccountGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IncomeTaxExpenseAccountGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PayFromAccountGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeTaxes", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_IncomeTaxes_Accounts_IncomeTaxExpenseAccountGuid",
                        column: x => x.IncomeTaxExpenseAccountGuid,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncomeTaxes_Accounts_LiabilityAccountGuid",
                        column: x => x.LiabilityAccountGuid,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncomeTaxes_FiscalYears_FiscalYearUid",
                        column: x => x.FiscalYearUid,
                        principalTable: "FiscalYears",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Investors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investors_Profiles_Id",
                        column: x => x.Id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EmployeePositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeePositions_EmployeePositionId",
                        column: x => x.EmployeePositionId,
                        principalTable: "EmployeePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Profiles_Id",
                        column: x => x.Id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    No = table.Column<int>(type: "int", nullable: false),
                    EmployeeUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeePaymentPeriodUid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TotalEarning = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayFromAccountUid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePayments_Accounts_PayFromAccountUid",
                        column: x => x.PayFromAccountUid,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeePayments_EmployeePaymentPeriods_EmployeePaymentPeriodUid",
                        column: x => x.EmployeePaymentPeriodUid,
                        principalTable: "EmployeePaymentPeriods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeePayments_Employees_EmployeeUid",
                        column: x => x.EmployeeUid,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePaymentItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeePaymentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeePaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePaymentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePaymentItems_EmployeePaymentTypes_EmployeePaymentTypeId",
                        column: x => x.EmployeePaymentTypeId,
                        principalTable: "EmployeePaymentTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeePaymentItems_EmployeePayments_EmployeePaymentId",
                        column: x => x.EmployeePaymentId,
                        principalTable: "EmployeePayments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroups_ParentId",
                table: "ItemGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePaymentItems_EmployeePaymentId",
                table: "EmployeePaymentItems",
                column: "EmployeePaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePaymentItems_EmployeePaymentTypeId",
                table: "EmployeePaymentItems",
                column: "EmployeePaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePaymentPeriods_PayFromAccountId",
                table: "EmployeePaymentPeriods",
                column: "PayFromAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayments_EmployeePaymentPeriodUid",
                table: "EmployeePayments",
                column: "EmployeePaymentPeriodUid");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayments_EmployeeUid",
                table: "EmployeePayments",
                column: "EmployeeUid");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayments_PayFromAccountUid",
                table: "EmployeePayments",
                column: "PayFromAccountUid");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePaymentTypes_AccountUid",
                table: "EmployeePaymentTypes",
                column: "AccountUid");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeePositionId",
                table: "Employees",
                column: "EmployeePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTaxes_FiscalYearUid",
                table: "IncomeTaxes",
                column: "FiscalYearUid");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTaxes_IncomeTaxExpenseAccountGuid",
                table: "IncomeTaxes",
                column: "IncomeTaxExpenseAccountGuid");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTaxes_LiabilityAccountGuid",
                table: "IncomeTaxes",
                column: "LiabilityAccountGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalYearAccountBalances_Accounts_AccountId",
                table: "FiscalYearAccountBalances",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalYearAccountBalances_FiscalYears_FiscalYearId",
                table: "FiscalYearAccountBalances",
                column: "FiscalYearId",
                principalTable: "FiscalYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGroups_ItemGroups_ParentId",
                table: "ItemGroups",
                column: "ParentId",
                principalTable: "ItemGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntryItems_Accounts_AccountId",
                table: "JournalEntryItems",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLedgers_Accounts_TransactionId",
                table: "TransactionLedgers",
                column: "TransactionId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLedgers_Transactions_TransactionId",
                table: "TransactionLedgers",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FiscalYearAccountBalances_Accounts_AccountId",
                table: "FiscalYearAccountBalances");

            migrationBuilder.DropForeignKey(
                name: "FK_FiscalYearAccountBalances_FiscalYears_FiscalYearId",
                table: "FiscalYearAccountBalances");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemGroups_ItemGroups_ParentId",
                table: "ItemGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntryItems_Accounts_AccountId",
                table: "JournalEntryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLedgers_Accounts_TransactionId",
                table: "TransactionLedgers");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLedgers_Transactions_TransactionId",
                table: "TransactionLedgers");

            migrationBuilder.DropTable(
                name: "EmployeePaymentItems");

            migrationBuilder.DropTable(
                name: "IncomeTaxes");

            migrationBuilder.DropTable(
                name: "Investors");

            migrationBuilder.DropTable(
                name: "EmployeePaymentTypes");

            migrationBuilder.DropTable(
                name: "EmployeePayments");

            migrationBuilder.DropTable(
                name: "EmployeePaymentPeriods");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeePositions");

            migrationBuilder.DropIndex(
                name: "IX_ItemGroups_ParentId",
                table: "ItemGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionLedgers",
                table: "TransactionLedgers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FiscalYearAccountBalances",
                table: "FiscalYearAccountBalances");

            migrationBuilder.DropColumn(
                name: "IsPosted",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsPosted",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "IsPosted",
                table: "JournalEntries");

            migrationBuilder.DropColumn(
                name: "AmountOnHand",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AmountOnPurchaseOrder",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AmountOnQuotation",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AmountOnSaleOrder",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AmountPurchase",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AmountSold",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GroupItemId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemsCount",
                table: "ItemGroups");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ItemGroups");

            migrationBuilder.DropColumn(
                name: "ExpenseBalance",
                table: "FiscalYears");

            migrationBuilder.DropColumn(
                name: "IncomeBalance",
                table: "FiscalYears");

            migrationBuilder.DropColumn(
                name: "IsPosted",
                table: "FiscalYears");

            migrationBuilder.DropColumn(
                name: "ProfitBalance",
                table: "FiscalYears");

            migrationBuilder.DropColumn(
                name: "IsPosted",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "OpeningCredit",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "OpeningDebit",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TransactionLedgers");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "TransactionLedgers");

            migrationBuilder.DropColumn(
                name: "ClosedCredit",
                table: "FiscalYearAccountBalances");

            migrationBuilder.DropColumn(
                name: "ClosedDebit",
                table: "FiscalYearAccountBalances");

            migrationBuilder.RenameTable(
                name: "TransactionLedgers",
                newName: "TransactionLedger");

            migrationBuilder.RenameTable(
                name: "FiscalYearAccountBalances",
                newName: "FiscalYearAccountBalance");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "JournalEntryItems",
                newName: "AccountUid");

            migrationBuilder.RenameIndex(
                name: "IX_JournalEntryItems_AccountId",
                table: "JournalEntryItems",
                newName: "IX_JournalEntryItems_AccountUid");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionLedgers_TransactionId",
                table: "TransactionLedger",
                newName: "IX_TransactionLedger_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_FiscalYearAccountBalances_FiscalYearId",
                table: "FiscalYearAccountBalance",
                newName: "IX_FiscalYearAccountBalance_FiscalYearId");

            migrationBuilder.RenameIndex(
                name: "IX_FiscalYearAccountBalances_AccountId",
                table: "FiscalYearAccountBalance",
                newName: "IX_FiscalYearAccountBalance_AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "Memo",
                table: "FiscalYears",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionLedger",
                table: "TransactionLedger",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FiscalYearAccountBalance",
                table: "FiscalYearAccountBalance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalYearAccountBalance_Accounts_AccountId",
                table: "FiscalYearAccountBalance",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalYearAccountBalance_FiscalYears_FiscalYearId",
                table: "FiscalYearAccountBalance",
                column: "FiscalYearId",
                principalTable: "FiscalYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntryItems_Accounts_AccountUid",
                table: "JournalEntryItems",
                column: "AccountUid",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLedger_Accounts_TransactionId",
                table: "TransactionLedger",
                column: "TransactionId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLedger_Transactions_TransactionId",
                table: "TransactionLedger",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
