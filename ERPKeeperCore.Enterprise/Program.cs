using ERPKeeper.Node.DAL;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Items.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;
using static System.Collections.Specialized.BitVector32;

namespace ERPKeeperCore.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Enterprises = new string[] { "tec", "bit" };

            foreach (var enterpriseDB in Enterprises)
            {
                Console.WriteLine($"###{enterpriseDB}########################################################");
                var newOrganization = new ERPKeeperCore.Enterprise.EnterpriseRepo(enterpriseDB, true);
                newOrganization.ErpCOREDBContext.Database.Migrate();
                var oldOrganization = new ERPKeeper.Node.DAL.Organization(enterpriseDB, true);
                Console.WriteLine($">{newOrganization.ErpCOREDBContext.Database.GetConnectionString()}");
                Console.WriteLine("###########################################################" + Environment.NewLine + Environment.NewLine);

                var migrationTool = new ERPMigrationTool(enterpriseDB);
                //migrationTool.Migrate();

                newOrganization.ChartOfAccount.CreateOpeningJournalEntry();
                PostLedgers(newOrganization);
                newOrganization.FiscalYears.UpdateAccountBalance();
            }
        }
        private static void PostLedgers(EnterpriseRepo newOrganization)
        {
            //Capital Activities
            //organization.CapitalActivities.PostLedger();

            //Commercial Section
            newOrganization.Sales.PostToTransactions();
            newOrganization.ReceivePayments.PostToTransactions();

            newOrganization.Purchases.PostToTransactions();
            newOrganization.SupplierPayments.PostToTransactions();



            //Financial Section
            newOrganization.FundTransfers.PostToTransactions();
            newOrganization.Loans.PostToTransactions();
            newOrganization.Lends.PostToTransactions();
            newOrganization.LiabilityPayments.PostToTransactions();

            //Taxes Section
            //organization.TaxPeriods.PostLedger();
            newOrganization.IncomeTaxes.PostToTransactions();

            //Employee Section
            newOrganization.EmployeePayments.PostToTransactions();

            //Other Section
            newOrganization.JournalEntries.PostToTransactions();

            //Assets
            newOrganization.Assets.PostToTransactions();
            newOrganization.AssetDeprecateSchedules.PostToTransactions();

            //organization.RetirementFixedAssets.UnPost();
            //organization.RetirementFixedAssets.PostLedger();
            //organization.FiscalYears.PostLedger();

            //Other Section
            newOrganization.IncomeTaxes.PostToTransactions();
            newOrganization.TaxPeriods.PostToTransactions(true);

        }
        private static void GeneralOperations(EnterpriseRepo newOrganization)
        {

            newOrganization.ErpCOREDBContext.FundTransferItems
                .Where(m => m.Debit == 0)
                .ToList()
                .ForEach(model =>
                {
                    Console.WriteLine($"{model.Account.Name}");
                    newOrganization.ErpCOREDBContext.FundTransferItems.Remove(model);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                });
        }
        private static void CreateTransactionForSales(EnterpriseRepo newOrganization)
        {
            var sales = newOrganization.ErpCOREDBContext.Sales
                .Where(x => x.TransactionId == null)
                .OrderBy(x => x.Date)
                .ToList();
            sales.ForEach(sale =>
            {
                Console.WriteLine($"SALE: {sale.No}-{sale.Name}");
                var transaction = new ERPKeeperCore.Enterprise.Models.Accounting.Transaction()
                {
                    Date = sale.Date,
                    Type = TransactionType.Sale,
                };

                newOrganization.ErpCOREDBContext.Transactions.Add(transaction);
                sale.TransactionId = transaction.Id;
                newOrganization.ErpCOREDBContext.SaveChanges();

            });
        }
        private static void CreateTransactionForPurchases(EnterpriseRepo newOrganization)
        {
            var purchases = newOrganization.ErpCOREDBContext.Purchases
                .Where(x => x.TransactionId == null)
                .OrderBy(x => x.Date)
                .ToList();

            purchases.ForEach(purchase =>
            {
                Console.WriteLine($"PUR: {purchase.No}-{purchase.Name}");
                var transaction = new ERPKeeperCore.Enterprise.Models.Accounting.Transaction()
                {
                    Date = purchase.Date,
                    Type = TransactionType.Purchase,
                };

                newOrganization.ErpCOREDBContext.Transactions.Add(transaction);
                purchase.TransactionId = transaction.Id;
                newOrganization.ErpCOREDBContext.SaveChanges();

            });
        }
    }
}
