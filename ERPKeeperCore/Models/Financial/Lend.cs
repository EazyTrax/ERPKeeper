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

    public class Lend
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }

        public DateTime Date { get; set; }
        public String? Reference { get; set; }
        public LendStatus Status { get; set; }
        public string Name => string.Format("{0}-{1}/{2}", "LEND", DocumentGroup, No.ToString().PadLeft(3, '0'));
        public string DocumentGroup => this.Date.ToString("yyMM");
        public int No { get; set; }
        public String? Memo { get; set; }

        public Guid? PayFrom_AssetAccountId { get; set; }
        [ForeignKey("PayFrom_AssetAccountId")]
        public virtual Accounting.Account? PayFrom_AssetAccount { get; set; }


        public Guid? Lending_AssetAccountId { get; set; }
        [ForeignKey("Lending_AssetAccountId")]
        public virtual Accounting.Account? Lending_AssetAccount { get; set; }


        public Decimal AmountLend { get; set; }

        public virtual ICollection<LendReturn> LendReturns { get; set; } = new List<LendReturn>();
        public Decimal AmountReturn { get; set; }
        public Decimal AmountRemain => AmountLend - AmountReturn;



        public void AddReturnLine(DateTime date, decimal amount, Guid AccountId)
        {
            var item = new LendReturn()
            {
                Date = date,
                ReturnToAccountId = AccountId,
                Amount = amount,
            };
            this.LendReturns.Add(item);
        }

        public void UpdateBalance()
        {
            AmountReturn = LendReturns.Sum(x => x.Amount);
        }

        public void CreateTransaction()
        {
            if (this.Transaction == null)
            {
                Console.WriteLine("Creating Lend Transaction");

                this.Transaction = new Accounting.Transaction()
                {
                    Id = this.Id,
                    Date = this.Date,
                    Reference = this.Name,
                    Type = Accounting.Enums.TransactionType.Lend,
                    Lend = this,
                };
            }
        }

        public void PostToTransaction()
        {
            Console.WriteLine($">Post  FT:{this.Name}");

            this.UpdateBalance();

            if (this.Transaction == null)
                return;


            this.Transaction.ClearLedger();
            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;

            // Dr.
            this.Transaction.AddDebit(this.Lending_AssetAccount, this.AmountLend);

            // Cr.
            this.Transaction.AddCredit(this.PayFrom_AssetAccount, this.AmountLend);


            IsPosted = this.Transaction.UpdateBalance();

        }

    }
}