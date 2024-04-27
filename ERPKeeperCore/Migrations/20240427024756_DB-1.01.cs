using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    No = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsLiquidity = table.Column<bool>(type: "bit", nullable: false),
                    IsCashEquivalent = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    IsFolder = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SubType = table.Column<int>(type: "int", nullable: true),
                    BalanceCalulatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurentCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShotName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishOnline = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ERP_Datum",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERP_Datum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ERP_Fiscal_Years",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERP_Fiscal_Years", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTerms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDayCount = table.Column<int>(type: "int", nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxDayCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsSelfOrganization = table.Column<bool>(type: "bit", nullable: false),
                    IsRDVerify = table.Column<bool>(type: "bit", nullable: false),
                    TitleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShotName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    localizedLanguage = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailLogin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaceBook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Projects_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodePrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeprecatedAble = table.Column<bool>(type: "bit", nullable: false),
                    ScrapValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UseFulLifeYear = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepreciationMethod = table.Column<int>(type: "int", nullable: false),
                    AwaitDeprecateAccId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchaseAccId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AmortizeExpenseAccId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccumulateDeprecateAccId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetTypes_Accounts_AccumulateDeprecateAccId",
                        column: x => x.AccumulateDeprecateAccId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetTypes_Accounts_AmortizeExpenseAccId",
                        column: x => x.AmortizeExpenseAccId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetTypes_Accounts_AwaitDeprecateAccId",
                        column: x => x.AwaitDeprecateAccId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssetTypes_Accounts_PurchaseAccId",
                        column: x => x.PurchaseAccId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CapitalActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    No = table.Column<int>(type: "int", nullable: false),
                    StockAmount = table.Column<int>(type: "int", nullable: false),
                    EachStockParValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EquityAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssetAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PostStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapitalActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CapitalActivities_Accounts_AssetAccountId",
                        column: x => x.AssetAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CapitalActivities_Accounts_EquityAccountId",
                        column: x => x.EquityAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ERP_Accounting_Default_Account",
                columns: table => new
                {
                    Type = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERP_Accounting_Default_Account", x => x.Type);
                    table.ForeignKey(
                        name: "FK_ERP_Accounting_Default_Account_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaxCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxDirection = table.Column<int>(type: "int", nullable: false),
                    isDefault = table.Column<bool>(type: "bit", nullable: false),
                    isRecoverable = table.Column<bool>(type: "bit", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OutputTaxAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InputTaxAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssignAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CloseToAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxReceivableAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxPayableAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsReady = table.Column<bool>(type: "bit", nullable: false),
                    CommercialCount = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxCodes_Accounts_AssignAccountId",
                        column: x => x.AssignAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaxCodes_Accounts_CloseToAccountId",
                        column: x => x.CloseToAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaxCodes_Accounts_InputTaxAccountId",
                        column: x => x.InputTaxAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaxCodes_Accounts_OutputTaxAccountId",
                        column: x => x.OutputTaxAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaxCodes_Accounts_TaxAccountId",
                        column: x => x.TaxAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaxCodes_Accounts_TaxPayableAccountId",
                        column: x => x.TaxPayableAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaxPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    No = table.Column<int>(type: "int", nullable: true),
                    FiscalYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TrnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseToAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiabilityPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostStatus = table.Column<int>(type: "int", nullable: false),
                    InputCommercialCount = table.Column<int>(type: "int", nullable: false),
                    InputBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InputTaxBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OutputBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OutputTaxBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OutputCommercialCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxPeriods_Accounts_CloseToAccountId",
                        column: x => x.CloseToAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    UPC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnlineSale = table.Column<bool>(type: "bit", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchaseAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IncomeAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StockAmount = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Accounts_IncomeAccountId",
                        column: x => x.IncomeAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Accounts_PurchaseAccountId",
                        column: x => x.PurchaseAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Items_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FiscalYearAccountBalance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FiscalYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalYearAccountBalance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiscalYearAccountBalance_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiscalYearAccountBalance_ERP_Fiscal_Years_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "ERP_Fiscal_Years",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ERP_Profile_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERP_Profile_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ERP_Profile_Roles_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ERP_Profiles_Addresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FaxNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERP_Profiles_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_ERP_Profiles_Addresses_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ERP_Profiles_BankAccount",
                columns: table => new
                {
                    BankAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERP_Profiles_BankAccount", x => x.BankAccountId);
                    table.ForeignKey(
                        name: "FK_ERP_Profiles_BankAccount_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ERP_Profiles_ContactPerson",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingNote = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERP_Profiles_ContactPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ERP_Profiles_ContactPerson_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    No = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDeprecationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SavageValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepreciationPerYear = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastCreateSchedule = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_AssetTypes_AssetTypeId",
                        column: x => x.AssetTypeId,
                        principalTable: "AssetTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    No = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FiscalYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LinesTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_TaxCodes_TaxCodeId",
                        column: x => x.TaxCodeId,
                        principalTable: "TaxCodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchases_TaxPeriods_TaxPeriodId",
                        column: x => x.TaxPeriodId,
                        principalTable: "TaxPeriods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    No = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FiscalYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LinesTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sales_TaxCodes_TaxCodeId",
                        column: x => x.TaxCodeId,
                        principalTable: "TaxCodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sales_TaxPeriods_TaxPeriodId",
                        column: x => x.TaxPeriodId,
                        principalTable: "TaxPeriods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeprecateSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    No = table.Column<int>(type: "int", nullable: false),
                    AssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepreciationValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepreciateOffsetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepreciateAccumulation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PostStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeprecateSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeprecateSchedules_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseItem_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SaleItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleItems_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetTypeId",
                table: "Assets",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTypes_AccumulateDeprecateAccId",
                table: "AssetTypes",
                column: "AccumulateDeprecateAccId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTypes_AmortizeExpenseAccId",
                table: "AssetTypes",
                column: "AmortizeExpenseAccId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTypes_AwaitDeprecateAccId",
                table: "AssetTypes",
                column: "AwaitDeprecateAccId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTypes_PurchaseAccId",
                table: "AssetTypes",
                column: "PurchaseAccId");

            migrationBuilder.CreateIndex(
                name: "IX_CapitalActivities_AssetAccountId",
                table: "CapitalActivities",
                column: "AssetAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CapitalActivities_EquityAccountId",
                table: "CapitalActivities",
                column: "EquityAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DeprecateSchedules_AssetId",
                table: "DeprecateSchedules",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ERP_Accounting_Default_Account_AccountId",
                table: "ERP_Accounting_Default_Account",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ERP_Profile_Roles_ProfileId",
                table: "ERP_Profile_Roles",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ERP_Profiles_Addresses_ProfileId",
                table: "ERP_Profiles_Addresses",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ERP_Profiles_BankAccount_ProfileId",
                table: "ERP_Profiles_BankAccount",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ERP_Profiles_ContactPerson_ProfileId",
                table: "ERP_Profiles_ContactPerson",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYearAccountBalance_AccountId",
                table: "FiscalYearAccountBalance",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYearAccountBalance_FiscalYearId",
                table: "FiscalYearAccountBalance",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_BrandId",
                table: "Items",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_IncomeAccountId",
                table: "Items",
                column: "IncomeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemGroupId",
                table: "Items",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_PurchaseAccountId",
                table: "Items",
                column: "PurchaseAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ParentId",
                table: "Projects",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItem_ItemId",
                table: "PurchaseItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItem_PurchaseId",
                table: "PurchaseItem",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_TaxCodeId",
                table: "Purchases",
                column: "TaxCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_TaxPeriodId",
                table: "Purchases",
                column: "TaxPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_ItemId",
                table: "SaleItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_SaleId",
                table: "SaleItems",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProjectId",
                table: "Sales",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_TaxCodeId",
                table: "Sales",
                column: "TaxCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_TaxPeriodId",
                table: "Sales",
                column: "TaxPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCodes_AssignAccountId",
                table: "TaxCodes",
                column: "AssignAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCodes_CloseToAccountId",
                table: "TaxCodes",
                column: "CloseToAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCodes_InputTaxAccountId",
                table: "TaxCodes",
                column: "InputTaxAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCodes_OutputTaxAccountId",
                table: "TaxCodes",
                column: "OutputTaxAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCodes_TaxAccountId",
                table: "TaxCodes",
                column: "TaxAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCodes_TaxPayableAccountId",
                table: "TaxCodes",
                column: "TaxPayableAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxPeriods_CloseToAccountId",
                table: "TaxPeriods",
                column: "CloseToAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CapitalActivities");

            migrationBuilder.DropTable(
                name: "DeprecateSchedules");

            migrationBuilder.DropTable(
                name: "ERP_Accounting_Default_Account");

            migrationBuilder.DropTable(
                name: "ERP_Datum");

            migrationBuilder.DropTable(
                name: "ERP_Profile_Roles");

            migrationBuilder.DropTable(
                name: "ERP_Profiles_Addresses");

            migrationBuilder.DropTable(
                name: "ERP_Profiles_BankAccount");

            migrationBuilder.DropTable(
                name: "ERP_Profiles_ContactPerson");

            migrationBuilder.DropTable(
                name: "FiscalYearAccountBalance");

            migrationBuilder.DropTable(
                name: "PaymentTerms");

            migrationBuilder.DropTable(
                name: "PurchaseItem");

            migrationBuilder.DropTable(
                name: "SaleItems");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "ERP_Fiscal_Years");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "AssetTypes");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "TaxCodes");

            migrationBuilder.DropTable(
                name: "TaxPeriods");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
