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

    public class Loan
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }

        public DateTime Date { get; set; }
        public String? Reference { get; set; }
        public LoanStatus Status { get; set; }
        public string Name => string.Format("{0}-{1}/{2}", "FT", DocumentGroup, No.ToString().PadLeft(3, '0'));
        public string DocumentGroup => this.Date.ToString("yyMM");
        public int No { get; set; }
        public String? Memo { get; set; }

        public Guid? RecevingAccountId { get; set; }
        [ForeignKey("RecevingAccountId")]
        public virtual Accounting.Account? Receving_AssetAccount { get; set; }

        public Guid? LoanAccountId { get; set; }
        [ForeignKey("LoanAccountId")]
        public virtual Accounting.Account? Loan_LiabilityAccount { get; set; }

        public Decimal AmountLoan { get; set; }
        public virtual ICollection<LoanReturn> LoanReturns { get; set; } = new List<LoanReturn>();
        public Decimal AmountReturn { get; set; }
        public Decimal AmountRemain => AmountLoan - AmountReturn;

        public void AddReturnLine(DateTime date, decimal amount, Guid AccountId)
        {
            var item = new LoanReturn()
            {
                Date = date,
                ReturnFromAccountId = AccountId,
                Amount = amount,
            };
            this.LoanReturns.Add(item);
        }

        public void UpdateBalance()
        {
            AmountReturn = LoanReturns.Sum(x => x.Amount);
        }

        public void CreateTransaction()
        {
            if (this.Transaction == null)
            {
                Console.WriteLine("Creating Loan Transaction");

                this.Transaction = new Accounting.Transaction()
                {
                    Id = this.Id,
                    Date = this.Date,
                    Reference = this.Name,
                    Type = Accounting.Enums.TransactionType.Loan,
                    Loan = this,
                };
            }
        }

        public void PostToTransaction()
        {
            Console.WriteLine($">Post  LOAN:{this.Name}");

            this.UpdateBalance();

            if (this.Transaction == null)
                return;


            this.Transaction.ClearLedger();      
            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;
            this.Transaction.PostedDate = DateTime.Now;

            // Dr.
            this.Transaction.AddDebit(this.Receving_AssetAccount, this.AmountLoan);

            // Cr.
            this.Transaction.AddCredit(this.Loan_LiabilityAccount, this.AmountLoan);



            IsPosted = this.Transaction.UpdateBalance();

        }

    }
}