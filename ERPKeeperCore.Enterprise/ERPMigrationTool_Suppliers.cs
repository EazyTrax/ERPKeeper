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

        private  void CopySupplierPayments()
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
                            PayFromAccountId = oldPurchase.GeneralPayment.AssetAccountUid,
                            PayableAccountId = oldPurchase.GeneralPayment.LiabilityAccountUid,
                            RetentionTypeId = oldPurchase.GeneralPayment.RetentionTypeGuid,

                        };

                        if (existSupplierPayment.RetentionTypeId != null)
                            existSupplierPayment.AmountRetention = oldPurchase.GeneralPayment.RetentionType.Rate * oldPurchase.LinesTotal / 100;


                        if (existSupplierPayment.PayableAccountId == null)
                            existSupplierPayment.PayableAccountId =
                                newOrganization.SystemAccounts.GetAccount(DefaultAccountType.AccountPayable).Id;


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
