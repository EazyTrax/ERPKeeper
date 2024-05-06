using ERPKeeper.Node.DAL;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Items.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;

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

                //CopyReceivePayments(newOrganization, oldOrganization);
                //CopySupplierPayments(newOrganization, oldOrganization);
                // PostLedgers(newOrganization);

                var migrationTool = new ERPMigrationTool(enterpriseDB);
                migrationTool.Migrate();

          
            }
        }
   
        
        
        private static void CopyReceivePayments(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            oldOrganization.ErpNodeDBContext
                .Sales
                .Where(s => s.GeneralPaymentUid != null)
                .OrderByDescending(s => s.GeneralPayment.TrnDate)
                .ToList()
                .ForEach(oldSale =>
                {
                    Console.WriteLine($"SALE: {oldSale.Name}");

                    var existReceivePayment = newOrganization.ErpCOREDBContext
                        .ReceivePayments
                        .FirstOrDefault(s => s.SaleId == oldSale.Uid);

                    if (existReceivePayment == null)
                    {
                        existReceivePayment = new Enterprise.Models.Customers.ReceivePayment()
                        {
                            SaleId = oldSale.Uid,
                            Amount = oldSale.Total,
                            Date = oldSale.GeneralPayment.TrnDate,
                            Reference = oldSale.GeneralPayment.Reference,
                            AmountBankFee = oldSale.GeneralPayment.BankFeeAmount,
                            AmountDiscount = oldSale.GeneralPayment.DiscountAmount,
                            PayToAccountId = oldSale.GeneralPayment.AssetAccountUid,
                            ReceivableAccountId = oldSale.GeneralPayment.ReceivableAccountUid,
                            RetentionTypeId = oldSale.GeneralPayment.RetentionTypeGuid,


                        };

                        if (existReceivePayment.RetentionTypeId != null)
                            existReceivePayment.AmountRetention = oldSale.GeneralPayment.RetentionType.Rate * oldSale.LinesTotal / 100;

                        newOrganization.ErpCOREDBContext
                            .ReceivePayments.Add(existReceivePayment);

                        newOrganization.SaveChanges();
                    }
                    else
                    {
                        existReceivePayment.ReceivableAccountId = oldSale.GeneralPayment.ReceivableAccountUid;
                    }
                });
        }

        private static void PostLedgers(EnterpriseRepo newOrganization)
        {
            //organization.OpeningEntries.PostLedger();

            //Capital Activities
            //organization.CapitalActivities.PostLedger();

            ////Commercial Section
            newOrganization.Sales.PostToTransactions();
            newOrganization.Purchases.PostToTransactions();

            //Financial Section
            //organization.ReceivePayments.PostLedger();
            //organization.SupplierPayments.PostLedger();
            //organization.LiabilityPayments.PostLedger();

            newOrganization.FundTransfers.PostToTransactions();
            //organization.Loans.PostLedger();
            //organization.Lends.PostLedger();

            //Taxes Section
            // organization.TaxPeriods.PostLedger();
            // organization.IncomeTaxes.PostLedger();

            //Employee Section
            //  organization.EmployeePayments.PostLedger();

            //Other Section
            newOrganization.JournalEntries.PostToTransactions();

            //Assets
            //  organization.FixedAssets.PostLedger();
            //  organization.FixedAssetDreprecateSchedules.PostLedger();

            //organization.RetirementFixedAssets.UnPost();
            //  organization.RetirementFixedAssets.PostLedger();

            // organization.FiscalYears.PostLedger();


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
