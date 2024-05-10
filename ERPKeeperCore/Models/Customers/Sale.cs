using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace ERPKeeperCore.Enterprise.Models.Customers
{
    public class Sale
    {
        [Key]
        public Guid Id { get; set; }
        public SaleStatus Status { get; set; }

        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }


        public Guid? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customers.Customer? Customer { get; set; }


        public String? Reference { get; set; }
        public String? Memo { get; set; }
        public int No { get; set; }
        public String? Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;


        public Decimal LinesTotal { get; set; }
        public Decimal Discount { get; set; }
        public Decimal LinesTotalAfterDiscount => LinesTotal - Discount;

        public Decimal Tax { get; set; }
        public Decimal Total => LinesTotalAfterDiscount + Tax;

        public virtual ICollection<SaleItem> Items { get; set; }
            = new List<SaleItem>();

        public virtual ICollection<ReceivePayment> ReceivePayments { get; set; }
            = new List<ReceivePayment>();

        public Guid? ReceivableAccountId { get; set; }
        [ForeignKey("ReceivableAccountId")]
        public virtual Account? ReceivableAccount { get; set; }

        public Guid? Discount_Given_Expense_AccountId { get; set; }
        [ForeignKey("DiscountAccountId")]
        public virtual Accounting.Account? Discount_Given_Expense_Account { get; set; }

        public Guid? TaxCodeId { get; set; }
        [ForeignKey("TaxCodeId")]
        public virtual TaxCode? TaxCode { get; set; }

        public Guid? TaxPeriodId { get; set; }
        [ForeignKey("TaxPeriodId")]
        public virtual TaxPeriod? TaxPeriod { get; set; }
        public Guid? CustomerAddressId { get; set; }

        public void AddItem(SaleItem existItem)
        {
            this.Items.Add(existItem);
        }

        public void PostToTransaction()
        {
            Console.WriteLine($">Post  SL:{this.Name}");

            this.UpdateBalance();

            if (this.Transaction == null)
                return;

            this.Transaction.ClearLedger();
            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;
            this.Transaction.Name = this.Name;



            //Dr. Post Receivable
            if (this.ReceivableAccount != null)
                this.Transaction.AddDebit(this.ReceivableAccount, this.Total);

            if (this.Discount != 0 && this.Discount_Given_Expense_Account != null)
                this.Transaction.AddDebit(this.Discount_Given_Expense_Account, this.Discount);

            //Cr. Post ITEMS
            foreach (var item in this.Items.Where(i => i.LineTotal > 0))
                this.Transaction.AddCredit(item.Item.IncomeAccount, item.LineTotal, $"{item.Quantity} x {item.PartNumber}");
            //Cr. Post Taxs
            if (this.TaxCode != null && this.TaxCode.OutputTaxAccountId != null)
                this.Transaction.AddCredit(this.TaxCode.OutputTaxAccount, this.Tax);




            this.Transaction.UpdateBalance();
            this.Transaction.PostedDate = DateTime.Now;
            this.IsPosted = true;

        }
        public void UpdateBalance()
        {
            this.LinesTotal = this.Items.Where(i => i.LineTotal > 0).Sum(i => i.LineTotal);

            if (this.TaxCode != null)
                this.Tax = this.TaxCode.Rate * this.LinesTotalAfterDiscount / 100;
            else
                this.Tax = 0;
        }
    }
}
