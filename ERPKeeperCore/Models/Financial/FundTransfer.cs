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


        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }




        public DateTime Date { get; set; }
        public String? Reference { get; set; }
        public FundTransferStatus Status { get; set; }
        public string Name => string.Format("{0}-{1}/{2}", "FT", DocumentGroup, No.ToString().PadLeft(3, '0'));
        public string DocumentGroup => this.Date.ToString("yyMM");
        public int No { get; set; }
        public String? Memo { get; set; }

        public Guid? WithDrawAccountId { get; set; }
        [ForeignKey("WithDrawAccountId")]
        public virtual Accounting.Account WithDrawAccount { get; set; }

        public Decimal WithDrawAmount { get; set; }

        public virtual ICollection<FundTransferItem> FundTransferDepositLines { get; set; } = new List<FundTransferItem>();

        public void AddDepositLine(Guid AccountGuid, decimal amount)
        {
            var item = new FundTransferItem()
            {
                AccountId = AccountGuid,
                Debit = amount,
            };
            this.FundTransferDepositLines.Add(item);
        }

        public void UpdateBalance()
        {
            WithDrawAmount = FundTransferDepositLines.Sum(x => x.Debit);
        }

        public void PostToTransaction()
        {
            Console.WriteLine($">Post  FT:{this.Name}");

            if (this.Transaction == null) return;
            this.Transaction.ClearLedger();
            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;
            this.Transaction.PostedDate = DateTime.Now;


            //Step 2. Prepare Data
            this.UpdateBalance();

            //Step 3. Post Data
            // Dr.
            foreach (var item in this.FundTransferDepositLines)
                this.Transaction.AddDebit(item.Account, item.Debit);
            // Cr.
            this.Transaction.AddCredit(this.WithDrawAccount, this.WithDrawAmount);


            // Step 4 Finalize
            this.Transaction.UpdateBalance();
            this.IsPosted = true;

        }

    }
}