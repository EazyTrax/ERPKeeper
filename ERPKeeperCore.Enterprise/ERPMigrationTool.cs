using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPKeeper.Node.DAL;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Items.Enums;
using Microsoft.EntityFrameworkCore;
namespace ERPKeeperCore.CMD
{
    public class ERPMigrationTool
    {
        private string enterpriseDB;
        public EnterpriseRepo newOrganization;
        public Organization oldOrganization;

        public ERPMigrationTool(string enterpriseDB)
        {
            this.enterpriseDB = enterpriseDB;

            Console.WriteLine($"###{enterpriseDB}########################################################");
            newOrganization = new ERPKeeperCore.Enterprise.EnterpriseRepo(enterpriseDB, true);
            newOrganization.ErpCOREDBContext.Database.Migrate();
            Console.WriteLine(newOrganization.ErpCOREDBContext.Transactions.Count());
            oldOrganization = new ERPKeeper.Node.DAL.Organization(enterpriseDB, true);
            Console.WriteLine("###########################################################");
        }


        public void Migrate()
        {
            //newOrganization.FiscalYears.PrepareFiscalYearBalances();
            //newOrganization.FiscalYears.UpdateTransactionFiscalYears();

            //CopyProfiles(newOrganization, oldOrganization);
            //CopySuppliers(newOrganization, oldOrganization);
            //CopySales(newOrganization, oldOrganization);
            //CopyPurchases(newOrganization, oldOrganization);
            //CopyPurchaseItems(newOrganization, oldOrganization);
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
            //CopySales(newOrganization, oldOrganization);
            //CopySaleItems(newOrganization, oldOrganization);
            //CopyPurchases(newOrganization, oldOrganization);
            //CopyPurchaseItems(newOrganization, oldOrganization);
            //CreateTransactionForSales(newOrganization);
            //CreateTransactionForPurchases(newOrganization);
            //CopyFundTransfers(newOrganization, oldOrganization);
            //CopyAssetTypes(newOrganization, oldOrganization);
            //CopyAssets(newOrganization, oldOrganization);
            //CopyAssetDeprecreates(newOrganization, oldOrganization);
            //CopyJournalEntryTypes(newOrganization, oldOrganization);
            //CopyJournalEntries(newOrganization, oldOrganization);
            //CopyJournalEntryItems(newOrganization, oldOrganization);
            //CopyEmployeePositions(newOrganization, oldOrganization);
            //CopyEmployees(newOrganization, oldOrganization);
            //CopyAccounts(newOrganization, oldOrganization);
            //CopyDefaultAccounts(newOrganization, oldOrganization);
            //CopyRetentionTypes(newOrganization, oldOrganization);


        }

        private void CopyAccounts()
        {
            var oldAccounts = oldOrganization.ErpNodeDBContext.AccountItems.ToList();
            oldAccounts.ForEach(oldAccount =>
            {
                Console.WriteLine($"ACC: {oldAccount.No}-{oldAccount.Name}");

                var exist = newOrganization.ErpCOREDBContext.Accounts.FirstOrDefault(x => x.Id == oldAccount.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Accounting.Account()
                    {
                        Id = oldAccount.Uid,
                        Name = oldAccount.Name,
                        Description = oldAccount.Description,
                        No = oldAccount.No,
                        Type = (AccountTypes)oldAccount.Type,
                        SubType = (AccountSubTypes?)oldAccount.SubEnumType,
                        IsCashEquivalent = oldAccount.IsCashEquivalent,
                        IsLiquidity = oldAccount.IsLiquidity,
                    };
                    newOrganization.ErpCOREDBContext.Accounts.Add(exist);
                }
                else
                {
                    exist.OpeningCredit = oldAccount.OpeningCreditBalance;
                    exist.OpeningDebit = oldAccount.OpeningDebitBalance;
                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void CopyDefaultAccounts()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.SystemAccounts.ToList();

            oldModels.ForEach(oldSystemAccount =>
            {
                Console.WriteLine($"PROF:{oldSystemAccount.AccountType}-{oldSystemAccount.AccountItem.Name}");

                var exist = newOrganization.ErpCOREDBContext.DefaultAccounts
                .FirstOrDefault(x => x.Type == (DefaultAccountType)oldSystemAccount.AccountType);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Accounting.DefaultAccount()
                    {
                        Type = (DefaultAccountType)oldSystemAccount.AccountType,
                        AccountId = oldSystemAccount.AccountItemUid,
                        LastUpdate = oldSystemAccount.LastUpdate,
                    };

                    newOrganization.ErpCOREDBContext.DefaultAccounts.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {

                }

            });
        }

        private void CopyProfiles()
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
        private void CopyBrands()
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
        private void CopyItemGroups()
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
        private void CopyItems()
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
        private void CopyProjects()
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
        private void CopyCustomers()
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
        private void CopySuppliers()
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
        private void CopyEmployeePositions()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.EmployeePositions.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"EmployeePositions:{a.Uid}-{a.Title}");

                var exist = newOrganization.ErpCOREDBContext.EmployeePositions.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Employees.EmployeePosition()
                    {
                        Id = a.Uid,
                        Description = a.Description ?? "NA",
                        Title = a.Title,
                    };
                    newOrganization.ErpCOREDBContext.EmployeePositions.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void CopyEmployees()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.Employees.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"Employees:{a.ProfileUid}-{a.Profile.Name}");

                var exist = newOrganization.ErpCOREDBContext.Employees.FirstOrDefault(x => x.Id == a.ProfileUid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Employees.Employee()
                    {
                        Id = a.ProfileUid,
                        EmployeePositionId = a.PositionGuid,
                        Status = (Enterprise.Models.Employees.Enums.EmployeeStatus)a.Status,
                    };
                    newOrganization.ErpCOREDBContext.Employees.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void CopyFiscalYear()
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
        private void CopyTaxCode()
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
        private void CopyTaxPeriod()
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
        private void CopySales()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.Sales.ToList();

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
                    exist.Reference = oldModel.Reference;
                }


            });

            newOrganization.ErpCOREDBContext.SaveChanges();
        }
        private void CopyRetentionTypes()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.RetentionTypes.ToList();

            oldModels.ForEach(oldModel =>
            {
                Console.WriteLine($"RetentionTypes:{oldModel.Name}-{oldModel.Uid}");
                var exist = newOrganization.ErpCOREDBContext
                .RetentionTypes
                .FirstOrDefault(x => x.Id == oldModel.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"> Add");

                    exist = new ERPKeeperCore.Enterprise.Models.Financial.RetentionType()
                    {
                        Id = oldModel.Uid,
                        IsActive = oldModel.IsActive,
                        Description = oldModel.Description ?? "NA",
                        Name = oldModel.Name,
                        Direction = (Enterprise.Models.Financial.Enums.RetentionDirection)oldModel.RetentionDirection,
                        Rate = oldModel.Rate,
                        RetentionToAccountId = oldModel.RetentionToAccountGuid,
                    };

                    newOrganization.ErpCOREDBContext.RetentionTypes.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {

                }
            });

            newOrganization.ErpCOREDBContext.SaveChanges();
        }
        private void CopySaleItems()
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
        private void CopyPurchases()
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
                    exist.Reference = oldModel.Reference;
                }


            });
            newOrganization.ErpCOREDBContext.SaveChanges();
        }
        private void CopyPurchaseItems()
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
        private void CopyFundTransfers()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.FundTransfers.ToList();

            oldModels.ForEach(oldFundTransfer =>
            {
                var exist = newOrganization.ErpCOREDBContext
                .FundTransfers
                .Find(oldFundTransfer.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"FT:{oldFundTransfer.Name}-{oldFundTransfer.TrnDate}");
                    exist = new ERPKeeperCore.Enterprise.Models.Financial.FundTransfer()
                    {
                        Id = oldFundTransfer.Uid,
                        Date = oldFundTransfer.TrnDate,
                        Reference = oldFundTransfer.Reference,
                        Status = (ERPKeeperCore.Enterprise.Models.Financial.Enums.FundTransferStatus)oldFundTransfer.Status,
                        WithDrawAccountId = (Guid)oldFundTransfer.WithDrawAccountGuid
                    };

                    if (oldFundTransfer.BankFeeAccountGuid != null)
                        exist.AddDepositLine((Guid)oldFundTransfer.BankFeeAccountGuid, oldFundTransfer.AmountFee);

                    if (oldFundTransfer.DepositAccountGuid != null)
                        exist.AddDepositLine((Guid)oldFundTransfer.DepositAccountGuid, oldFundTransfer.AmountDeposit);

                    exist.UpdateBalance();

                    newOrganization.ErpCOREDBContext.FundTransfers.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();

                    Console.WriteLine($">{exist.WithDrawAmount}");


                }
                else
                {
                    Console.WriteLine($"> Update > FT:{oldFundTransfer.Name}-{oldFundTransfer.TrnDate}");

                    exist.WithDrawAccountId = oldFundTransfer.WithDrawAccountGuid;
                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void CopyAssetTypes()
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
        private void CopyAssets()
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
        private void CopyAssetDeprecreates()
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
        private void CopyJournalEntryTypes()
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
        private void CopyJournalEntries()
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
        private void CopyJournalEntryItems()
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
