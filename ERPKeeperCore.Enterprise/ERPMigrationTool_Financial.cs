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
        private void Copy_Financial_LiabilityPayments()
        {
            oldOrganization.ErpNodeDBContext
                .LiabilityPayments
                .OrderByDescending(s => s.TrnDate)
                .ToList()
                .ForEach(oldPurchase =>
                {
                    Console.WriteLine($"PURCHASE: {oldPurchase.Name}");

                    var existLiabilityPayment = newOrganization.ErpCOREDBContext
                        .LiabilityPayments
                        .FirstOrDefault(s => s.Id == oldPurchase.Uid);

                    if (existLiabilityPayment == null)
                    {
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine($"> Liability Payment: {oldPurchase.Name} ");
                        Console.WriteLine($"> Amount   {oldPurchase.Amount} ");
                        Console.WriteLine($"> Disount  {oldPurchase.DiscountAmount} ");
                        Console.WriteLine($"> BankFee  {oldPurchase.BankFeeAmount} ");
                        Console.WriteLine($"> Lib Acc  {oldPurchase.LiabilityAccount.Name} ");

                        if (oldPurchase.OptionalAssetAccount != null)
                            Console.WriteLine($"> Pay From {oldPurchase.AmountPayFromOptionalAcc} {oldPurchase.OptionalAssetAccount.Name} ");

                        Console.WriteLine($"> Pay From {oldPurchase.AmountPayFromPrimaryAcc} {oldPurchase.AssetAccount.Name} ");


                        existLiabilityPayment = new Enterprise.Models.Financial.LiabilityPayment()
                        {
                            Id = oldPurchase.Uid,
                            Date = oldPurchase.TrnDate,
                            Reference = oldPurchase.Reference ?? "NA",
                            Memo = oldPurchase.Memo ?? "NA",
                            Name = oldPurchase.Name,
                            AmountDiscount = oldPurchase.DiscountAmount,
                            AmountBankFee = oldPurchase.BankFeeAmount,
                            LiabilityAccountId = oldPurchase.LiabilityAccount.Uid,
                        };
                        newOrganization.ErpCOREDBContext.LiabilityPayments.Add(existLiabilityPayment);
                        newOrganization.SaveChanges();

                        if (oldPurchase.OptionalAssetAccount != null)
                        {
                            var payFromAccount = new Enterprise.Models.Financial.LiabilityPaymentPayFromAccount()
                            {
                                AccountId = oldPurchase.OptionalAssetAccount.Uid,
                                Amount = oldPurchase.AmountPayFromOptionalAcc,
                            };
                            existLiabilityPayment.LiabilityPaymentPayFromAccounts.Add(payFromAccount);
                        }

                        if (oldPurchase.AssetAccount != null)
                        {
                            var payFromAccount = new Enterprise.Models.Financial.LiabilityPaymentPayFromAccount()
                            {
                                AccountId = oldPurchase.AssetAccount.Uid,
                                Amount = oldPurchase.AmountPayFromPrimaryAcc,
                            };
                            existLiabilityPayment.LiabilityPaymentPayFromAccounts.Add(payFromAccount);
                        }

                        newOrganization.SaveChanges();
                    }
                    else
                    {
                        existLiabilityPayment.Amount = oldPurchase.Amount;
                        newOrganization.SaveChanges();
                    }


                });
        }

    }
}
