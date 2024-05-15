using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


using System.Globalization;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Taxes.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Suppliers;
using ERPKeeperCore.Enterprise.Helpers;


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
        public DateTime EndDate => StartDate.AddMonths(1).AddDays(-1);
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
        public Decimal SalesBalance { get; set; }
        public Decimal SalesTaxBalance { get; set; }
        public Guid? SaleTaxAccountId { get; set; }
        [ForeignKey("SaleTaxAccountId")]
        public virtual Account? SaleTaxAccount { get; set; }



        //Purchhases Tax
        public Decimal PuchasesBalance { get; set; }
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
            SalesTaxBalance = Sales.Select(t => t.Tax).DefaultIfEmpty(0).Sum();
            SalesBalance = Sales.Select(t => t.LinesTotalAfterDiscount).DefaultIfEmpty(0).Sum();
            SalesCount = Sales.Count;

            if (this.SaleTaxAccount == null)
                this.SaleTaxAccount = Sales.FirstOrDefault()?.TaxCode?.OutputTaxAccount;


            PurchasesTaxBalance = Purchases.Select(t => t.Tax).DefaultIfEmpty(0).Sum();
            PuchasesBalance = Purchases.Select(t => t.LinesTotalAfterDiscount).DefaultIfEmpty(0).Sum();
            PurchasesCount = Purchases.Count;

            if (this.PurchaseTaxAccount == null)
                this.PurchaseTaxAccount = Purchases.FirstOrDefault()?.TaxCode?.InputTaxAccount;
        }

        public void PostToTransaction()
        {
            Console.WriteLine($">Post > TaxPeriods:{this.Name}");
            this.UpdateBalance();

            if (this.Transaction == null)
                return;

            this.Transaction.ClearLedger();
            this.Transaction.Date = this.EndDate;
            this.Transaction.Name = this.Name;
            this.Transaction.PostedDate = DateTime.Now;

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



            this.Transaction.UpdateBalance();
            this.IsPosted = true;
        }
    }
}
