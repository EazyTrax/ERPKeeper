using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeper.Node.Models.Financial.Payments
{
    public class LiabilityPayment : GeneralPayment
    {
        public void Update(LiabilityPayment debtPayment)
        {
            if (this.PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Aleardy Locked");

            this.LiabilityAmount = debtPayment.LiabilityAmount;
            this.Reference = debtPayment.Reference;
            this.BankFeeAmount = debtPayment.BankFeeAmount;

            this.AssetAccountUid = debtPayment.AssetAccountUid;
            this.OptionalAssetAccountUid = debtPayment.OptionalAssetAccountUid;
            this.AmountPayFromOptionalAcc = debtPayment.AmountPayFromOptionalAcc;
            this.TrnDate = debtPayment.TrnDate;
            this.Memo = debtPayment.Memo;

            debtPayment.CalculateAmount();

        }


    }

}




