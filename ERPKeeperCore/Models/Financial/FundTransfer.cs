using ERPKeeperCore.Enterprise.Models.Financial.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Financial
{

    public class FundTransfer
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public String? Reference { get; set; }
        public FundTransferStatus Status { get; set; }
        public string Name => string.Format("{0}-{1}/{2}", "FT", DocumentGroup, No.ToString().PadLeft(3, '0'));
        public string DocumentGroup => this.Date.ToString("yyMM");
        public int No { get; set; }

        public String? Memo { get; set; }

        public Decimal TotalDebit { get; set; }
        public Decimal TotalCredit { get; set; }


        public Decimal AmountwithDraw { get; set; }
        public Decimal AmountFee { get; set; }



        public Guid? WithDrawAccountGuid { get; set; }
        [ForeignKey("WithDrawAccountGuid")]
        public virtual Accounting.Account WithDrawAccount { get; set; }


        public Guid? DepositAccountGuid { get; set; }
        [ForeignKey("DepositAccountGuid")]
        public virtual Accounting.Account DepositAccount { get; set; }


        public Guid? BankFeeAccountGuid { get; set; }
        [ForeignKey("BankFeeAccountGuid")]
        public virtual Accounting.Account BankFeeAccount { get; set; }


        public virtual ICollection<FundTransferItem> Items { get; set; } = new List<FundTransferItem>();

        public void AddCredit(Guid AccountGuid, decimal amount)
        {

            var item = new FundTransferItem()
            {
                AccountId = AccountGuid,
                DebitAmount = 0,
                CreditAmount = amount,
            };
            this.Items.Add(item);
        }

        public void AddDebit(Guid AccountGuid, decimal amount)
        {

            var item = new FundTransferItem()
            {
                AccountId = AccountGuid,
                DebitAmount = amount,
                CreditAmount = 0,
            };
            this.Items.Add(item);
        }

        public void Refresh()
        {
            TotalDebit = Items.Sum(x => x.DebitAmount);
            TotalCredit = Items.Sum(x => x.CreditAmount);
        }
    }
}