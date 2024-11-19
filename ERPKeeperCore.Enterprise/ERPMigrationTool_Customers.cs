using ERPKeeper.Node.DAL;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPKeeperCore.CMD
{
    public partial class ERPMigrationTool
    {
        public void Copy_Customers()
        {
            Console.WriteLine("> Copy_Customers");

            Copy_Customers_Customers();
            Copy_Customers_Sales();
            Copy_Customers_SaleItems();
         //   Copy_Customers_SaleQuotes();
         //   Copy_Customers_SaleQuoteItems();
         //   Copy_Customers_ReceivePayments();
        }


        public void Copy_Customers_Customers()
        {
            Console.WriteLine("> Copy_Customers_Customers");

            var existModelIds = newOrganization.ErpCOREDBContext.Customers
              .Select(x => x.Id)
              .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.Customers
                .Where(i => !existModelIds.Contains(i.ProfileUid))
                .ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.ProfileUid}-{a.Profile.Name}");

                var exist = newOrganization.ErpCOREDBContext.Customers.FirstOrDefault(x => x.Id == a.ProfileUid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Customers.Customer()
                    {
                        Id = a.ProfileUid,
                    };
                    newOrganization.ErpCOREDBContext.Customers.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }


        public void Copy_Customers_ReceivePayments()
        {
            Console.WriteLine("> Copy_Customers_ReceivePayments");

            // Use HashSet for more efficient lookups
            var existingReceivePaymentIds = newOrganization.ErpCOREDBContext.ReceivePayments
                .Select(x => x.Id)
                .ToHashSet();

            // Include necessary related data in single query
            var oldSalesWithPayments = oldOrganization.ErpNodeDBContext.Sales
                .Where(s => s.GeneralPaymentUid != null)
                .Include(s => s.GeneralPayment)
                .Include(s => s.GeneralPayment.RetentionType)
                .OrderByDescending(s => s.GeneralPayment.TrnDate)
                .ToList();

            // Batch process payments
            var batchSize = 100;
            var paymentsToAdd = new List<Enterprise.Models.Customers.ReceivePayment>();
            var paymentsToUpdate = new List<Enterprise.Models.Customers.ReceivePayment>();

            foreach (var batch in oldSalesWithPayments.Chunk(batchSize))
            {
                var saleIds = batch.Select(s => s.Uid).ToList();
                var existingPayments = newOrganization.ErpCOREDBContext.ReceivePayments
                    .Where(rp => rp.SaleId != null && saleIds.Contains((Guid)rp.SaleId))
                    .ToDictionary(rp => rp.SaleId);

                foreach (var oldSale in batch)
                {
                    if (!existingPayments.TryGetValue(oldSale.Uid, out var existReceivePayment))
                    {
                        Console.WriteLine($"SALE: {oldSale.Name}");
                        var newPayment = new Enterprise.Models.Customers.ReceivePayment
                        {
                            SaleId = oldSale.Uid,
                            Amount = oldSale.Total,
                            Date = oldSale.GeneralPayment.TrnDate,
                            Reference = oldSale.GeneralPayment.Reference,
                            AmountBankFee = oldSale.GeneralPayment.BankFeeAmount,
                            AmountDiscount = oldSale.GeneralPayment.DiscountAmount,
                            PayToAccountId = oldSale.GeneralPayment.AssetAccountUid,
                            Receivable_Asset_AccountId = oldSale.GeneralPayment.ReceivableAccountUid,
                            RetentionTypeId = oldSale.GeneralPayment.RetentionTypeGuid
                        };

                        if (newPayment.RetentionTypeId != null)
                        {
                            newPayment.AmountRetention = oldSale.GeneralPayment.RetentionType.Rate * oldSale.LinesTotal / 100;
                        }

                        paymentsToAdd.Add(newPayment);
                    }
                    else if (existReceivePayment.Receivable_Asset_AccountId != oldSale.GeneralPayment.ReceivableAccountUid)
                    {
                        existReceivePayment.Receivable_Asset_AccountId = oldSale.GeneralPayment.ReceivableAccountUid;
                        paymentsToUpdate.Add(existReceivePayment);
                    }
                }

                // Batch save changes
                if (paymentsToAdd.Any())
                {
                    newOrganization.ErpCOREDBContext.ReceivePayments.AddRange(paymentsToAdd);
                    paymentsToAdd.Clear();
                }

                if (paymentsToUpdate.Any())
                {
                    newOrganization.ErpCOREDBContext.ReceivePayments.UpdateRange(paymentsToUpdate);
                    paymentsToUpdate.Clear();
                }

                newOrganization.SaveChanges();
            }
        }
        public void Copy_Customers_ReceivePayments_oldWay()
        {
            Console.WriteLine("> Copy_Customers_ReceivePayments");


            var existReceivePaymentIds = newOrganization.ErpCOREDBContext
                .ReceivePayments
                .Select(x => x.Id)
                .ToList();

            var oldSalesWithPayments = oldOrganization.ErpNodeDBContext
                .Sales
                .Where(s => s.GeneralPaymentUid != null)
                .OrderByDescending(s => s.GeneralPayment.TrnDate)
                .ToList();

            oldSalesWithPayments.ForEach(oldSale =>
                {
                    var existReceivePayment = newOrganization.ErpCOREDBContext
                        .ReceivePayments
                        .FirstOrDefault(s => s.SaleId == oldSale.Uid);

                    if (existReceivePayment == null)
                    {
                        Console.WriteLine($"SALE: {oldSale.Name}");

                        existReceivePayment = new Enterprise.Models.Customers.ReceivePayment()
                        {
                            SaleId = oldSale.Uid,
                            Amount = oldSale.Total,
                            Date = oldSale.GeneralPayment.TrnDate,
                            Reference = oldSale.GeneralPayment.Reference,
                            AmountBankFee = oldSale.GeneralPayment.BankFeeAmount,
                            AmountDiscount = oldSale.GeneralPayment.DiscountAmount,
                            PayToAccountId = oldSale.GeneralPayment.AssetAccountUid,
                            Receivable_Asset_AccountId = oldSale.GeneralPayment.ReceivableAccountUid,
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
                        existReceivePayment.Receivable_Asset_AccountId = oldSale.GeneralPayment.ReceivableAccountUid;
                    }
                });
        }



        public void Copy_Customers_Sales()
        {

            Console.WriteLine("> Copy_Customers_Sales");

            var existModelIds = newOrganization.ErpCOREDBContext.Sales
               .Select(x => x.Id)
               .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.Sales
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();


            oldModels.ForEach(oldModel =>
            {
                Console.WriteLine($"SALE:{oldModel.Name}-{oldModel.Uid}");
                var exist = newOrganization.ErpCOREDBContext.Sales.FirstOrDefault(x => x.Id == oldModel.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"> Add");

                    exist = new ERPKeeperCore.Enterprise.Models.Customers.Sale()
                    {
                        Id = oldModel.Uid,
                        Date = oldModel.TrnDate,
                        No = oldModel.No,
                        Name = oldModel.Name,
                        Memo = oldModel.Memo,
                        Status = SaleStatus.Open,
                        TaxCodeId = oldModel.TaxCodeId,
                        CustomerId = (Guid)oldModel.ProfileGuid,
                        TaxPeriodId = oldModel.TaxPeriodId,
                        LinesTotal = oldModel.LinesTotal,
                        Tax = oldModel.Tax,
                    };

                    newOrganization.ErpCOREDBContext.Sales.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {
                    exist.Reference = oldModel.Reference;
                }


            });

            newOrganization.ErpCOREDBContext.SaveChanges();
        }
        public void Copy_Customers_SaleItems()
        {
            Console.WriteLine("> Copy_Customers_SaleItems");

            var existModelIds = newOrganization.ErpCOREDBContext.SaleItems
                .Select(x => x.Id)
                .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext
                .CommercialItems
                .Where(i => !existModelIds.Contains(i.Uid))
                .Where(m => m.TransactionGuid != null)
                .Where(i => i.TransactionType == ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.Sale)
                .Where(i => i.Amount > 0 && i.UnitPrice > 0)
                .ToList();

            oldModels.ForEach((Action<ERPKeeper.Node.Models.Transactions.CommercialItem>)(oldModel =>
            {
                var existItem = newOrganization.ErpCOREDBContext
                .SaleItems
                .Find(oldModel.Uid);

                if (existItem == null)
                {
                    Console.WriteLine($"> Add {oldModel.Uid}: {oldModel.ItemPartNumber}: {oldModel.Amount}:{oldModel.UnitPrice}: ");


                    existItem = new ERPKeeperCore.Enterprise.Models.Customers.SaleItem()
                    {
                        Id = oldModel.Uid,
                        SaleId = (Guid)oldModel.TransactionGuid,
                        ItemId = oldModel.ItemGuid,
                        Quantity = oldModel.Amount,
                        Price = oldModel.UnitPrice,
                        PartNumber = oldModel.ItemPartNumber,
                        Description = oldModel.ItemDescription,
                        Memo = oldModel.Memo,
                        DiscountPercent = oldModel.DiscountPercent,
                    };

                    newOrganization.ErpCOREDBContext.SaleItems.Add(existItem);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"> Exists {oldModel.ItemPartNumber}: {oldModel.Amount}:{oldModel.UnitPrice}: ");
                }
            }));


        }
        public void Copy_Customers_SaleQuotes()
        {

            Console.WriteLine("> Copy_Customers_SaleQuotes");
            var existModelIds = newOrganization.ErpCOREDBContext.SaleQuotes
               .Select(x => x.Id)
               .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.SaleEstimates
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();


            oldModels.ForEach(oldModel =>
            {
                Console.WriteLine($"SALE:{oldModel.Name}-{oldModel.Uid}");
                var exist = newOrganization.ErpCOREDBContext.SaleQuotes.FirstOrDefault(x => x.Id == oldModel.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"> Add SaleQuote: {oldModel.Name}");

                    exist = new ERPKeeperCore.Enterprise.Models.Customers.SaleQuote()
                    {
                        Id = oldModel.Uid,
                        Date = oldModel.TrnDate,
                        No = oldModel.No,
                        Name = oldModel.Name,
                        Memo = oldModel.Memo,
                        Status = (SaleQuoteStatus)oldModel.Status,
                        TaxCodeId = oldModel.TaxCodeGuid,
                        CustomerId = (Guid)oldModel.ProfileGuid,
                        LinesTotal = oldModel.LinesTotal,
                        Tax = oldModel.Tax,
                        Reference = oldModel.Reference,
                    };

                    newOrganization.ErpCOREDBContext.SaleQuotes.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {
                    exist.Reference = oldModel.Reference;
                }


            });

            newOrganization.ErpCOREDBContext.SaveChanges();
        }
        public void Copy_Customers_SaleQuoteItems()
        {

            Console.WriteLine("> Copy_Customers_SaleQuoteItems");


            var existModelIds = newOrganization.ErpCOREDBContext.SaleQuoteItems
                .Select(x => x.Id)
                .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext
                .EstimateItems
                .Where(i => !existModelIds.Contains(i.Uid))
                .Where(i => i.Quote.TransactionType == ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.SaleQuote)
                .Where(i => i.Amount > 0 && i.UnitPrice > 0)
                .ToList();

            oldModels.ForEach(oldModel =>
            {
                var existItem = newOrganization.ErpCOREDBContext
                .SaleQuoteItems
                .Find(oldModel.Uid);

                if (existItem == null)
                {
                    Console.WriteLine($"> Add SaleQuoteItems {oldModel.Uid}: {oldModel.ItemPartNumber}: {oldModel.Amount}:{oldModel.UnitPrice}: ");


                    existItem = new ERPKeeperCore.Enterprise.Models.Customers.SaleQuoteItem()
                    {
                        Id = oldModel.Uid,
                        QuoteId = (Guid)oldModel.QuoteId,
                        ItemId = (Guid)oldModel.ItemGuid,
                        Quantity = (int)oldModel.Amount,
                        Price = oldModel.UnitPrice,
                        PartNumber = oldModel.ItemPartNumber,
                        Description = oldModel.ItemDescription,
                        Memo = oldModel.Memo,
                        DiscountPercent = oldModel.DiscountPercent,
                    };

                    newOrganization.ErpCOREDBContext.SaleQuoteItems.Add(existItem);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"> Exists {oldModel.ItemPartNumber}: {oldModel.Amount}:{oldModel.UnitPrice}: ");
                }
            });


        }
    }
}
