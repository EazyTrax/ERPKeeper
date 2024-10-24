﻿
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
        public string Name => string.Format("INT{0}-{1}", this.Date.ToString("yyMM"), this.No.ToString().PadLeft(3, '0'));
        public Decimal ProfitAmount { get; set; }
        public Decimal TaxAmount { get; set; }

        public Decimal TaxReceivable_Amount { get; set; }
        public Decimal PayFrom_TaxReceiveableAccount_Amount { get; set; }



        public Decimal Ramain_TaxReceiveableAccount_Amount => Math.Max(TaxReceivable_Amount - PayFrom_TaxReceiveableAccount_Amount, 0);
        public Decimal Remain_Liability_Amount => TaxAmount - PayFrom_TaxReceiveableAccount_Amount;


        public String? Memo { get; set; }

        public Guid? FiscalYearId { get; set; }
        [ForeignKey("FiscalYearId")]
        public virtual Accounting.FiscalYear FiscalYear { get; set; }

        public Guid? ExpenseAccountId { get; set; }
        [ForeignKey("ExpenseAccountId")]
        public virtual Accounting.Account ExpenseAccount { get; set; }

        //Cr.
        public Guid? PayFrom_AssetAccountId { get; set; }
        [ForeignKey("PayFrom_AssetAccountId")]
        public virtual Accounting.Account PayFrom_AssetAccount { get; set; }

        public Guid? LiabilityAccountId { get; set; }
        [ForeignKey("LiabilityAccountId")]
        public virtual Accounting.Account LiabilityAccount { get; set; }

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

            this.UpdateBalance();

            if (this.Transaction == null) return;

            this.Transaction.ClearLedger();
            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;
            this.Transaction.PostedDate = DateTime.Now;

            // Dr.
            this.Transaction.AddDebit(this.ExpenseAccount, this.TaxAmount);
            if (this.Remain_Liability_Amount < 0)
                this.Transaction.AddDebit(this.WriteOff_TaxReceiveable_ExpenseAccount, this.Remain_Liability_Amount * -1);

            // Cr.
            if (this.PayFrom_AssetAccount != null)
                this.Transaction.AddCredit(this.PayFrom_AssetAccount, this.PayFrom_TaxReceiveableAccount_Amount);
            if (this.Remain_Liability_Amount > 0)
                this.Transaction.AddCredit(this.LiabilityAccount, this.Remain_Liability_Amount);

            this.IsPosted = this.Transaction.UpdateBalance();
        }

        private void UpdateBalance()
        {

        }
    }
}
