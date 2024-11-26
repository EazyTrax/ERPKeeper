using ERPKeeper.Node.DAL;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Suppliers.Enums;
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
        public void Copy_Suppliers()
        {
            Console.WriteLine("> Copy_Suppliers");

            Copy_Suppliers_Suppliers();
            Copy_Suppliers_Purchases();
            Copy_Suppliers_PurchaseItems();
           // Copy_Suppliers_PurchaseQuotes();
           //Copy_Suppliers_PurchaseQuoteItems();
           // Copy_Supplier_Payments();
        }
        public void Copy_Suppliers_Suppliers()
        {
            Console.WriteLine("> Copy_Suppliers_Suppliers");

            var existModelIds = newOrganization.ErpCOREDBContext.Suppliers
              .Select(x => x.Id)
              .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.Suppliers
                .Where(i => !existModelIds.Contains(i.ProfileUid))
                .ToList();


            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.ProfileUid}-{a.Profile.Name}");

                var exist = newOrganization.ErpCOREDBContext.Suppliers.FirstOrDefault(x => x.Id == a.ProfileUid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Suppliers.Supplier()
                    {
                        Id = a.ProfileUid,
                        Status = Enterprise.Models.ProfileStatus.Active,
                    };

                    newOrganization.ErpCOREDBContext.Suppliers.Add(exist);
                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }


        public void Copy_Suppliers_Purchases()
        {
            Console.WriteLine("> Copy_Suppliers_Purchases");

            var existModelIds = newOrganization.ErpCOREDBContext.Purchases
                .Select(x => x.Id)
                .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.Purchases
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();

            oldModels.ForEach(oldModel =>
            {
                Console.WriteLine($"PUR:{oldModel.Name}-{oldModel.Uid}");

                var exist = newOrganization.ErpCOREDBContext
                .Purchases
                .Find(oldModel.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"> Add {oldModel.Name}");
                    Console.WriteLine($"> supplier {oldModel.ProfileGuid}");

                    var supplier = newOrganization.ErpCOREDBContext
                       .Suppliers
                       .Find(oldModel.ProfileGuid);


                    //if (supplier == null)
                    //{
                    //    var profile = newOrganization.ErpCOREDBContext
                    //     .Profiles
                    //     .Find(oldModel.ProfileGuid);

                    //    if (profile != null)
                    //    {
                    //        Console.WriteLine($"> profile {profile.Name}");

                    //        supplier = new ERPKeeperCore.Enterprise.Models.Suppliers.Supplier()
                    //        {
                    //            Id = profile.Id,
                    //            Status =  Enterprise.Models.ProfileStatus.Active,
                    //        };
                    //        newOrganization.ErpCOREDBContext.Suppliers.Add(supplier);
                    //        newOrganization.ErpCOREDBContext.SaveChanges();

                    //    }
                    //}

                    exist = new ERPKeeperCore.Enterprise.Models.Suppliers.Purchase()
                    {
                        Id = oldModel.Uid,
                        Date = oldModel.TrnDate,
                        No = oldModel.No,
                        Name = oldModel.Name,
                        Memo = oldModel.Memo,
                        Status = Enterprise.Models.Suppliers.Enums.PurchaseStatus.Open,
                        TaxCodeId = oldModel.TaxCodeId,
                        SupplierId = (Guid)oldModel.ProfileGuid,
                        SupplierAddressId = oldModel.ProfileAddressGuid,
                        TaxPeriodId = oldModel.TaxPeriodId,
                        LinesTotal = oldModel.LinesTotal,
                        Tax = oldModel.Tax,
                    };

                    newOrganization.ErpCOREDBContext.Purchases.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {
                    exist.Reference = oldModel.Reference;
                }


            });
            newOrganization.ErpCOREDBContext.SaveChanges();
        }

        public void Copy_Suppliers_PurchaseItems()
        {
            Console.WriteLine("> Copy_Suppliers_PurchaseItems");

            try
            {
                // Process in smaller chunks to avoid timeout
                const int queryBatchSize = 5000;
                const int saveBatchSize = 100;
                var processedCount = 0;

                // Get existing IDs in batches
                var existingItemIds = new HashSet<Guid>();

                foreach (var batch in newOrganization.ErpCOREDBContext.PurchaseItems
                    .Select(x => x.Id)
                    .ToList()
                    .Chunk(queryBatchSize))
                {
                    existingItemIds.UnionWith(batch);
                }

                // Query and process old items in batches
                var query = oldOrganization.ErpNodeDBContext.CommercialItems
                    .AsNoTracking()  // Improve query performance
                    .Where(i => i.TransactionGuid != null
                        && i.TransactionType == ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.Purchase
                        && i.Amount > 0
                        && i.UnitPrice > 0);

                // Process items in chunks
                var itemsToAdd = new List<ERPKeeperCore.Enterprise.Models.Suppliers.PurchaseItem>();

                foreach (var oldModel in query)
                {
                    // Skip if item already exists
                    if (existingItemIds.Contains(oldModel.Uid))
                        continue;

                    Console.WriteLine($"> Processing {oldModel.Uid}: {oldModel.ItemPartNumber}: {oldModel.Amount}:{oldModel.UnitPrice}");

                    var newItem = new ERPKeeperCore.Enterprise.Models.Suppliers.PurchaseItem
                    {
                        Id = oldModel.Uid,
                        PurchaseId = (Guid)oldModel.TransactionGuid,
                        ItemId = oldModel.ItemGuid,
                        Quantity = oldModel.Amount,
                        Price = oldModel.UnitPrice,
                        PartNumber = oldModel.ItemPartNumber,
                        Description = oldModel.ItemDescription,
                        Memo = oldModel.Memo,
                        DiscountPercent = oldModel.DiscountPercent
                    };

                    itemsToAdd.Add(newItem);
                    processedCount++;

                    // Save in smaller batches
                    if (itemsToAdd.Count >= saveBatchSize)
                    {
                        SaveBatch(itemsToAdd);
                        itemsToAdd.Clear();
                    }

                    // Optional: Add a progress indicator
                    if (processedCount % 1000 == 0)
                    {
                        Console.WriteLine($"> Processed {processedCount} items");
                    }
                }

                // Save any remaining items
                if (itemsToAdd.Any())
                {
                    SaveBatch(itemsToAdd);
                }

                Console.WriteLine($"> Successfully processed total of {processedCount} items");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"> Error in Copy_Suppliers_PurchaseItems: {ex.Message}");
                Console.WriteLine($"> Stack Trace: {ex.StackTrace}");
                throw; // Re-throw after logging
            }
        }

        public void SaveBatch(List<ERPKeeperCore.Enterprise.Models.Suppliers.PurchaseItem> items)
        {
            try
            {
                using (var transaction = newOrganization.ErpCOREDBContext.Database.BeginTransaction())
                {
                    newOrganization.ErpCOREDBContext.PurchaseItems.AddRange(items);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"> Error saving batch: {ex.Message}");
                throw; // Re-throw after logging
            }
        }

        public void Copy_Suppliers_PurchaseItems_Old()
        {
            Console.WriteLine("> Copy_Suppliers_PurchaseItems");

            var existModelIds = newOrganization.ErpCOREDBContext.PurchaseItems
                .Select(x => x.Id)
                .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext
                .CommercialItems
                .Where(i => !existModelIds.Contains(i.Uid) && i.TransactionGuid != null)
                .Where(i => i.TransactionType == ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.Purchase)
                .Where(i => i.Amount > 0 && i.UnitPrice > 0)
                .ToList();

            oldModels.ForEach((Action<ERPKeeper.Node.Models.Transactions.CommercialItem>)(oldModel =>
            {
                var existItem = newOrganization.ErpCOREDBContext
                .PurchaseItems
                .Find(oldModel.Uid);

                if (existItem == null)
                {
                    Console.WriteLine($"> Add {oldModel.Uid}: {oldModel.ItemPartNumber}: {oldModel.Amount}:{oldModel.UnitPrice}: ");


                    existItem = new ERPKeeperCore.Enterprise.Models.Suppliers.PurchaseItem()
                    {
                        Id = oldModel.Uid,
                        PurchaseId = (Guid)oldModel.TransactionGuid,
                        ItemId = oldModel.ItemGuid,
                        Quantity = oldModel.Amount,
                        Price = oldModel.UnitPrice,
                        PartNumber = oldModel.ItemPartNumber,
                        Description = oldModel.ItemDescription,
                        Memo = oldModel.Memo,
                        DiscountPercent = oldModel.DiscountPercent,
                    };

                    newOrganization.ErpCOREDBContext.PurchaseItems.Add(existItem);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"> Exists {oldModel.ItemPartNumber}: {oldModel.Amount}:{oldModel.UnitPrice}: ");
                }
            }));


        }
        public void Copy_Supplier_Payments()
        {
            Console.WriteLine("> Copy_Supplier_Payments");

            oldOrganization.ErpNodeDBContext
                .Purchases
                .Where(s => s.GeneralPaymentUid != null)
                .Include(s => s.GeneralPayment)
                .OrderByDescending(s => s.GeneralPayment.TrnDate)
                .ToList()
                .ForEach(oldPurchase =>
                {
                
                    var existSupplierPayment = newOrganization.ErpCOREDBContext
                        .SupplierPayments
                        .FirstOrDefault(s => s.PurchaseId == oldPurchase.Uid);

                    if (existSupplierPayment == null)
                    {
                        Console.WriteLine($"PURCHASE: {oldPurchase.Name}");

                        existSupplierPayment = new Enterprise.Models.Suppliers.SupplierPayment()
                        {
                            PurchaseId = oldPurchase.Uid,
                            Amount = oldPurchase.Total,
                            Date = oldPurchase.GeneralPayment.TrnDate,
                            Reference = oldPurchase.GeneralPayment.Reference,
                            AmountBankFee = oldPurchase.GeneralPayment.BankFeeAmount,
                            AmountDiscount = oldPurchase.GeneralPayment.DiscountAmount,
                            PayFrom_AssetAccountId = oldPurchase.GeneralPayment.AssetAccountUid,
                            LiablityAccount_SupplierPayableId = oldPurchase.GeneralPayment.LiabilityAccountUid,
                            RetentionTypeId = oldPurchase.GeneralPayment.RetentionTypeGuid,
                        };

                        if (existSupplierPayment.RetentionTypeId != null)
                            existSupplierPayment.AmountRetention = oldPurchase.GeneralPayment.RetentionType.Rate * oldPurchase.LinesTotal / 100;


                        if (existSupplierPayment.LiablityAccount_SupplierPayableId == null)
                            existSupplierPayment.LiablityAccount_SupplierPayableId =
                                newOrganization.SystemAccounts.GetAccount(DefaultAccountType.Liability_AccountPayable).Id;


                        //Console.WriteLine($"> {oldPurchase.GeneralPayment.AssetAccount.Name}");
                        //Console.WriteLine($"> {oldPurchase.GeneralPayment.LiabilityAccount?.Name}");


                        newOrganization.ErpCOREDBContext
                            .SupplierPayments.Add(existSupplierPayment);

                        newOrganization.SaveChanges();
                    }
                    else
                    {

                    }
                    


                });
        }
        public void Copy_Suppliers_PurchaseQuotes()
        {
            Console.WriteLine("> Copy_Suppliers_PurchaseQuotes");

            var existModelIds = newOrganization.ErpCOREDBContext.PurchaseQuotes
               .Select(x => x.Id)
               .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.PurchaseEstimates
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();


            oldModels.ForEach(oldModel =>
            {
                var exist = newOrganization.ErpCOREDBContext.PurchaseQuotes.FirstOrDefault(x => x.Id == oldModel.Uid);

                if (exist == null)
                {
                   Console.WriteLine($"> Add PurchaseQuote: {oldModel.Name}");

                    exist = new ERPKeeperCore.Enterprise.Models.Suppliers.PurchaseQuote()
                    {
                        Id = oldModel.Uid,
                        Date = oldModel.TrnDate,
                        No = oldModel.No,
                        Name = oldModel.Name,
                        Memo = oldModel.Memo,
                        Status = (PurchaseQuoteStatus)oldModel.Status,
                        TaxCodeId = oldModel.TaxCodeGuid,
                        SupplierId = (Guid)oldModel.ProfileGuid,
                        SupplierAddressId = oldModel.ProfileAddressGuid,
                        LinesTotal = oldModel.LinesTotal,
                        Tax = oldModel.Tax,
                        Reference = oldModel.Reference,
                    };

                    newOrganization.ErpCOREDBContext.PurchaseQuotes.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {
                    exist.Reference = oldModel.Reference;
                }


            });

            newOrganization.ErpCOREDBContext.SaveChanges();
        }
        public void Copy_Suppliers_PurchaseQuoteItems()
        {

            Console.WriteLine("> Copy_Suppliers_PurchaseQuoteItems");


            var existModelIds = newOrganization.ErpCOREDBContext.PurchaseQuoteItems
                .Select(x => x.Id)
                .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext
                .EstimateItems
                .Where(i => !existModelIds.Contains(i.Uid) && i.QuoteId != null)
                .Where(i => i.Quote.TransactionType == ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.PurchaseQuote)
                .Where(i => i.Amount > 0 && i.UnitPrice > 0)
                .ToList();

            oldModels.ForEach(oldModel =>
            {
                var existItem = newOrganization.ErpCOREDBContext
                .PurchaseQuoteItems
                .Find(oldModel.Uid);

                if (existItem == null)
                {
                    Console.WriteLine($"> Add PurchaseQuoteItems {oldModel.Uid}: {oldModel.ItemPartNumber}: {oldModel.Amount}:{oldModel.UnitPrice}: ");

                    try
                    {
                        existItem = new ERPKeeperCore.Enterprise.Models.Suppliers.PurchaseQuoteItem()
                        {
                            Id = oldModel.Uid,
                            SupplierQuoteId = (Guid)oldModel.QuoteId,
                            ItemId = (Guid)oldModel.ItemGuid,
                            Quantity = (int)oldModel.Amount,
                            Price = oldModel.UnitPrice,
                            PartNumber = oldModel.ItemPartNumber,
                            Description = oldModel.ItemDescription,
                            Memo = oldModel.Memo,
                            DiscountPercent = oldModel.DiscountPercent,
                        };

                        newOrganization.ErpCOREDBContext.PurchaseQuoteItems.Add(existItem);
                        newOrganization.ErpCOREDBContext.SaveChanges();
                    }
                    catch (Exception)
                    {

                    }

                }
                else
                {
                    Console.WriteLine($"> Exists {oldModel.ItemPartNumber}: {oldModel.Amount}:{oldModel.UnitPrice}: ");
                }
            });


        }
    }
}
