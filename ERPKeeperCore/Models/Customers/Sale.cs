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

        public Guid? ReceivePaymentId { get; set; }
        [ForeignKey("ReceivePaymentId")]
        public virtual Customers.ReceivePayment? ReceivePayment { get; set; }

        public Guid? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customers.Customer? Customer { get; set; }


        public String? Reference { get; set; }
        public String? Memo { get; set; }
        public int No { get; set; }
        public String? Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public Decimal LinesTotal { get; set; }
        public Decimal Tax { get; set; }
        public Decimal Total => LinesTotal + Tax;

        public virtual ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();




        public Guid? ReceivableAccountId { get; set; }
        [ForeignKey("ReceivableAccountId")]
        public virtual Account ReceivableAccount { get; set; }









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
            Console.Write($"Post SL:{this.Name}");

            this.UpdateBalance();

            if (this.Transaction == null)
                return;

            this.Transaction.ClearLedger();

            // Post ITEMS
            foreach (var item in this.Items)
            {
                Console.WriteLine($"{item.Item.IncomeAccount.Name} {item.LineTotal}");
                this.Transaction.AddCredit(item.Item.IncomeAccount, item.LineTotal);
            }

            //Post Taxs
            if (this.TaxCode != null && this.TaxCode.OutputTaxAccountId != null)
            {
                Console.WriteLine($"{this.TaxCode.OutputTaxAccount.Name} {this.Tax}");
                this.Transaction.AddCredit(this.TaxCode.OutputTaxAccount, this.Tax);
            }

            //Post Receivable
            if (this.ReceivableAccount != null)
            {
                Console.WriteLine($"{this.ReceivableAccount.Name} {this.Total}");
                this.Transaction.AddDebit(this.ReceivableAccount, this.Total);
            }

            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;
            this.Transaction.UpdateBalance();
            this.Transaction.PostedDate = DateTime.Now;
            this.IsPosted = true;

        }
        public void UpdateBalance()
        {
            this.LinesTotal = this.Items.Sum(i => i.LineTotal);

            if (this.TaxCode != null)
                this.Tax = this.TaxCode.Rate * this.LinesTotal / 100;
            else
                this.Tax = 0;
        }

 

    }
}
