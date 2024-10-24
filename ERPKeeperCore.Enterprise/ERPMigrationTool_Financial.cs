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
        public void Copy_Financial()
        {
            Console.WriteLine("> Copy_Financial");

            Copy_Financial_FundTransfers();
            Copy_Financial_LiabilityPayments();
            Copy_Financial_Loans();
            Copy_Financial_Lends();
            Copy_Financial_Retentions_Types();
            Copy_Financial_Retentions_Groups();
        }

        private void Copy_Financial_FundTransfers()
        {
            Console.WriteLine("> Copy_Financial_FundTransfers");
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
                        WithDrawAccountId = (Guid)oldFundTransfer.WithDrawAccountGuid,
                        BankFeeAmount = oldFundTransfer.AmountFee,
                        WithDrawAmount = oldFundTransfer.AmountwithDraw,

                    };


                    newOrganization.ErpCOREDBContext.FundTransfers.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();

                    Console.WriteLine($">{exist.WithDrawAmount}");


                }
                else
                {
                   // Console.WriteLine($"> Update > FT:{oldFundTransfer.Name}-{oldFundTransfer.TrnDate}");

                 //   exist.WithDrawAccountId = oldFundTransfer.WithDrawAccountGuid;
                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void Copy_Financial_LiabilityPayments()
        {
            Console.WriteLine("> Copy_Financial_LiabilityPayments");

            var existModelIds = newOrganization.ErpCOREDBContext.LiabilityPayments
                .Select(x => x.Id)
                .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.LiabilityPayments
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();

            oldModels.ForEach(oldPurchase =>
                {

                    var existLiabilityPayment = newOrganization.ErpCOREDBContext
                        .LiabilityPayments
                        .FirstOrDefault(s => s.Id == oldPurchase.Uid);

                    if (existLiabilityPayment == null)
                    {

                        Console.WriteLine($"PURCHASE: {oldPurchase.Name}");

                        //ฝฝ   Console.WriteLine("----------------------------------------------");
                        Console.WriteLine($"> Liability Payment: {oldPurchase.Name} ");
                        //Console.WriteLine($"> Amount   {oldPurchase.Amount} ");
                        //Console.WriteLine($"> Disount  {oldPurchase.DiscountAmount} ");
                        //Console.WriteLine($"> BankFee  {oldPurchase.BankFeeAmount} ");
                        //Console.WriteLine($"> Lib Acc  {oldPurchase.LiabilityAccount.Name} ");

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

                    }


                });
        }
        private void Copy_Financial_Loans()
        {
            Console.WriteLine("> Copy_Financial_Loans");

            oldOrganization.ErpNodeDBContext
                .Loans
                .OrderByDescending(s => s.TrnDate)
                .ToList()
                .ForEach(oldPurchase =>
                {
                    var existLoan = newOrganization.ErpCOREDBContext
                        .Loans
                        .FirstOrDefault(s => s.Id == oldPurchase.Uid);

                    if (existLoan == null)
                    {
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine($"> Loans: {oldPurchase.Name} ");

                        existLoan = new Enterprise.Models.Financial.Loan()
                        {
                            Id = oldPurchase.Uid,
                            Date = oldPurchase.TrnDate,
                            Memo = "NA",
                            AmountLoan = oldPurchase.Amount,
                            RecevingAccountId = oldPurchase.AssetAccountGuid,
                            LoanAccountId = oldPurchase.LiabilityAccount.Uid,
                        };
                        newOrganization.ErpCOREDBContext.Loans.Add(existLoan);
                        newOrganization.SaveChanges();
                    }
                    else
                    {

                    }

                });
        }
        private void Copy_Financial_Lends()
        {
            Console.WriteLine("> Copy_Financial_Lends");

            oldOrganization.ErpNodeDBContext
                .Lends
                .OrderByDescending(s => s.TrnDate)
                .ToList()
                .ForEach(oldPurchase =>
                {
                    var existLoan = newOrganization.ErpCOREDBContext
                        .Lends
                        .FirstOrDefault(s => s.Id == oldPurchase.Uid);

                    if (existLoan == null)
                    {
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine($"> Lends: {oldPurchase.Name} ");

                        existLoan = new Enterprise.Models.Financial.Lend()
                        {
                            Id = oldPurchase.Uid,
                            Date = oldPurchase.TrnDate,
                            Memo = "NA",
                            AmountLend = oldPurchase.Amount,
                            PayFrom_AssetAccountId = oldPurchase.AssetAccountGuid,
                            Lending_AssetAccountId = oldPurchase.LendingAssetAccountGuid,
                        };
                        newOrganization.ErpCOREDBContext.Lends.Add(existLoan);
                        newOrganization.SaveChanges();
                    }
                    else
                    {

                    }

                });
        }
        private void Copy_Financial_Retentions_Types()
        {

            Console.WriteLine("> Copy_Financial_Retentions_Types");
            var existModelIds = newOrganization.ErpCOREDBContext.RetentionTypes
            .Select(x => x.Id)
            .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.RetentionTypes
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();


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

            //newOrganization.ErpCOREDBContext.SaveChanges();
        }
        private void Copy_Financial_Retentions_Groups()
        {
            Console.WriteLine("> Copy_Financial_Retentions_Groups");

            var existModelIds = newOrganization.ErpCOREDBContext.RetentionGroups
            .Select(x => x.Id)
            .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.RetentionGroups
                .Where(i => !existModelIds.Contains(i.Id))
                .ToList();

            oldModels.ForEach(oldModel =>
            {
                Console.WriteLine($"RetentionGroups:{oldModel.Id}");
                var exist = newOrganization.ErpCOREDBContext
                .RetentionGroups
                .FirstOrDefault(x => x.Id == oldModel.Id);

                if (exist == null)
                {
                    Console.WriteLine($"> Add");

                    exist = new ERPKeeperCore.Enterprise.Models.Financial.RetentionGroup()
                    {
                        Id = oldModel.Id,
                        AmountCommercial = oldModel.AmountCommercial,
                        Date = oldModel.TrnDate,
                        AmountRetention = oldModel.AmountRetention,
                        Count = oldModel.Count,
                        RetentionTypeId = oldModel.RetentionTypeId,
                    };

                    newOrganization.ErpCOREDBContext.RetentionGroups.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {

                }
            });

            newOrganization.ErpCOREDBContext.SaveChanges();
        }
    }
}
