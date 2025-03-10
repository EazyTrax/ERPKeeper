﻿using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Data.SqlClient;
using ERPKeeperCore.Enterprise.Models;
using System.ComponentModel.DataAnnotations.Schema;
using ERPKeeperCore.Enterprise.Models.Profiles;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Assets;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Suppliers;
using ERPKeeperCore.Enterprise.Models.Projects;
using ERPKeeperCore.Enterprise.Models.Financial;
using ERPKeeperCore.Enterprise.Models.Employees;

namespace ERPKeeperCore.Enterprise.DBContext
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
            string dbHost = @"172.19.2.5";
            string userName = "sa";
            string password = "P@ssw0rd@1";

            var sb = new SqlConnectionStringBuilder()
            {
                InitialCatalog = dbName,
                DataSource = dbHost,
                UserID = userName,
                Password = password,
                TrustServerCertificate = true
            };
            Console.WriteLine(sb.ConnectionString);

            if (this.useLazyLoading)
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(sb.ConnectionString);
            else
                optionsBuilder.UseSqlServer(sb.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //foreach (var property in modelBuilder.Model.GetEntityTypes()
            //   .SelectMany(t => t.GetProperties())
            //   .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            //{
            //    if (string.IsNullOrEmpty(property.GetColumnType()))
            //    {
            //        property.SetColumnType("decimal(18, 4)");
            //    }
            //}
        }

        public DbSet<Models.Setting.Branch> Branches { get; set; }
        public DbSet<DataItem> DataItems { get; set; }

        public DbSet<CapitalActivity> CapitalActivities { get; set; }
        public DbSet<Models.Items.Item> Items { get; set; }
        public DbSet<Models.Items.ItemGroup> ItemGroups { get; set; }
        public DbSet<Models.Items.Brand> Brands { get; set; }
        public DbSet<FiscalYear> FiscalYears { get; set; }
        public DbSet<FiscalYearAccountBalance> FiscalYearAccountBalances { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountBalance> AccountBalances { get; set; }


        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<JournalEntryItem> JournalEntryItems { get; set; }
        public DbSet<JournalEntryType> JournalEntryTypes { get; set; }
        public DbSet<DefaultAccount> DefaultAccounts { get; set; }



        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionLedger> TransactionLedgers { get; set; }




        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileRole> ProfileRoles { get; set; }
        public DbSet<ProfileBankAccount> ProfileBankAccounts { get; set; }
        public DbSet<ProfileAddress> ProfileAddresses { get; set; }
        public DbSet<ProfileContact> ProfileContacts { get; set; }




        /***
         * Customers
         */

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerItem> CustomerItems { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<ReceivePayment> ReceivePayments { get; set; }
        public DbSet<SaleQuote> SaleQuotes { get; set; }
        public DbSet<SaleQuoteItem> SaleQuoteItems { get; set; }




        /***
         * Suppliers
         */


        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierItem> SupplierItems { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<SupplierPayment> SupplierPayments { get; set; }
        public DbSet<PurchaseQuote> PurchaseQuotes { get; set; }
        public DbSet<PurchaseQuoteItem> PurchaseQuoteItems { get; set; }




        public DbSet<Models.Investors.Investor> Investors { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Project> Projects { get; set; }




        public DbSet<Asset> Assets { get; set; }
        public DbSet<ObsoleteAsset> ObsoleteAssets { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<AssetDeprecateSchedule> AssetDeprecateSchedules { get; set; }




        #region Financial
        public DbSet<Models.Financial.FundTransfer> FundTransfers { get; set; }
        public DbSet<Models.Financial.FundTransferItem> FundTransferItems { get; set; }
        public DbSet<Models.Financial.RetentionType> RetentionTypes { get; set; }
        public DbSet<Models.Financial.RetentionPeriod> RetentionPeriods { get; set; }
        public DbSet<Models.Financial.LiabilityPayment> LiabilityPayments { get; set; }
        public DbSet<Models.Financial.LiabilityPaymentPayFromAccount> LiabilityPaymentPayFromAccounts { get; set; }

        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<Models.Financial.Loan> Loans { get; set; }
        public DbSet<Models.Financial.LoanReturn> LoanReturns { get; set; }
        public DbSet<Models.Financial.Lend> Lends { get; set; }
        public DbSet<Models.Financial.LendReturn> LendReturns { get; set; }

        #endregion



        #region Employement

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }
        public DbSet<EmployeePayment> EmployeePayments { get; set; }
        public DbSet<EmployeePaymentItem> EmployeePaymentItems { get; set; }
        public DbSet<EmployeePaymentPeriod> EmployeePaymentPeriods { get; set; }
        public DbSet<EmployeePaymentType> EmployeePaymentTypes { get; set; }
        #endregion





        #region Taxes
        public DbSet<TaxCode> TaxCodes { get; set; }
        public DbSet<TaxPeriod> TaxPeriods { get; set; }
        public DbSet<IncomeTax> IncomeTaxes { get; set; }
        #endregion



        public DbSet<Models.Security.Member> Members { get; set; }

        public DbSet<Models.Setting.MemoTemplate> MemoTemplates { get; set; }



        public DbSet<Models.Storage.Document> Documents { get; set; }
        public DbSet<Models.Storage.NoteItem> NoteItems { get; set; }




        public DbSet<Models.Logistic.Shipment> Shipments { get; set; }



        public DbSet<Models.Logistic.LogisticProvider> LogisticProviders { get; set; }


    }


}