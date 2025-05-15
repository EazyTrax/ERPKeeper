using ERPKeeperCore.Enterprise.DBContext;
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

    public class LiabilityPayment
    {

        [Key]
        public Guid Id { get; set; }
        public LiabilityPaymentStatus Status { get; set; }
        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }
        public String? Reference { get; set; }
        public String? Memo { get; set; }
        public int No { get; set; }
        public String? Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;

        public Decimal Amount { get; set; }
        public Decimal AmountBankFee { get; set; }
        public decimal AmountDiscount { get; set; }
        public Decimal AmountTotal => Amount - (AmountDiscount + AmountBankFee);


        public virtual ICollection<Financial.LiabilityPaymentPayFromAccount> LiabilityPaymentPayFromAccounts { get; set; }
        = new HashSet<Financial.LiabilityPaymentPayFromAccount>();


        // Dr.
        public Guid? LiabilityAccountId { get; set; }
        [ForeignKey("LiabilityAccountId")]
        public virtual Accounting.Account? LiabilityAccount { get; set; }


        public decimal PayFromAmount { get; private set; }
        public int PayFromCount { get; private set; }

        public void UpdateBalance()
        {
            this.Amount = this.LiabilityPaymentPayFromAccounts.Sum(x => x.Amount);
            this.PayFromCount = this.LiabilityPaymentPayFromAccounts.Count();
        }


        public void PostLedgers()
        {
            Console.WriteLine($">Post LiabilityPayment:{this.Name}");

            this.UpdateBalance();

            if (this.Transaction == null)
                return;

            this.Transaction.ClearLedger();
            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;
            this.Transaction.PostedDate = DateTime.Today;

            // Dr.
            this.Transaction.AddDebit(this.LiabilityAccount, this.Amount);


            // Dr.
            foreach (var item in this.LiabilityPaymentPayFromAccounts)
            {
                this.Transaction.AddCredit(item.Account, item.Amount);
            }
            IsPosted = this.Transaction.UpdateBalance();
        }

        public void UnPostLedger()
        {
            Console.WriteLine($">UnPost LiabilityPayment:{this.Name}");

            if (this.Transaction == null)
                return;

            this.Transaction.ClearLedger();
            this.IsPosted = false;

        }

        public void UpdateName()
        {
            this.Name = "LP-" + this.Date.Year + "-" + this.Date.Month.ToString("00") + "-" + this.No.ToString("0000");
        }
    }
}

