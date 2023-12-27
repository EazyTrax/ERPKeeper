using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Data.SqlClient;
using KeeperCore.ERPNode.Models.Taxes;
using System.ComponentModel.DataAnnotations.Schema;
using KeeperCore.ERPNode.Models.Accounting;
using KeeperCore.ERPNode.Models.Transactions;

namespace KeeperCore.ERPNode.DBContext
{
    public class ERPCoreDbContext : DbContext
    {
        private readonly bool useLazyLoading;
        string DbName;
        public ERPCoreDbContext(string DbName, bool useLazyLoading = true) : base()
        {
            this.DbName = DbName;
            this.useLazyLoading = useLazyLoading;
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbName = $"ERPCore-{DbName}";
            string dbHost = @"sql-01.advancemaker.com";
            string userName = "sa";
            string password = "P@ssw0rd@1";

            var sb = new SqlConnectionStringBuilder()
            {
                InitialCatalog = dbName,
                DataSource = dbHost,
                UserID = userName,
                Password = password
            };

            if (this.useLazyLoading)
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(sb.ConnectionString);
            else
                optionsBuilder.UseSqlServer(sb.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KeeperCore.ERPNode.Models.Taxes.TaxCode>()
                .HasOne(x => x.AssignAccount)
                .WithMany(z => z.TaxCodeAssignAccounts)
                .HasForeignKey(x => x.AssignAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<KeeperCore.ERPNode.Models.Taxes.TaxCode>()
              .HasOne(x => x.CloseToAccount)
              .WithMany(z => z.TaxCodeCloseToAccounts)
              .HasForeignKey(x => x.CloseToAccountId)
              .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<KeeperCore.ERPNode.Models.Taxes.TaxCode>()
              .HasOne(x => x.TaxAccount)
              .WithMany(z => z.TaxCodeTaxAccounts)
              .HasForeignKey(x => x.TaxAccountId)
              .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<KeeperCore.ERPNode.Models.Taxes.TaxCode>()
              .HasOne(x => x.OutputTaxAccount)
              .WithMany(z => z.TaxCodeOutputTaxAccounts)
              .HasForeignKey(x => x.OutputTaxAccountId)
              .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<KeeperCore.ERPNode.Models.Taxes.TaxCode>()
              .HasOne(x => x.InputTaxAccount)
              .WithMany(z => z.TaxCodeInputTaxAccounts)
              .HasForeignKey(x => x.InputTaxAccountId)
              .OnDelete(DeleteBehavior.ClientSetNull);




            // Financial > Loan
            modelBuilder.Entity<Loan>()
               .HasOne(x => x.DebitTo_AssetAccount)
               .WithMany(z => z.Loan_DebitTo_AssetAccounts)
               .HasForeignKey(x => x.DebitTo_AssetAccountId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Loan>()
               .HasOne(x => x.CreditTo_LiabilityAccount)
               .WithMany(z => z.Loan_CreditTo_LiabilityAccounts)
               .HasForeignKey(x => x.CreditTo_LiabilityAccountId)
               .OnDelete(DeleteBehavior.ClientSetNull);


            // Financial > Lend
            modelBuilder.Entity<Lend>()
               .HasOne(x => x.CreditFrom_AssetAccount)
               .WithMany(z => z.Lend_CreditFrom_AssetAccounts)
               .HasForeignKey(x => x.CreditFrom_AssetAccountId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Lend>()
               .HasOne(x => x.DebitTo_AssetAccount)
               .WithMany(z => z.Lend_DebitTo_AssetAccounts)
               .HasForeignKey(x => x.DebitTo_AssetAccountId)
               .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<LendPayment>()
             .HasOne(x => x.LendAccount)
             .WithMany(z => z.LendPayment_LendAccounts)
             .HasForeignKey(x => x.LendAccountId)
             .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<LoanPayment>()
          .HasOne(x => x.LoanAccount)
          .WithMany(z => z.LoanPayment_LoanAccounts)
          .HasForeignKey(x => x.LoanAccountId)
          .OnDelete(DeleteBehavior.ClientSetNull);


            // Financial > Lend
            modelBuilder.Entity<FundTransfer>()
               .HasOne(x => x.WithDrawAccount)
               .WithMany(z => z.FundTransfer_WithDrawAccounts)
               .HasForeignKey(x => x.WithDrawAccountId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<FundTransfer>()
               .HasOne(x => x.DepositAccount)
               .WithMany(z => z.FundTransfer_DepositAccounts)
               .HasForeignKey(x => x.DepositAccountId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<FundTransfer>()
            .HasOne(x => x.BankFeeAccount)
            .WithMany(z => z.FundTransfer_BankFeeAccounts)
            .HasForeignKey(x => x.BankFeeAccountId)
            .OnDelete(DeleteBehavior.ClientSetNull);


            //Accounting > Fiscalyear
            modelBuilder.Entity<KeeperCore.ERPNode.Models.Accounting.FiscalYear>()
            .HasOne(x => x.CloseToAccount)
            .WithMany(z => z.FiscalYears_CloseToAccounts)
            .HasForeignKey(x => x.CloseToAccountId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<KeeperCore.ERPNode.Models.Items.Item>()
           .HasOne(x => x.PurchaseAccount)
           .WithMany(z => z.Items_PurchaseAccount)
           .HasForeignKey(x => x.PurchaseAccountId)
           .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<KeeperCore.ERPNode.Models.Items.Item>()
           .HasOne(x => x.IncomeAccount)
           .WithMany(z => z.Items_IncomeAccount)
           .HasForeignKey(x => x.IncomeAccountId)
           .OnDelete(DeleteBehavior.ClientSetNull);


            //Employee Payment > Period
            modelBuilder.Entity<KeeperCore.ERPNode.Models.Employees.EmployeePayment>()
           .HasOne(x => x.EmployeePaymentPeriod)
           .WithMany(z => z.EmployeePayments)
           .HasForeignKey(x => x.EmployeePaymentPeriodId)
           .OnDelete(DeleteBehavior.ClientSetNull);



            modelBuilder.Entity<CapitalActivity>()
            .HasOne(x => x.AssetAccount)
            .WithMany(z => z.CapitalActivity_AssetAccounts)
            .HasForeignKey(x => x.AssetAccountId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<CapitalActivity>()
             .HasOne(x => x.EquityAccount)
             .WithMany(z => z.CapitalActivity_EquityAccounts)
             .HasForeignKey(x => x.EquityAccountId)
             .OnDelete(DeleteBehavior.ClientSetNull);




            modelBuilder.Entity<GeneralPayment>()
                .HasOne(x => x.OptionalAssetAccount)
                .WithMany(z => z.GeneralPayment_OptionalAssetAccounts)
                .HasForeignKey(x => x.OptionalAssetAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<GeneralPayment>()
             .HasOne(x => x.AssetAccount)
             .WithMany(z => z.GeneralPayment_AssetAccounts)
             .HasForeignKey(x => x.AssetAccountId)
             .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<GeneralPayment>()
             .HasOne(x => x.LiabilityAccount)
             .WithMany(z => z.GeneralPayment_LiabilityAccounts)
             .HasForeignKey(x => x.LiabilityAccountId)
             .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<GeneralPayment>()
             .HasOne(x => x.ReceivableAccount)
             .WithMany(z => z.GeneralPayment_ReceivableAccounts)
             .HasForeignKey(x => x.ReceivableAccountId)
             .OnDelete(DeleteBehavior.ClientSetNull);





            modelBuilder.Entity<KeeperCore.ERPNode.Models.Transactions.Transaction>()
             .HasOne(x => x.Profile)
             .WithMany(z => z.Commercials)
             .HasForeignKey(x => x.ProfileId)
             .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<KeeperCore.ERPNode.Models.Transactions.Transaction>()
               .HasOne(x => x.TaxPeriod)
               .WithMany(z => z.Commercials)
               .HasForeignKey(x => x.TaxPeriodId)
               .OnDelete(DeleteBehavior.ClientSetNull);



            //Fixed Assets

            modelBuilder.Entity<KeeperCore.ERPNode.Models.Assets.DeprecateSchedule>()
            .HasOne(x => x.FixedAsset)
            .WithMany(z => z.DepreciationSchedules)
            .HasForeignKey(x => x.FixedAssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);





            //Items > Assisted
            modelBuilder.Entity<KeeperCore.ERPNode.Models.Items.AssistItem>()
               .HasOne(x => x.Profile)
               .WithMany(z => z.AssistItems)
               .HasForeignKey(x => x.ProfileId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<KeeperCore.ERPNode.Models.Items.AssistItem>()
             .HasOne(x => x.Item)
             .WithMany(z => z.AssistItems)
             .HasForeignKey(x => x.ItemId)
             .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<KeeperCore.ERPNode.Models.Employees.EmployeePaymentLine>()
              .HasOne(x => x.EmployeePayment)
              .WithMany(z => z.EmployeePaymentLines)
              .HasForeignKey(x => x.EmployeePaymentId)
              .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<KeeperCore.ERPNode.Models.Estimations.QuoteItem>()
           .HasOne(x => x.Quote)
           .WithMany(z => z.QuoteItems)
           .HasForeignKey(x => x.QuoteId)
           .OnDelete(DeleteBehavior.ClientSetNull);

        }



        public DbSet<Models.Datum.DataItem> DataItems { get; set; }
        public DbSet<Models.Location> Locations { get; set; }
        public DbSet<CapitalActivity> CapitalActivities { get; set; }
        public DbSet<Models.Security.User> Users { get; set; }

        #region Items
        public DbSet<Models.Items.Item> Items { get; set; }
        public DbSet<Models.Items.GroupItem> GroupItems { get; set; }
        public DbSet<Models.Items.AssistItem> AssistItems { get; set; }
        public DbSet<Models.Items.ItemGroup> ItemGroups { get; set; }
        public DbSet<Models.Items.Brand> Brands { get; set; }
        public DbSet<Models.Items.ItemImage> ItemImages { get; set; }
        public DbSet<Models.Items.ItemParameterType> ItemParameterTypes { get; set; }
        public DbSet<Models.Items.ItemParameter> ItemParameters { get; set; }
        public DbSet<Models.Items.PriceRange> PriceRanges { get; set; }
        public DbSet<Models.Items.PriceRangeItem> PriceRangeItems { get; set; }
        #endregion


        #region Accounting
        public DbSet<Models.Accounting.FiscalYear> FiscalYears { get; set; }
        public DbSet<PeriodAccountBalance> PeriodAccountsBalances { get; set; }
        public DbSet<Models.Accounting.FiscalYears.PeriodItemCOGS> PeriodItemsCOGS { get; set; }
        public DbSet<Account> AccountItems { get; set; }
        public DbSet<DefaultAccount> SystemAccounts { get; set; }

        public DbSet<AccountBalance> HistoryBalanceItems { get; set; }
        public DbSet<Models.Accounting.TransactionLedger> TransactionLedgers { get; set; }
        public DbSet<Models.Accounting.Ledger> Ledgers { get; set; }
        public DbSet<Models.AccountingEntries.JournalEntryType> JournalEntryTypes { get; set; }
        public DbSet<Models.AccountingEntries.JournalEntry> JournalEntries { get; set; }
        public DbSet<Models.AccountingEntries.JournalEntryItem> JournalEntryItems { get; set; }
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
        public DbSet<Models.Transactions.Transaction> Commercials { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<Models.Transactions.Commercials.ShippingTerm> ShippingTerms { get; set; }
        public DbSet<Models.Transactions.Commercials.ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Models.Transactions.TransactionItem> CommercialItems { get; set; }

        public DbSet<Models.Transactions.CommercialShipment> CommercialShipments { get; set; }
        public DbSet<Models.Transactions.CommercialDailyBalance> CommercialDailyBalances { get; set; }

        #endregion



        public DbSet<Models.Projects.Project> Projects { get; set; }
        public DbSet<Models.Projects.WorkTask> WorkTasks { get; set; }
        public DbSet<Models.Projects.Material> Materials { get; set; }


        public DbSet<Models.Assets.Asset> FixedAssets { get; set; }
        public DbSet<Models.Assets.AssetType> FixedAssetTypes { get; set; }
        public DbSet<Models.Assets.DeprecateSchedule> DeprecateSchedules { get; set; }





        #region Financial
        public DbSet<FundTransfer> FundTransfers { get; set; }
        public DbSet<RetentionType> RetentionTypes { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanPayment> LoanPayments { get; set; }
        public DbSet<Lend> Lends { get; set; }
        public DbSet<LendPayment> LendPayments { get; set; }
        public DbSet<GeneralPayment> GeneralPayments { get; set; }
        public DbSet<LiabilityPayment> LiabilityPayments { get; set; }
        public DbSet<ReceivePayment> ReceivePayments { get; set; }
        public DbSet<SupplierPayment> SupplierPayments { get; set; }




        #endregion






        #region Taxes
        public DbSet<Models.Taxes.TaxGroup> TaxGroups { get; set; }
        public DbSet<Models.Taxes.TaxCode> TaxCodes { get; set; }
        public DbSet<Models.Taxes.TaxRate> TaxRate { get; set; }
        public DbSet<Models.Taxes.IncomeTax> IncomeTaxs { get; set; }
        public DbSet<Models.Taxes.TaxPeriod> TaxPeriods { get; set; }
        #endregion




        #region Employees
        public DbSet<Models.Employees.Employee> Employees { get; set; }
        public DbSet<Models.Employees.EmployeePosition> EmployeePositions { get; set; }
        public DbSet<Models.Employees.EmployeePaymentPeriod> EmployeePaymentPeriods { get; set; }
        public DbSet<Models.Employees.EmployeePaymentType> EmployeePaymentTypes { get; set; }
        public DbSet<Models.Employees.EmployeePaymentLine> EmployeePaymentLines { get; set; }
        public DbSet<Models.Employees.EmployeePayment> EmployeePayments { get; set; }
        public DbSet<Models.Employees.EmployeeLeave> EmployeeLeaves { get; set; }
        public DbSet<Models.Employees.EmployeePaymentTemplate> EmployeePaymentTemplates { get; set; }
        public DbSet<Models.Employees.EmployeePaymentTemplateItem> EmployeePaymentTemplateItems { get; set; }
        #endregion


        public DbSet<Models.Documents.Document> Documents { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }
    }


}