using ERPKeeper.Node.DAL;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPKeeperCore.CMD
{
    public partial class ERPMigrationTool
    {
        private void Copy_Suppliers_Suppliers()
        {

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
        private void Copy_Suppliers_Purchases()
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
        private void Copyy_Suppliers_PurchaseItems()
        {
            var existModelIds = newOrganization.ErpCOREDBContext.PurchaseItems
                .Select(x => x.Id)
                .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext
                .CommercialItems
                .Where(i => !existModelIds.Contains(i.Uid))
                .Where(i => i.TransactionType == ERPKeeper.Node.Models.Accounting.Enums.ERPObjectType.Purchase)
                .Where(i => i.Amount > 0 && i.UnitPrice > 0)
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
                        DiscountPercent = oldModel.DiscountPercent,
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
        private void CopySupplierPayments()
        {
            oldOrganization.ErpNodeDBContext
                .Purchases
                .Where(s => s.GeneralPaymentUid != null)
                .OrderByDescending(s => s.GeneralPayment.TrnDate)
                .ToList()
                .ForEach(oldPurchase =>
                {
                    Console.WriteLine($"PURCHASE: {oldPurchase.Name}");

                    var existSupplierPayment = newOrganization.ErpCOREDBContext
                        .SupplierPayments
                        .FirstOrDefault(s => s.PurchaseId == oldPurchase.Uid);

                    if (existSupplierPayment == null)
                    {
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


                        Console.WriteLine($"> {oldPurchase.GeneralPayment.AssetAccount.Name}");
                        Console.WriteLine($"> {oldPurchase.GeneralPayment.LiabilityAccount?.Name}");


                        newOrganization.ErpCOREDBContext
                            .SupplierPayments.Add(existSupplierPayment);

                        newOrganization.SaveChanges();
                    }
                    else
                    {

                    }



                });
        }

    }
}
