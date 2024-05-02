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

        public Decimal Debit { get; private set; }
        public Decimal Credit { get; private set; }


        public virtual ICollection<FundTransferItem> Items { get; set; } = new List<FundTransferItem>();

        public void AddCredit(Guid AccountGuid, decimal amount)
        {

            var item = new FundTransferItem()
            {
                AccountId = AccountGuid,
                Debit = 0,
                Credit = amount,
            };
            this.Items.Add(item);
        }

        public void AddDebit(Guid AccountGuid, decimal amount)
        {

            var item = new FundTransferItem()
            {
                AccountId = AccountGuid,
                Debit = amount,
                Credit = 0,
            };
            this.Items.Add(item);
        }

        public void Refresh()
        {
            Debit = Items.Sum(x => x.Debit);
            Credit = Items.Sum(x => x.Credit);
        }

        public void CreateTransaction()
        {
            if (this.Transaction == null)
            {
                Console.WriteLine("Creating FundTransfer Transaction");

                this.Transaction = new Accounting.Transaction()
                {
                    Id = this.Id,
                    Date = this.Date,
                    Reference = this.Name,
                    Type = Accounting.Enums.TransactionType.FundTransfer,
                    FundTransfer = this,
                };
            }
            this.Transaction.ClearLedger();
            this.Transaction.UpdateBalance();
        }

        public void Post()
        {
            this.CreateTransaction();
           
            foreach (var item in this.Items)
            {
                var ledger = new Accounting.TransactionLedger()
                {
                    AccountId = item.AccountId,
                    Account = item.Account,
                    Debit = item.Debit,
                    Credit = item.Credit,
                    Transaction = this.Transaction,
                    TransactionId = this.Transaction.Id,
                };

                this.Transaction.Ledgers.Add(ledger);
            }
        }
    }
}