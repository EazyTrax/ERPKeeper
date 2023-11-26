using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KeeperCore.ERPNode.Models.Financial.Payments
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

            this.AssetAccountId = debtPayment.AssetAccountId;
            this.AmountPayFromOptionalAcc = debtPayment.AmountPayFromOptionalAcc;
            this.TrnDate = debtPayment.TrnDate;
            this.Memo = debtPayment.Memo;

            debtPayment.CalculateAmount();

        }


    }

}




