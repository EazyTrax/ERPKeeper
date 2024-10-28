
using ERPKeeperCore.Enterprise.DBContext;
using ERPKeeperCore.Enterprise.Models.Taxes.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Taxes
{
    public class IncomeTax
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsPosted { get; set; }

        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }



        public int No { get; set; }

        public DateTime Date { get; set; }
        public string? Name => string.Format("INT{0}-{1}", this.Date.ToString("yyMM"), this.No.ToString().PadLeft(3, '0'));


        public Decimal ProfitAmount { get; set; }
        public Decimal TaxAmount { get; set; }
        public Decimal TaxReceivable_Amount { get; set; }
        public Decimal PayFrom_TaxReceiveableAccount_Amount { get; set; }
        public Decimal Ramain_TaxReceiveableAccount_Amount { get; set; }
        public Decimal Remain_Liability_Amount { get; set; }


        public void CalculateBalance()
        {
            this.Tax_Receiveable_AccountId = Tax_Receiveable_AccountId;
            this.Tax_Liability_AccountId = Tax_Liability_AccountId;
            // this.PayFrom_CashEquivalent_AccountId = PayFrom_AssetAccountId;



            PayFrom_TaxReceiveableAccount_Amount = Math.Min(TaxReceivable_Amount, TaxAmount);
            Ramain_TaxReceiveableAccount_Amount = TaxReceivable_Amount - PayFrom_TaxReceiveableAccount_Amount;
            Remain_Liability_Amount = TaxAmount - PayFrom_TaxReceiveableAccount_Amount;
        }


        public String? Memo { get; set; }

        public Guid? FiscalYearId { get; set; }
        [ForeignKey("FiscalYearId")]
        public virtual Accounting.FiscalYear FiscalYear { get; set; }

        public Guid? ExpenseAccountId { get; set; }
        [ForeignKey("ExpenseAccountId")]
        public virtual Accounting.Account ExpenseAccount { get; set; }


        //Cr.
        public Guid? Tax_Receiveable_AccountId { get; set; }
        [ForeignKey("Tax_Receiveable_AccountId")]
        public virtual Accounting.Account TaxReceiveable_Account { get; set; }


        public Guid? Tax_Liability_AccountId { get; set; }
        [ForeignKey("Tax_Liability_AccountId")]
        public virtual Accounting.Account Tax_Liability_Account { get; set; }

        //Cr.
        public Guid? PayFrom_CashEquivalent_AccountId { get; set; }
        [ForeignKey("PayFrom_CashEquivalent_AccountId")]
        public virtual Accounting.Account PayFrom_CashEquivalent_Account { get; set; }

        public Guid? WriteOff_TaxReceiveable_ExpenseAccountId { get; set; }
        [ForeignKey("WriteOff_TaxReceiveable_ExpenseAccountId")]
        public virtual Accounting.Account WriteOff_TaxReceiveable_ExpenseAccount { get; set; }


        public string? Reference { get; set; }
        public IncomeTaxStatus Status { get; set; }

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
                    Type = Accounting.Enums.TransactionType.IncomeTax,
                    IncomeTax = this,
                };
            }
        }

        public void PostToTransaction()
        {
            Console.WriteLine($">Post > IncomeTaxes:{this.Name}");

            this.CalculateBalance();

            if (this.Transaction == null) return;

            this.Transaction.ClearLedger();
            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;
            this.Transaction.PostedDate = DateTime.Now;

            // Dr.
            this.Transaction.AddDebit(this.ExpenseAccount, this.TaxAmount);

            if (this.Ramain_TaxReceiveableAccount_Amount > 0)
            {
                this.Transaction.AddDebit(this.WriteOff_TaxReceiveable_ExpenseAccount, this.Ramain_TaxReceiveableAccount_Amount);
                this.Transaction.AddCredit(this.TaxReceiveable_Account, this.Ramain_TaxReceiveableAccount_Amount);
            }
            if (this.TaxReceiveable_Account != null)
                this.Transaction.AddCredit(this.TaxReceiveable_Account, this.PayFrom_TaxReceiveableAccount_Amount);
            if (this.Remain_Liability_Amount > 0)
                this.Transaction.AddCredit(this.Tax_Liability_Account, this.Remain_Liability_Amount);

            this.IsPosted = this.Transaction.UpdateBalance();
        }

        internal void UnPost()
        {
            if (this.Transaction != null)
            {
                this.Transaction.ClearLedger();
                this.Transaction.Debit = 0;
                this.Transaction.Credit = 0;
            }

            this.IsPosted = false;
        }
    }
}