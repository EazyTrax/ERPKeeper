using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Suppliers;
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
    public class ReceivePayment
    {
        [Key]
        public Guid Id { get; set; }
        public PaymentStatus Status { get; set; }

        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }

        public Guid? SaleId { get; set; }
        [ForeignKey("SaleId")]
        public virtual Sale? Sale { get; set; }

        public String? Reference { get; set; }
        public String? Memo { get; set; }
        public int No { get; set; }
        public String Name => $"RP/{this.Date.Year % 100}-{this.Date.Month}-{No}";
        public DateTime Date { get; set; } = DateTime.Today;


        public Decimal Amount { get; set; }
        public Decimal AmountExcludeTax { get; set; }
        public Decimal AmountDiscount { get; set; }
        public Decimal AmountTotal => Amount - AmountDiscount;

        public Decimal AmountRetention { get; set; }
        public Decimal AmountTotalReceive => AmountTotal - AmountRetention;


        public Decimal AmountBankFee { get; set; }


        public Guid? PayToAccountId { get; set; }
        [ForeignKey("PayToAccountId")]
        public virtual Accounting.Account? PayToAccount { get; set; }

        public Guid? Receivable_Asset_AccountId { get; set; }
        [ForeignKey("Receivable_Asset_AccountId")]
        public virtual Accounting.Account? Receivable_Asset_Account { get; set; }


        public Guid? RetentionTypeId { get; set; }
        [ForeignKey("RetentionTypeId")]
        public virtual Financial.RetentionType? RetentionType { get; set; }

        public Guid? RetentionGroupId { get; set; }
        [ForeignKey("RetentionGroupId")]
        public virtual Financial.RetentionPeriod? RetentionGroup { get; set; }



        public Guid? Discount_Given_Expense_AccountId { get; set; }
        [ForeignKey("DiscountAccountId")]
        public virtual Accounting.Account? Discount_Given_Expense_Account { get; set; }

        public Guid? BankFee_Expense_AccountId { get; set; }
        [ForeignKey("BankFee_Expense_AccountId")]
        public virtual Accounting.Account? BankFee_Expense_Account { get; set; }


        public void PostToTransaction()
        {
            Console.WriteLine($">Post  RP:{this.Name}");

            if (this.PayToAccountId == null)
            {
                Console.WriteLine($">> Fail >> No pay to Account");
                return;
            }
            if (this.Transaction == null)
                return;

            this.Transaction.ClearLedger();
            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;

            // Dr.
            this.Transaction.AddDebit(this.PayToAccount, this.AmountTotalReceive);
            if (this.AmountDiscount > 0)
                this.Transaction.AddDebit(this.Discount_Given_Expense_Account, this.AmountDiscount);
            if (this.RetentionTypeId != null)
                this.Transaction.AddDebit(this.RetentionType.RetentionToAccount, this.AmountRetention);

            // Cr.
            this.Transaction.AddCredit(this.Receivable_Asset_Account, this.Amount);



            if (this.AmountBankFee > 0 && this.BankFee_Expense_Account != null)
            {
                this.Transaction.AddDebit(this.BankFee_Expense_Account, this.AmountBankFee);

                this.Transaction.AddCredit(this.PayToAccount, this.AmountBankFee);
            }



            IsPosted = this.Transaction.UpdateBalance();
        }

        public void UpdateBalance()
        {
            this.Amount = this.Sale.Total;
            this.AmountRetention = this.RetentionType?.GetRetentionAmount(this.Sale.LinesTotalAfterDiscount) ?? 0;
        }

        public void UnPostLedger()
        {
            Console.WriteLine($">UnPost  SP:{this.Name}");

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

        public string GetThaiRDReport(int order)
        {
            string date = this.Date.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
            string profile = this.Sale?.Customer?.Profile?.Name ?? "NA";
            string taxId = this.Sale?.Customer?.Profile?.TaxNumber ?? "NA";

            string address = this.Sale?.Customer?.Profile?.Addresses?.FirstOrDefault()?.AddressLine ?? "NA";
            if (address != null && address.Length > 30)
            {
                address = address.Substring(0, 28);
                address = address.Replace(Environment.NewLine, "");
            }

            string branchNo = this.Sale?.Customer?.Profile?.Addresses?.FirstOrDefault()?.Number ?? "NA";


            string retentionStr = "";
            retentionStr += $"{this.RetentionType?.Name}";
            retentionStr += $"|{order}";
            retentionStr += $"|{taxId}";
            retentionStr += $"|{branchNo}";
            retentionStr += $"|{this.Sale?.Customer?.Profile?.Name ?? "NA"}";//5
            retentionStr += $"|{address}";
            retentionStr += $"|{date}";
            retentionStr += $"|{this.RetentionType?.PaymentType}";
            retentionStr += $"|{this.RetentionType?.Rate}";
            retentionStr += $"|{this.Sale.LinesTotalAfterDiscount}";
            retentionStr += $"|{this.AmountRetention}";//15
            retentionStr += $"|1";
            return retentionStr;
        }


  

        public ReceivePayment()
        {

        }
    }
}
