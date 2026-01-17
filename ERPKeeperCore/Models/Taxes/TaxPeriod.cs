using ERPKeeperCore.Enterprise.Helpers;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Suppliers;
using ERPKeeperCore.Enterprise.Models.Taxes.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;


namespace ERPKeeperCore.Enterprise.Models.Taxes
{

    public enum TaxPeriodStatus
    {
        Draft = 0,
        Active = 1
    }

    public class TaxPeriod
    {
        [Key]
        public Guid Id { get; set; }



        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }

        public TaxPeriodStatus Status { get; set; }

        public int? No { get; set; }
        public Guid? FiscalYearId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate => StartDate.AddMonths(1).AddMinutes(-1);
        public String? Name => "TP-" + StartDate.ToString("yyyy/MM");


        public Guid? CloseToAccountId { get; set; }
        [ForeignKey("CloseToAccountId")]
        public virtual Account CloseToAccount { get; set; }
        public Guid? LiabilityPaymentId { get; set; }
        public String? Memo { get; set; }
        public LedgerPostStatus PostStatus { get; set; }
        public int CommercialsCount => SalesCount + PurchasesCount;

        //Sales Tax
        public int SalesCount { get; set; }

        [Precision(18, 2)]
        public Decimal SalesBalance { get; set; }

        [Precision(18, 2)]
        public Decimal SalesTaxBalance { get; set; }


        public Guid? SaleTaxAccountId { get; set; }
        [ForeignKey("SaleTaxAccountId")]
        public virtual Account? SaleTaxAccount { get; set; }



        [Precision(18, 2)]
        public Decimal PuchasesBalance { get; set; }
        [Precision(18, 2)]
        public Decimal PurchasesTaxBalance { get; set; }


        public Guid? PurchaseTaxAccountId { get; set; }
        [ForeignKey("PurchaseTaxAccountId")]
        public virtual Account? PurchaseTaxAccount { get; set; }




        public int PurchasesCount { get; set; }

        public Decimal ClosingAmount => PurchasesTaxBalance - SalesTaxBalance;

        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }


        public void UpdateBalance()
        {
            // Handle Sales calculations with null checks
            SalesTaxBalance = Sales?.Select(t => t.Tax)?.Sum() ?? 0;
            SalesBalance = Sales?.Select(t => t.LinesTotalAfterDiscount)?.Sum() ?? 0;
            SalesCount = Sales?.Count ?? 0;

            if (this.SaleTaxAccount == null)
                this.SaleTaxAccount = Sales?.FirstOrDefault()?.TaxCode?.OutputTaxAccount;

            // Handle Purchases calculations with null checks
            PurchasesTaxBalance = Purchases?.Select(t => t.Tax)?.Sum() ?? 0;
            PuchasesBalance = Purchases?.Select(t => t.LinesTotalAfterDiscount)?.Sum() ?? 0;
            PurchasesCount = Purchases?.Count ?? 0;

            if (this.PurchaseTaxAccount == null)
                this.PurchaseTaxAccount = Purchases?.FirstOrDefault()?.TaxCode?.InputTaxAccount;
        }

        public void PostToTransaction()
        {
            Console.WriteLine($">Post > TaxPeriods:{this.Name}");
            this.UpdateBalance();

            if (this.Transaction == null)
                return;
            if (this.CloseToAccountId == null)
                return;
            Console.WriteLine($">Post > ###");



            this.Transaction.ClearLedger();
            this.Transaction.Date = this.EndDate;
            this.Transaction.Name = this.Name;
            this.Transaction.PostedDate = DateTime.Today;

            // Dr.
            if (this.PurchasesTaxBalance > 0)
                this.Transaction.AddCredit(this.PurchaseTaxAccount, this.PurchasesTaxBalance);
            // Cr.
            if (this.SalesTaxBalance > 0)
                this.Transaction.AddDebit(this.SaleTaxAccount, this.SalesTaxBalance);

            if (ClosingAmount > 0 && this.CloseToAccount.Type == AccountTypes.Asset)
                this.Transaction.AddDebit(this.CloseToAccount, Math.Abs(this.ClosingAmount));
            else if (ClosingAmount < 0 && this.CloseToAccount.Type == AccountTypes.Liability)
                this.Transaction.AddCredit(this.CloseToAccount, Math.Abs(this.ClosingAmount));
            else if (ClosingAmount == 0)
            {

            }
            else
            {
                this.Transaction.ClearLedger();
                return;
            }



            IsPosted = this.Transaction.UpdateBalance();
        }

        public void UnPostLedger()
        {
            Console.WriteLine($">UnPost  TP:{this.Name}");

            if (Transaction == null)
            {
                this.IsPosted = false;
                return;
            }
            else
            {
                this.Transaction.ClearLedger();
                this.Transaction.Date = this.EndDate;
                this.Transaction.Name = this.Name;
                this.IsPosted = false;
            }
        }
    }
}
