using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Data.SqlClient;
using KeeperCore.ERPNode.Models;
using System.ComponentModel.DataAnnotations.Schema;
using KeeperCore.ERPNode.Models;
using KeeperCore.ERPNode.Models.Transactions.Enums;

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


        }



        public DbSet<DataItem> DataItems { get; set; }

        public DbSet<CapitalActivity> CapitalActivities { get; set; }



        public DbSet<Models.Items.Item> Items { get; set; }

        public DbSet<Models.Items.ItemGroup> ItemGroups { get; set; }
        public DbSet<Models.Items.Brand> Brands { get; set; }

        public DbSet<FiscalYear> FiscalYears { get; set; }
        public DbSet<Account> AccountItems { get; set; }
        public DbSet<DefaultAccount> SystemAccounts { get; set; }
        public DbSet<Models.Profile> Profiles { get; set; }
        public DbSet<Models.ProfileRole> ProfileRoles { get; set; }
        public DbSet<Models.ProfileBankAccount> ProfileBankAccounts { get; set; }
        public DbSet<Models.ProfileAddress> ProfileAddresses { get; set; }
        public DbSet<Models.ProfileContact> ProfileContacts { get; set; }
        public DbSet<Models.Sale> Sales { get; set; }
        public DbSet<Models.Purchase> Purchases { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<SaleItem> CommercialItems { get; set; }




        public DbSet<Models.Project> Projects { get; set; }


        public DbSet<Models.Asset> Assets { get; set; }
        public DbSet<Models.AssetType> AssetTypes { get; set; }
        public DbSet<Models.AssetDeprecateSchedule> DeprecateSchedules { get; set; }












        #region Taxes
        public DbSet<Models.TaxCode> TaxCodes { get; set; }
        public DbSet<Models.TaxPeriod> TaxPeriods { get; set; }
        #endregion







    }


}