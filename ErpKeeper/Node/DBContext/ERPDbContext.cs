
using ERPKeeper.Node.Models.Logging;
using System;
using System.Data.Entity;


namespace ERPKeeper.Node.DBContext
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeRush", "Can implement base type constructors")]
    public class ERPDbContext : DbContext
    {
        public String DBName { get; private set; }
        private String connectionString { get; set; }
        private string DatabaseUser { get; set; }
        private string DatabasePassword { get; set; }

        public ERPDbContext(string databaseName, bool MigratedDB = false)
        {

            this.DBName = databaseName.ToLower().Trim().Replace(".advancemaker.com", "");
            this.DatabaseUser = "sa";
            this.DatabasePassword = "P@ssw0rd@1";

            connectionString = $"Server=172.19.2.5;Database=erp-{this.DBName};User Id={this.DatabaseUser};Password={this.DatabasePassword};";
            this.Database.Connection.ConnectionString = connectionString;


            if (MigratedDB)
            {
                Console.WriteLine("> Migrate DB Starting");
                Database.SetInitializer<ERPDbContext>(new CreateDatabaseIfNotExists<ERPDbContext>());
                Database.SetInitializer<ERPDbContext>(new MigrateDatabaseToLatestVersion<ERPDbContext, ConfigurationERPDbContext>(true));
                Console.WriteLine("> Migrate DB Finished");
            }
            else
            {
                Database.SetInitializer<ERPDbContext>(null);
            }
        }

        public bool CheckDatabaseExist()
        {
            if (this.Database.Exists())
                return true;

            throw new DatabaseNotFoundException();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public DbSet<Models.Datum.DataItem> DataItems { get; set; }
        public DbSet<Models.Location> Locations { get; set; }
        public DbSet<Models.Online.ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Models.Equity.CapitalActivity> CapitalActivities { get; set; }
        public DbSet<Models.Security.Member> Members { get; set; }

        #region Items
        public DbSet<Models.Items.Item> Items { get; set; }
        public DbSet<Models.Items.GroupItem> GroupItems { get; set; }


        public DbSet<Models.Items.AssistItem> AssistItems { get; set; }
        public DbSet<Models.Items.ItemGroup> ItemGroups { get; set; }
        public DbSet<Models.Items.Brand> Brands { get; set; }
        public DbSet<Models.Items.ItemImage> ItemImages { get; set; }
        public DbSet<Models.Items.DataSheet> DataSheets { get; set; }
        public DbSet<Models.Items.ItemParameterType> ItemParameterTypes { get; set; }
        public DbSet<Models.Items.ItemParameter> ItemParameters { get; set; }
        public DbSet<Models.Items.PriceRange> PriceRanges { get; set; }
        public DbSet<Models.Items.PriceRangeItem> PriceRangeItems { get; set; }
        #endregion


        #region Accounting
        public DbSet<Models.Accounting.FiscalYear> FiscalYears { get; set; }
        public DbSet<Models.Accounting.FiscalYears.PeriodAccountBalance> PeriodAccountsBalances { get; set; }
        public DbSet<Models.Accounting.FiscalYears.PeriodItemCOGS> PeriodItemsCOGS { get; set; }
        public DbSet<Models.Accounting.AccountItem> AccountItems { get; set; }
        public DbSet<Models.Accounting.DefaultAccountItem> SystemAccounts { get; set; }
        public DbSet<Models.Accounting.PreviewAccount> PreviewAccounts { get; set; }
        public DbSet<Models.Accounting.HistoryBalanceItem> HistoryBalanceItems { get; set; }
        public DbSet<Models.Accounting.TransactionLedger> TransactionLedgers { get; set; }
        public DbSet<Models.Accounting.Ledger> Ledgers { get; set; }
        public DbSet<Models.Accounting.JournalEntryType> JournalEntryTypes { get; set; }
        public DbSet<Models.Accounting.JournalEntry> JournalEntries { get; set; }
        public DbSet<Models.Accounting.JournalEntryItem> JournalEntryItems { get; set; }
        #endregion



        #region Estimations
        public DbSet<Models.Estimations.Quote> Estimates { get; set; }
        public DbSet<Models.Estimations.SaleQuote> SaleEstimates { get; set; }
        public DbSet<Models.Estimations.PurchaseQuote> PurchaseEstimates { get; set; }
        public DbSet<Models.Estimations.QuoteItem> EstimateItems { get; set; }

        #endregion



        #region Role
        public DbSet<Models.Equity.Investor> Investors { get; set; }
        public DbSet<Models.Customers.Customer> Customers { get; set; }
        public DbSet<Models.Suppliers.Supplier> Suppliers { get; set; }
        #endregion




        #region Profiles
        public DbSet<Models.Profiles.Profile> Profiles { get; set; }
        public DbSet<Models.Profiles.ProfileRole> ProfileRoles { get; set; }
        public DbSet<Models.Profiles.ProfileBankAccount> ProfileBankAccounts { get; set; }
        public DbSet<Models.Profiles.ProfileAddress> ProfileAddresses { get; set; }
        public DbSet<Models.Profiles.ProfileContact> ProfileContacts { get; set; }
        public DbSet<Models.Profiles.HistoryItem> HistoryItems { get; set; }


        #endregion





        #region Commercial
        public DbSet<Models.Transactions.Commercial> Commercials { get; set; }
        public DbSet<Models.Transactions.Commercials.Sale> Sales { get; set; }
        public DbSet<Models.Transactions.Commercials.Purchase> Purchases { get; set; }
        public DbSet<Models.Transactions.Commercials.PaymentTerm> PaymentTerms { get; set; }
        public DbSet<Models.Transactions.Commercials.ShippingTerm> ShippingTerms { get; set; }
        public DbSet<Models.Transactions.Commercials.ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Models.Transactions.CommercialItem> CommercialItems { get; set; }

        public DbSet<Models.Transactions.CommercialShipment> CommercialShipments { get; set; }
        public DbSet<Models.Transactions.CommercialDailyBalance> CommercialDailyBalances { get; set; }

        #endregion



        public DbSet<Models.Projects.Project> Projects { get; set; }
        public DbSet<Models.Tasks.Task> Tasks { get; set; }
        public DbSet<Models.Projects.Material> Materials { get; set; }


        public DbSet<Models.Assets.FixedAsset> FixedAssets { get; set; }
        public DbSet<Models.Assets.FixedAssetType> FixedAssetTypes { get; set; }
        public DbSet<Models.Assets.DeprecateSchedule> DeprecateSchedules { get; set; }





        #region Financial
        public DbSet<Models.Financial.Transfer.FundTransfer> FundTransfers { get; set; }
        public DbSet<Models.Financial.Payments.RetentionType> RetentionTypes { get; set; }
        public DbSet<Models.Financial.Loans.Loan> Loans { get; set; }
        public DbSet<Models.Financial.Loans.LoanPayment> LoanPayments { get; set; }
        public DbSet<Models.Financial.Lends.Lend> Lends { get; set; }
        public DbSet<Models.Financial.Lends.LendPayment> LendPayments { get; set; }
        public DbSet<Models.Financial.Payments.GeneralPayment> GeneralPayments { get; set; }
        public DbSet<Models.Financial.Payments.LiabilityPayment> LiabilityPayments { get; set; }
        public DbSet<Models.Financial.Payments.ReceivePayment> ReceivePayments { get; set; }
        public DbSet<Models.Financial.Payments.SupplierPayment> SupplierPayments { get; set; }

        public DbSet<Models.Financial.Payments.RetentionGroup> RetentionGroups { get; set; }


        #endregion






        #region Taxes
        //public DbSet<Models.Taxes.TaxGroup> TaxGroups { get; set; }
        public DbSet<Models.Taxes.TaxCode> TaxCodes { get; set; }
        public DbSet<Models.Taxes.TaxRate> TaxRate { get; set; }
        public DbSet<Models.Taxes.IncomeTax> IncomeTaxes { get; set; }
        public DbSet<Models.Taxes.TaxPeriod> TaxPeriods { get; set; }
        #endregion




        #region Employees
        public DbSet<Models.Employees.Employee> Employees { get; set; }
        public DbSet<Models.Employees.EmployeePosition> EmployeePositions { get; set; }
        public DbSet<Models.Employees.EmployeePaymentPeriod> EmployeePaymentPeriods { get; set; }
        public DbSet<Models.Employees.EmployeePaymentType> EmployeePaymentTypes { get; set; }
        public DbSet<Models.Employees.EmployeePaymentItem> EmployeePaymentLines { get; set; }
        public DbSet<Models.Employees.EmployeePayment> EmployeePayments { get; set; }
        public DbSet<Models.Employees.EmployeeLeave> EmployeeLeaves { get; set; }
        public DbSet<Models.Employees.EmployeePaymentTemplate> EmployeePaymentTemplates { get; set; }
        public DbSet<Models.Employees.EmployeePaymentTemplateItem> EmployeePaymentTemplateItems { get; set; }
        #endregion


        public DbSet<Models.Documents.Document> Documents { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }
    }


}