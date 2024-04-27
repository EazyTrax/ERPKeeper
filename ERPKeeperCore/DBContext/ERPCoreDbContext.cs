using Microsoft.EntityFrameworkCore;
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

            if (this.useLazyLoading)
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(sb.ConnectionString);
            else
                optionsBuilder.UseSqlServer(sb.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
               .SelectMany(t => t.GetProperties())
               .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                if (string.IsNullOrEmpty(property.GetColumnType()))
                {
                    property.SetColumnType("decimal(18, 2)");
                }
            }

        }



        public DbSet<DataItem> DataItems { get; set; }

        public DbSet<CapitalActivity> CapitalActivities { get; set; }



        public DbSet<Models.Items.Item> Items { get; set; }

        public DbSet<Models.Items.ItemGroup> ItemGroups { get; set; }
        public DbSet<Models.Items.Brand> Brands { get; set; }

        public DbSet<FiscalYear> FiscalYears { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<DefaultAccount> DefaultAccounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileRole> ProfileRoles { get; set; }
        public DbSet<ProfileBankAccount> ProfileBankAccounts { get; set; }
        public DbSet<ProfileAddress> ProfileAddresses { get; set; }
        public DbSet<ProfileContact> ProfileContacts { get; set; }




        public DbSet<Customer> Customers { get; set; }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }


        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }

        public DbSet<PaymentTerm> PaymentTerms { get; set; }



        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Project> Projects { get; set; }


        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<AssetDeprecateSchedule> DeprecateSchedules { get; set; }












        #region Taxes
        public DbSet<TaxCode> TaxCodes { get; set; }
        public DbSet<TaxPeriod> TaxPeriods { get; set; }
        #endregion







    }


}