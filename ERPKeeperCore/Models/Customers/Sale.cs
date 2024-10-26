using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Items;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;


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


        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customers.Customer? Customer { get; set; }


        public Guid? TaxCodeId { get; set; }
        [ForeignKey("TaxCodeId")]
        public virtual TaxCode? TaxCode { get; set; }


        public Guid? TaxPeriodId { get; set; }
        [ForeignKey("TaxPeriodId")]
        public virtual TaxPeriod? TaxPeriod { get; set; }



        public String? Reference { get; set; }
        public String? Memo { get; set; }
        public int No { get; set; }
        public String? Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public int AgeInDays
        {
            get
            {
                var endDate = ReceivePayment?.Date ?? DateTime.Now;
                return (endDate - Date).Days;
            }
        }

        public Decimal LinesTotal { get; set; }
        public Decimal Discount { get; set; }
        public Decimal LinesTotalAfterDiscount => LinesTotal - Discount;
        public Decimal Tax { get; set; }
        public Decimal Total => LinesTotalAfterDiscount + Tax;

        public virtual ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
        public virtual ReceivePayment ReceivePayment { get; set; }

        public Guid? IncomeAccountId { get; set; }
        [ForeignKey("IncomeAccountId")]
        public virtual Account? IncomeAccount { get; set; }

        public Guid? ReceivableAccountId { get; set; }
        [ForeignKey("ReceivableAccountId")]
        public virtual Account? ReceivableAccount { get; set; }

        public Guid? Discount_Given_Expense_AccountId { get; set; }
        [ForeignKey("DiscountAccountId")]
        public virtual Accounting.Account? Discount_Given_Expense_Account { get; set; }

        public Guid? CustomerAddressId { get; set; }

        public Guid? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Projects.Project? Project { get; set; }



















        public void AddItem(SaleItem existItem)
        {
            this.Items.Add(existItem);
        }

        public void Post_Ledgers(int order = 1)
        {
            Console.WriteLine($"> {order} Posting SaleL:{this.Name}");

            this.UpdateBalance();

            if (this.Transaction == null)
                return;

            this.Transaction.ClearLedger();
            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;
            this.Transaction.Name = this.Name;

            //Dr. Post Receivable
            this.Transaction.AddDebit(this.ReceivableAccount, this.Total);

            if (this.Discount != 0 && this.Discount_Given_Expense_Account != null)
                this.Transaction.AddDebit(this.Discount_Given_Expense_Account, this.Discount);

            //Cr. Post Taxs
            this.Transaction.AddCredit(this.IncomeAccount, this.LinesTotal);

            //Cr. Post Taxs
            if (this.TaxCode != null && this.TaxCode.OutputTaxAccountId != null)
                this.Transaction.AddCredit(this.TaxCode.OutputTaxAccount, this.Tax);

            IsPosted = this.Transaction.UpdateBalance();

        }

        public void Reorder()
        {
            int index = 1;
            this.Items = this.Items.OrderBy(i => i.Order).ToList();

            foreach (var item in Items)
            {
                item.Order = index++;
            }
        }


        public void UpdateBalance()
        {
            this.LinesTotal = this.Items.Where(i => i.LineTotal > 0).Sum(i => i.LineTotal);

            if (this.TaxCode != null)
                this.Tax = this.TaxCode.Rate * this.LinesTotalAfterDiscount / 100;
            else
                this.Tax = 0;

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

        internal void AssignDefaultAccount(Account incomeAccount, Account discount_Given_Expense_Account, Account receivableAccount)
        {
            if (this.ReceivableAccount == null)
            {
                this.ReceivableAccount = receivableAccount;
                this.ReceivableAccountId = this.ReceivableAccount.Id;
            }

            if (this.Discount_Given_Expense_Account == null && this.Discount > 0)
            {
                this.Discount_Given_Expense_Account = discount_Given_Expense_Account;
                this.Discount_Given_Expense_AccountId = this.Discount_Given_Expense_Account.Id;
            }

            if (this.IncomeAccount == null)
            {
                this.IncomeAccount = incomeAccount;
                this.IncomeAccountId = this.IncomeAccount?.Id;
            }

        }

        public void AddItem(Item item)
        {
            var saleItem = new SaleItem()
            {
                ItemId = item.Id,
                PartNumber = item.PartNumber,
                Description = item.Description,
                Price = item.SalePrice,
                Quantity = 1,
                Order = this.Items.Count() + 1,
            };
            this.Items.Add(saleItem);
        }

        public void UpdateItems()
        {
            var removeItems = this.Items
                 .Where(i => i.Quantity == 0)
                 .ToList();

            foreach (var item in removeItems)
            {
                this.Items.Remove(item);
            }

            this.Reorder();
            this.UpdateBalance();
        }

        public void UpdateName()
        {
            var currentYear = this.Date.Year;
            var currentMonth = this.Date.Month;

            this.Name = $"SL-{currentYear}/{currentMonth}/{this.No.ToString()}";
        }
    }
}
