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
            if (this.TransactionId != null)
            {
                Console.Write($"Post SL:{this.Name}");

                this.Transaction.ClearLedger();

                foreach (var item in this.Items)
                {
                    Console.WriteLine($"{item.Item.IncomeAccount.Name} {item.LineTotal}");
                    this.Transaction.AddAcount(item.Item.IncomeAccount, 0, item.LineTotal);
                }

                if (this.TaxCode != null && this.TaxCode.OutputTaxAccountId != null)
                {
                    Console.WriteLine($"{this.TaxCode.OutputTaxAccount.Name} {this.Tax}");
                    this.Transaction.AddAcount(this.TaxCode.OutputTaxAccount, 0, this.TaxCode.Rate * this.LinesTotal / 100);
                }
                this.Transaction.UpdateBalance();

            }
        }

        public void CreateTransaction()
        {
            if (this.Transaction == null)
            {
                Console.Write($"Create TR:{this.Name}");

                this.Transaction = new Accounting.Transaction()
                {
                    Id = this.Id,
                    Date = this.Date,
                    Reference = this.Name,
                    Type = Accounting.Enums.TransactionType.Sale,
                    Sale = this,
                };
            }
        }


    }
}
