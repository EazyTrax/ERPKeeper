﻿using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Suppliers.Enums;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace ERPKeeperCore.Enterprise.Models.Suppliers
{
    public partial class Purchase
    {
        [Key]
        public Guid Id { get; set; }


        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }



        public Guid SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Suppliers.Supplier? Supplier { get; set; }
        public Guid? SupplierAddressId { get; set; }


        public String? Reference { get; set; }
        public PurchaseStatus Status { get; set; }
        public String? Memo { get; set; }
        public int No { get; set; }
        public String? Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;



        public Decimal LinesTotal { get; set; }
        public Decimal Discount { get; set; }
        public Decimal LinesTotalAfterDiscount => LinesTotal - Discount;

        public Decimal Tax { get; set; }
        public Decimal Total => LinesTotalAfterDiscount + Tax;

        public virtual ICollection<PurchaseItem> Items { get; set; }
            = new List<PurchaseItem>();

        public virtual SupplierPayment SupplierPayment { get; set; }

        public Guid? TaxCodeId { get; set; }
        [ForeignKey("TaxCodeId")]
        public virtual TaxCode? TaxCode { get; set; }

        public Guid? TaxPeriodId { get; set; }
        [ForeignKey("TaxPeriodId")]
        public virtual TaxPeriod? TaxPeriod { get; set; }


        public Guid? PayableAccountId { get; set; }
        [ForeignKey("PayableAccountId")]
        public virtual Account PayableAccount { get; set; }


        public Guid? IncomeAccount_DiscountTakenId { get; set; }
        [ForeignKey("IncomeAccount_DiscountTakenId")]
        public virtual Account? IncomeAccount_DiscountTaken { get; set; }


        public Guid? ExpenseAccountId { get; set; }
        [ForeignKey("ExpenseAccountId")]
        public virtual Account? ExpenseAccount { get; set; }



        public void UpdateBalance()
        {
            this.LinesTotal = this.Items.Where(i => i.LineTotal > 0).Sum(i => i.LineTotal);

            if (this.TaxCode != null)
                this.Tax = this.TaxCode.Rate * this.LinesTotalAfterDiscount / 100;
            else
                this.Tax = 0;
        }

        public void Post_Ledgers(int order)
        {
            Console.WriteLine($">{order} Post PUR:{this.Name}");

            this.UpdateBalance();

            if (this.Transaction == null)
                return;
            if (this.ExpenseAccount == null)
                return;
            if (this.Total == 0)
                return;

            this.Transaction.ClearLedger();
            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;
            this.Transaction.Name = this.Name;
            this.Transaction.PostedDate = DateTime.Now;

            //Dr. Post Items
            this.Transaction.AddDebit(this.ExpenseAccount, this.LinesTotal, $"");

            if (this.TaxCode != null && this.TaxCode.InputTaxAccountId != null)
                this.Transaction.AddDebit(this.TaxCode.InputTaxAccount, this.Tax);

            //Cr. Post Receivable
            if (this.PayableAccount != null)
                this.Transaction.AddCredit(this.PayableAccount, this.Total);

            if (this.Discount != 0 && this.IncomeAccount_DiscountTaken != null)
                this.Transaction.AddCredit(this.IncomeAccount_DiscountTaken, this.Discount);

            IsPosted = this.Transaction.UpdateBalance();

        }

        public void UnPostLedger()
        {
            Console.WriteLine($">UnPost  SL:{this.Name}");

            if (Transaction == null)
            {
                this.IsPosted = false;
                return;
            }
            else
            {
                this.Transaction.ClearLedger();
                this.Transaction.Date = this.Date;
                this.Transaction.Reference = this.Reference;
                this.Transaction.Name = this.Name;
                this.IsPosted = false;
            }
        }

        internal void AssignDefaultAccount(Account expenseAccount, Account incomeAccount_DiscountTaken, Account payableAccount)
        {
            if (this.PayableAccount == null)
            {
                this.PayableAccount = payableAccount;
                this.PayableAccountId = this.PayableAccount?.Id;
            }

            if (this.IncomeAccount_DiscountTaken == null)
            {
                this.IncomeAccount_DiscountTaken = incomeAccount_DiscountTaken;
                this.IncomeAccount_DiscountTakenId = this.IncomeAccount_DiscountTaken?.Id;
            }

            if (this.ExpenseAccount == null)
            {
                this.ExpenseAccount = this.Items.FirstOrDefault()?.Item?.PurchaseAccount;
                this.ExpenseAccountId = this.ExpenseAccount?.Id;
            }
        }
    }
}
