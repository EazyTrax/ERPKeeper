using ERPKeeperCore.Enterprise.Models.Accounting;
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



        public Guid? SupplierId { get; set; }
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
        public Decimal Tax { get; set; }
        public Decimal Total => LinesTotal + Tax;

        public virtual ICollection<PurchaseItem> Items { get; set; }
            = new List<PurchaseItem>();
        public virtual ICollection<SupplierPayment> SupplierPayments { get; set; }
            = new List<SupplierPayment>();



        public Guid? TaxCodeId { get; set; }
        [ForeignKey("TaxCodeId")]
        public virtual TaxCode? TaxCode { get; set; }

        public Guid? TaxPeriodId { get; set; }
        [ForeignKey("TaxPeriodId")]
        public virtual TaxPeriod? TaxPeriod { get; set; }

        public Guid? PayableAccountId { get; set; }
        [ForeignKey("PayableAccountId")]
        public virtual Account PayableAccount { get; set; }

        public void UpdateBalance()
        {
            this.LinesTotal = this.Items.Sum(i => i.LineTotal);

            if (this.TaxCode != null)
                this.Tax = this.TaxCode.Rate * this.LinesTotal / 100;
            else
                this.Tax = 0;
        }




        public void PostToTransaction()
        {
            Console.WriteLine($">Post  PUR:{this.Name}");

            this.UpdateBalance();

            if (this.Transaction == null)
                return;

            this.Transaction.ClearLedger();

            // Post Items
            foreach (var item in this.Items)
            {
                Console.WriteLine($"{item.Item.PurchaseAccount.Name} {item.LineTotal}");
                this.Transaction.AddDebit(item.Item.PurchaseAccount, item.LineTotal);
            }

            //Post Taxes
            if (this.TaxCode != null && this.TaxCode.InputTaxAccountId != null)
            {
                Console.WriteLine($"{this.TaxCode.InputTaxAccount.Name} {this.Tax}");
                this.Transaction.AddDebit(this.TaxCode.InputTaxAccount, this.Tax);
            }

            //Post Receivable
            if (this.PayableAccount != null)
            {
                Console.WriteLine($"{this.PayableAccount.Name} {this.Total}");
                this.Transaction.AddCredit(this.PayableAccount, this.Total);
            }

            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;
            this.Transaction.Name = this.Name;
            this.Transaction.UpdateBalance();
            this.Transaction.PostedDate = DateTime.Now;
            this.IsPosted = true;

        }
    }
}
