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
                Console.WriteLine("###########################################################");


                newOrganization.ErpCOREDBContext.Transactions.ToList()
                    .ForEach(m =>
                    {
                        newOrganization.ErpCOREDBContext.Remove(m);
                        newOrganization.ErpCOREDBContext.SaveChanges();
                    });

                Console.WriteLine(newOrganization.ErpCOREDBContext.Transactions.Count());

                newOrganization.ErpCOREDBContext.Sales
                    .Where(m => m.TransactionId == null)
                    .ToList()
                    .ForEach(model =>
                    {
                        Console.WriteLine($"SALE: {model.No}-{model.Name}");

                        var tr = newOrganization.Transactions.CreateTransaction(model.Id, TransactionType.Sale);
                        model.TransactionId = tr.Id;

                        tr.Date = model.Date;
                        tr.Reference = model.Name;
                        newOrganization.SaveChanges();
                    });

                newOrganization.ErpCOREDBContext.Purchases
                  .Where(m => m.TransactionId == null)
                  .ToList()
                  .ForEach(model =>
                  {
                      Console.WriteLine($"SALE: {model.No}-{model.Name}");

                      var tr = newOrganization.Transactions.CreateTransaction(model.Id, TransactionType.Sale);
                      model.TransactionId = tr.Id;

                      tr.Date = model.Date;
                      tr.Reference = model.Name;

                      newOrganization.SaveChanges();
                  });



                //newOrganization.ErpCOREDBContext.Purchases.ToList()
                //  .ForEach(m => m.CreateTransaction());


                //newOrganization.ErpCOREDBContext.FundTransfers.ToList()
                // .ForEach(m => m.CreateTransaction());
                //newOrganization.SaveChanges();




                //newOrganization.SaveChanges();

                //CopyAccounts(newOrganization, oldOrganization);
                //CopyProfiles(newOrganization, oldOrganization);
                //CopyBrands(newOrganization, oldOrganization);

                //CopyItemGroups(newOrganization, oldOrganization);
                //CopyItems(newOrganization, oldOrganization);

                //CopyProjects(newOrganization, oldOrganization);
                //CopyCustomers(newOrganization, oldOrganization);
                //CopySuppliers(newOrganization, oldOrganization);
                //CopyFiscalYear(newOrganization, oldOrganization);
                //CopyTaxCode(newOrganization, oldOrganization);
                //CopyTaxPeriod(newOrganization, oldOrganization);
                // CopySales(newOrganization, oldOrganization);
                //CopySaleItems(newOrganization, oldOrganization);
                //CopyPurchases(newOrganization, oldOrganization);
                //CopyPurchaseItems(newOrganization, oldOrganization);
                // CreateTransactionForSales(newOrganization);
                // CreateTransactionForPurchases(newOrganization);
                //CopyFundTransfers(newOrganization, oldOrganization);

                //CopyAssetTypes(newOrganization, oldOrganization);
                // CopyAssets(newOrganization, oldOrganization);
                //CopyAssetDeprecreates(newOrganization, oldOrganization);


                // CopyJournalEntryTypes(newOrganization, oldOrganization);
                // CopyJournalEntries(newOrganization, oldOrganization);
                // CopyJournalEntryItems(newOrganization, oldOrganization);

                //
                //        newOrganization.ErpCOREDBContext.SaveChanges();
                Console.WriteLine($">{newOrganization.ErpCOREDBContext.Database.GetConnectionString()}");
                Console.WriteLine("###########################################################" + Environment.NewLine + Environment.NewLine);
            }

            Console.WriteLine("###########################################################");

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

        private static void CopyAccounts(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldAccounts = oldOrganization.ErpNodeDBContext.AccountItems.ToList();
            oldAccounts.ForEach(a =>
            {
                Console.WriteLine($"ACC: {a.No}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.Accounts.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Accounting.Account()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Description = a.Description,
                        No = a.No,
                        Type = (AccountTypes)a.Type,
                        SubType = (AccountSubTypes?)a.SubEnumType,
                        IsCashEquivalent = a.IsCashEquivalent,
                        IsLiquidity = a.IsLiquidity,
                    };
                    newOrganization.ErpCOREDBContext.Accounts.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private static void CopyProfiles(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.Profiles.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Uid}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.Profiles.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Profiles.Profile()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Detail = a.Detail,
                        TaxNumber = a.TaxNumber,
                        Note = a.Note,

                    };
                    newOrganization.ErpCOREDBContext.Profiles.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private static void CopyBrands(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.Brands.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Uid}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.Brands.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Items.Brand()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Description = a.Description,
                        WebSite = a.WebSite,
                        ShotName = a.ShotName,
                    };
                    newOrganization.ErpCOREDBContext.Brands.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private static void CopyItemGroups(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.ItemGroups.ToList();

            oldModels.ForEach(a =>
            {

                var exist = newOrganization.ErpCOREDBContext.ItemGroups.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"ITM:{a.Uid}-{a.PartNumber}");
                    exist = new ERPKeeperCore.Enterprise.Models.Items.ItemGroup()
                    {
                        Id = a.Uid,
                        Name = a.PartNumber,
                    };
                    newOrganization.ErpCOREDBContext.ItemGroups.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {

                }


            });
        }

        private static void CopyItems(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.Items/*.Where(i => i.ParentUid != null)*/.ToList();

            oldModels.ForEach(a =>
            {
                var exist = newOrganization.ErpCOREDBContext.Items.FirstOrDefault(x => x.Id == a.Uid);
                var brand = newOrganization.ErpCOREDBContext.Brands.FirstOrDefault(x => x.Id == a.BrandGuid);

                if (exist == null)
                {
                    Console.WriteLine($"ITM:{a.Uid}-{a.PartNumber}");
                    exist = new ERPKeeperCore.Enterprise.Models.Items.Item()
                    {
                        Id = a.Uid,
                        PartNumber = a.PartNumber,
                        BrandId = brand?.Id,
                        Description = a.Description,
                        PurchasePrice = a.PurchasePrice,
                        SalePrice = a.UnitPrice,
                        UPC = a.UPC,
                        Status = (ItemStatus)a.Status,
                        ItemType = (ItemTypes)a.ItemType,
                        OnlineSale = a.OnlineSale,
                        Specification = a.Specification,
                        IncomeAccountId = a.IncomeAccountUid,
                        PurchaseAccountId = a.PurchaseAccountUid,
                        CreatedDate = (DateTime)(a.CreatedDate ?? DateTime.Today),
                    };
                    newOrganization.ErpCOREDBContext.Items.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {
                    try
                    {
                        exist.ItemGroupId = a.ParentUid;
                        newOrganization.ErpCOREDBContext.SaveChanges();
                    }
                    catch (Exception) { }
                }


            });
        }
        private static void CopyProjects(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.Projects.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Uid}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.Projects.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Projects.Project()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Detail = a.Detail,
                        StartDate = a.CreatedDate,
                        EndDate = a.CreatedDate.AddYears(1),
                    };
                    newOrganization.ErpCOREDBContext.Projects.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private static void CopyCustomers(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.Customers.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.ProfileUid}-{a.Profile.Name}");

                var exist = newOrganization.ErpCOREDBContext.Customers.FirstOrDefault(x => x.Id == a.ProfileUid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Customers.Customer()
                    {
                        Id = a.ProfileUid,
                        Status = (CustomerStatus)a.Status,
                    };
                    newOrganization.ErpCOREDBContext.Customers.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private static void CopySuppliers(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.Suppliers.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.ProfileUid}-{a.Profile.Name}");

                var exist = newOrganization.ErpCOREDBContext.Suppliers.FirstOrDefault(x => x.Id == a.ProfileUid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Suppliers.Supplier()
                    {
                        Id = a.ProfileUid,
                        Status = (Enterprise.Models.Suppliers.Enums.SupplierStatus)a.Status,
                    };

                    newOrganization.ErpCOREDBContext.Suppliers.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private static void CopyFiscalYear(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.FiscalYears.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Name}-{a.StartDate}");

                var exist = newOrganization.ErpCOREDBContext.FiscalYears.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Accounting.FiscalYear()
                    {
                        Id = a.Uid,
                        StartDate = a.StartDate,
                        EndDate = a.EndDate,
                        Memo = a.Memo,
                        Status = (FiscalYearStatus)a.Status,
                    };

                    newOrganization.ErpCOREDBContext.FiscalYears.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private static void CopyTaxCode(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.TaxCodes.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Name}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.TaxCodes.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Taxes.TaxCode()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Description = a.Description,
                        Rate = a.TaxRate,
                        TaxDirection = (Enterprise.Models.Taxes.Enums.TaxDirection)a.TaxDirection,
                        AssignAccountId = a.AssignAccountGuid,
                        CloseToAccountId = a.CloseToAccountGuid,
                        InputTaxAccountId = a.InputTaxAccountGuid,
                        OutputTaxAccountId = a.OutputTaxAccountGuid,
                        IsDefault = a.isDefault,
                        TaxAccountId = a.TaxAccountGuid,
                        TaxReceivableAccountId = a.TaxReceivableAccountGuid,
                        TaxPayableAccountId = a.TaxPayableAccountGuid,
                    };

                    newOrganization.ErpCOREDBContext.TaxCodes.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private static void CopyTaxPeriod(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.TaxPeriods.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Name}-{a.PeriodStartDate}");

                var exist = newOrganization.ErpCOREDBContext.TaxPeriods.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Taxes.TaxPeriod()
                    {
                        Id = a.Uid,
                        StartDate = a.PeriodStartDate,
                        CloseToAccountId = a.CloseToAccountGuid,
                        FiscalYearId = a.FiscalYearUid,
                        LiabilityPaymentId = a.LiabilityPaymentUid,
                        Memo = a.Memo,
                        No = a.No,
                    };

                    newOrganization.ErpCOREDBContext.TaxPeriods.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private static void CopySales(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.Sales.ToList();

            oldModels.ForEach(oldModel =>
            {
                Console.WriteLine($"PROF:{oldModel.Name}-{oldModel.Uid}");

                var exist = newOrganization.ErpCOREDBContext.Sales.FirstOrDefault(x => x.Id == oldModel.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Customers.Sale()
                    {
                        Id = oldModel.Uid,
                        Date = oldModel.TrnDate,
                        No = oldModel.No,
                        Name = oldModel.Name,
                        Memo = oldModel.Memo,
                        Status = SaleStatus.Invoice,
                        TaxCodeId = oldModel.TaxCodeId,
                        CustomerId = oldModel.ProfileGuid,
                        CustomerAddressId = oldModel.ProfileAddressGuid,
                        TaxPeriodId = oldModel.TaxPeriodId,
                        LinesTotal = oldModel.LinesTotal,
                        Tax = oldModel.Tax,
                    };

                    newOrganization.ErpCOREDBContext.Sales.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {

                }


            });
        }
        private static void CopySaleItems(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var existModelIds = newOrganization.ErpCOREDBContext.SaleItems
                .Select(x => x.Id)
                .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext
                .CommercialItems
                .Where(i => !existModelIds.Contains(i.Uid))
                .Where(i => i.TransactionType == ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.Sale)
                .ToList();

            oldModels.ForEach(oldModel =>
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
                        SaleId = oldModel.TransactionGuid,
                        ItemId = oldModel.ItemGuid,
                        Quantity = oldModel.Amount,
                        Price = oldModel.UnitPrice,
                        PartNumber = oldModel.ItemPartNumber,
                        Description = oldModel.ItemDescription,
                        Memo = oldModel.Memo,
                    };

                    newOrganization.ErpCOREDBContext.SaleItems.Add(existItem);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"> Exists {oldModel.ItemPartNumber}: {oldModel.Amount}:{oldModel.UnitPrice}: ");
                }
            });


        }
        private static void CopyPurchases(EnterpriseRepo newOrganization, Organization oldOrganization)
        {

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


                    if (supplier == null)
                    {
                        var profile = newOrganization.ErpCOREDBContext
                         .Profiles
                         .Find(oldModel.ProfileGuid);

                        if (profile != null)
                        {
                            Console.WriteLine($"> profile {profile.Name}");

                            supplier = new ERPKeeperCore.Enterprise.Models.Suppliers.Supplier()
                            {
                                Id = profile.Id,
                                Status = Enterprise.Models.Suppliers.Enums.SupplierStatus.Active,
                            };
                            newOrganization.ErpCOREDBContext.Suppliers.Add(supplier);
                            newOrganization.ErpCOREDBContext.SaveChanges();

                        }
                    }

                    exist = new ERPKeeperCore.Enterprise.Models.Suppliers.Purchase()
                    {
                        Id = oldModel.Uid,
                        Date = oldModel.TrnDate,
                        No = oldModel.No,
                        Name = oldModel.Name,
                        Memo = oldModel.Memo,
                        Status = Enterprise.Models.Suppliers.Enums.PurchaseStatus.Invoice,
                        TaxCodeId = oldModel.TaxCodeId,
                        SupplierId = oldModel.ProfileGuid,
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

                }


            });
        }
        private static void CopyPurchaseItems(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var existModelIds = newOrganization.ErpCOREDBContext.PurchaseItems
                .Select(x => x.Id)
                .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext
                .CommercialItems
                .Where(i => !existModelIds.Contains(i.Uid))
                .Where(i => i.TransactionType == ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.Purchase)
                .ToList();

            oldModels.ForEach(oldModel =>
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
                        PurchaseId = oldModel.TransactionGuid,
                        ItemId = oldModel.ItemGuid,
                        Quantity = oldModel.Amount,
                        Price = oldModel.UnitPrice,
                        PartNumber = oldModel.ItemPartNumber,
                        Description = oldModel.ItemDescription,
                        Memo = oldModel.Memo,
                    };

                    newOrganization.ErpCOREDBContext.PurchaseItems.Add(existItem);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"> Exists {oldModel.ItemPartNumber}: {oldModel.Amount}:{oldModel.UnitPrice}: ");
                }
            });


        }

        private static void CopyFundTransfers(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.FundTransfers.ToList();

            oldModels.ForEach(a =>
            {


                var exist = newOrganization.ErpCOREDBContext
                .FundTransfers
                .Find(a.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"FT:{a.Name}-{a.TrnDate}");
                    exist = new ERPKeeperCore.Enterprise.Models.Financial.FundTransfer()
                    {
                        Id = a.Uid,
                        Date = a.TrnDate,
                        Reference = a.Reference,
                        Status = (ERPKeeperCore.Enterprise.Models.Financial.Enums.FundTransferStatus)a.Status,
                    };


                    if (a.WithDrawAccountGuid != null)
                        exist.AddCredit((Guid)a.WithDrawAccountGuid, a.AmountwithDraw);

                    if (a.BankFeeAccountGuid != null)
                        exist.AddDebit((Guid)a.BankFeeAccountGuid, a.AmountFee);

                    if (a.DepositAccountGuid != null)
                        exist.AddDebit((Guid)a.DepositAccountGuid, a.AmountDeposit);

                    exist.Refresh();

                    newOrganization.ErpCOREDBContext.FundTransfers.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();

                    Console.WriteLine($">{exist.Credit}-{exist.Debit}");


                }
                else
                {

                }

                // newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private static void CopyAssetTypes(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.AssetTypes.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Name}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.AssetTypes.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Assets.AssetType()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Description = a.Description,
                        AccumulateDeprecateAccId = a.AccumulateDeprecateAccUid,
                        AmortizeExpenseAccId = a.AmortizeExpenseAccUid,
                        AwaitDeprecateAccId = a.AwaitDeprecateAccUid,
                        PurchaseAccId = a.PurchaseAccUid,
                        CodePrefix = a.CodePrefix,
                        DeprecatedAble = a.DeprecatedAble,
                        DepreciationMethod = (Enterprise.Models.Assets.Enums.EnumDepreciationMethod)a.DepreciationMethod,
                        ScrapValue = a.ScrapValue,
                        UseFulLifeYear = a.UseFulLifeYear,
                    };

                    newOrganization.ErpCOREDBContext.AssetTypes.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private static void CopyAssets(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.Assets.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Name}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.Assets.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Assets.Asset()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        AssetTypeId = a.FixedAssetTypeUid,
                        AssetValue = a.AssetValue,
                        Code = a.Code,
                        DepreciationPerYear = a.DepreciationPerYear,
                        LastCreateSchedule = a.LastCreateSchedule,
                        Memo = a.Memo,
                        No = a.No,
                        PurchaseDate = a.StartDeprecationDate,
                        SavageValue = a.SavageValue,
                        Reference = a.Reference,
                        StartDeprecationDate = a.StartDeprecationDate,
                        Status = (Enterprise.Models.Assets.Enums.AssetStatus)a.Status,
                    };

                    newOrganization.ErpCOREDBContext.Assets.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private static void CopyAssetDeprecreates(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.DeprecateSchedules.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Name}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.AssetDeprecateSchedules.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Assets.AssetDeprecateSchedule()
                    {
                        Id = a.Uid,
                        AssetId = a.FixedAssetUid,
                        DepreciateAccumulation = a.DepreciateAccumulation,
                        DepreciateOffsetValue = a.DepreciateOffsetValue,
                        DepreciationValue = a.DepreciationValue,
                        EndDate = a.EndDate,
                        No = a.No,
                        OpeningValue = a.OpeningValue,
                        PostStatus = (Enterprise.Models.Accounting.Enums.LedgerPostStatus)a.PostStatus,
                        RemainValue = a.RemainValue,
                        StartDate = a.StartDate,
                    };

                    newOrganization.ErpCOREDBContext.AssetDeprecateSchedules.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }


        private static void CopyJournalEntryTypes(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.JournalEntryTypes.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Name}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.JournalEntryTypes.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Accounting.JournalEntryType()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Detail = a.Detail,
                        IsActive = a.IsActive,
                    };

                    newOrganization.ErpCOREDBContext.JournalEntryTypes.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }

        private static void CopyJournalEntries(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.JournalEntries.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Name}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.JournalEntries.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Accounting.JournalEntry()
                    {
                        Id = a.Uid,
                        Date = a.TrnDate,
                        No = a.No,
                        Credit = a.Credit,
                        Debit = a.Debit,
                        Memo = a.Memo,
                        JournalEntryTypeId = a.JournalEntryTypeGuid,
                    };

                    newOrganization.ErpCOREDBContext.JournalEntries.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }

        private static void CopyJournalEntryItems(EnterpriseRepo newOrganization, Organization oldOrganization)
        {
            var oldModels = oldOrganization.ErpNodeDBContext.JournalEntryItems.ToList();

            oldModels.ForEach(a =>
            {
                var exist = newOrganization.ErpCOREDBContext.JournalEntryItems.FirstOrDefault(x => x.Id == a.JournalEntryItemId);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Accounting.JournalEntryItem()
                    {
                        Id = a.JournalEntryItemId,
                        Credit = a.Credit,
                        Debit = a.Debit,
                        Memo = a.Memo,
                        JournalEntryId = a.JournalEntryId,
                        AccountId = a.AccountUid,
                    };

                    newOrganization.ErpCOREDBContext.JournalEntryItems.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }

    }
}
