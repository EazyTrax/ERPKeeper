using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ERP_Accounting_Default_Account_Accounts_AccountId",
                table: "ERP_Accounting_Default_Account");

            migrationBuilder.DropForeignKey(
                name: "FK_ERP_Profile_Roles_Profiles_ProfileId",
                table: "ERP_Profile_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_ERP_Profiles_Addresses_Profiles_ProfileId",
                table: "ERP_Profiles_Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_ERP_Profiles_BankAccount_Profiles_ProfileId",
                table: "ERP_Profiles_BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_ERP_Profiles_ContactPerson_Profiles_ProfileId",
                table: "ERP_Profiles_ContactPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_FiscalYearAccountBalance_ERP_Fiscal_Years_FiscalYearId",
                table: "FiscalYearAccountBalance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ERP_Profiles_ContactPerson",
                table: "ERP_Profiles_ContactPerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ERP_Profiles_BankAccount",
                table: "ERP_Profiles_BankAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ERP_Profiles_Addresses",
                table: "ERP_Profiles_Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ERP_Profile_Roles",
                table: "ERP_Profile_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ERP_Fiscal_Years",
                table: "ERP_Fiscal_Years");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ERP_Datum",
                table: "ERP_Datum");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ERP_Accounting_Default_Account",
                table: "ERP_Accounting_Default_Account");

            migrationBuilder.RenameTable(
                name: "ERP_Profiles_ContactPerson",
                newName: "ProfileContacts");

            migrationBuilder.RenameTable(
                name: "ERP_Profiles_BankAccount",
                newName: "ProfileBankAccounts");

            migrationBuilder.RenameTable(
                name: "ERP_Profiles_Addresses",
                newName: "ProfileAddresses");

            migrationBuilder.RenameTable(
                name: "ERP_Profile_Roles",
                newName: "ProfileRoles");

            migrationBuilder.RenameTable(
                name: "ERP_Fiscal_Years",
                newName: "FiscalYears");

            migrationBuilder.RenameTable(
                name: "ERP_Datum",
                newName: "DataItems");

            migrationBuilder.RenameTable(
                name: "ERP_Accounting_Default_Account",
                newName: "DefaultAccounts");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "FundTransferItems",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ERP_Profiles_ContactPerson_ProfileId",
                table: "ProfileContacts",
                newName: "IX_ProfileContacts_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ERP_Profiles_BankAccount_ProfileId",
                table: "ProfileBankAccounts",
                newName: "IX_ProfileBankAccounts_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ERP_Profiles_Addresses_ProfileId",
                table: "ProfileAddresses",
                newName: "IX_ProfileAddresses_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ERP_Profile_Roles_ProfileId",
                table: "ProfileRoles",
                newName: "IX_ProfileRoles_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ERP_Accounting_Default_Account_AccountId",
                table: "DefaultAccounts",
                newName: "IX_DefaultAccounts_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileContacts",
                table: "ProfileContacts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileBankAccounts",
                table: "ProfileBankAccounts",
                column: "BankAccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileAddresses",
                table: "ProfileAddresses",
                column: "AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileRoles",
                table: "ProfileRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FiscalYears",
                table: "FiscalYears",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataItems",
                table: "DataItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DefaultAccounts",
                table: "DefaultAccounts",
                column: "Type");

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessLevel = table.Column<int>(type: "int", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DefaultAccounts_Accounts_AccountId",
                table: "DefaultAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalYearAccountBalance_FiscalYears_FiscalYearId",
                table: "FiscalYearAccountBalance",
                column: "FiscalYearId",
                principalTable: "FiscalYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileAddresses_Profiles_ProfileId",
                table: "ProfileAddresses",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileBankAccounts_Profiles_ProfileId",
                table: "ProfileBankAccounts",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileContacts_Profiles_ProfileId",
                table: "ProfileContacts",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileRoles_Profiles_ProfileId",
                table: "ProfileRoles",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DefaultAccounts_Accounts_AccountId",
                table: "DefaultAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_FiscalYearAccountBalance_FiscalYears_FiscalYearId",
                table: "FiscalYearAccountBalance");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileAddresses_Profiles_ProfileId",
                table: "ProfileAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileBankAccounts_Profiles_ProfileId",
                table: "ProfileBankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileContacts_Profiles_ProfileId",
                table: "ProfileContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileRoles_Profiles_ProfileId",
                table: "ProfileRoles");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileRoles",
                table: "ProfileRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileContacts",
                table: "ProfileContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileBankAccounts",
                table: "ProfileBankAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileAddresses",
                table: "ProfileAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FiscalYears",
                table: "FiscalYears");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DefaultAccounts",
                table: "DefaultAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataItems",
                table: "DataItems");

            migrationBuilder.RenameTable(
                name: "ProfileRoles",
                newName: "ERP_Profile_Roles");

            migrationBuilder.RenameTable(
                name: "ProfileContacts",
                newName: "ERP_Profiles_ContactPerson");

            migrationBuilder.RenameTable(
                name: "ProfileBankAccounts",
                newName: "ERP_Profiles_BankAccount");

            migrationBuilder.RenameTable(
                name: "ProfileAddresses",
                newName: "ERP_Profiles_Addresses");

            migrationBuilder.RenameTable(
                name: "FiscalYears",
                newName: "ERP_Fiscal_Years");

            migrationBuilder.RenameTable(
                name: "DefaultAccounts",
                newName: "ERP_Accounting_Default_Account");

            migrationBuilder.RenameTable(
                name: "DataItems",
                newName: "ERP_Datum");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "FundTransferItems",
                newName: "Uid");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileRoles_ProfileId",
                table: "ERP_Profile_Roles",
                newName: "IX_ERP_Profile_Roles_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileContacts_ProfileId",
                table: "ERP_Profiles_ContactPerson",
                newName: "IX_ERP_Profiles_ContactPerson_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileBankAccounts_ProfileId",
                table: "ERP_Profiles_BankAccount",
                newName: "IX_ERP_Profiles_BankAccount_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileAddresses_ProfileId",
                table: "ERP_Profiles_Addresses",
                newName: "IX_ERP_Profiles_Addresses_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_DefaultAccounts_AccountId",
                table: "ERP_Accounting_Default_Account",
                newName: "IX_ERP_Accounting_Default_Account_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ERP_Profile_Roles",
                table: "ERP_Profile_Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ERP_Profiles_ContactPerson",
                table: "ERP_Profiles_ContactPerson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ERP_Profiles_BankAccount",
                table: "ERP_Profiles_BankAccount",
                column: "BankAccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ERP_Profiles_Addresses",
                table: "ERP_Profiles_Addresses",
                column: "AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ERP_Fiscal_Years",
                table: "ERP_Fiscal_Years",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ERP_Accounting_Default_Account",
                table: "ERP_Accounting_Default_Account",
                column: "Type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ERP_Datum",
                table: "ERP_Datum",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ERP_Accounting_Default_Account_Accounts_AccountId",
                table: "ERP_Accounting_Default_Account",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ERP_Profile_Roles_Profiles_ProfileId",
                table: "ERP_Profile_Roles",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ERP_Profiles_Addresses_Profiles_ProfileId",
                table: "ERP_Profiles_Addresses",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ERP_Profiles_BankAccount_Profiles_ProfileId",
                table: "ERP_Profiles_BankAccount",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ERP_Profiles_ContactPerson_Profiles_ProfileId",
                table: "ERP_Profiles_ContactPerson",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalYearAccountBalance_ERP_Fiscal_Years_FiscalYearId",
                table: "FiscalYearAccountBalance",
                column: "FiscalYearId",
                principalTable: "ERP_Fiscal_Years",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
