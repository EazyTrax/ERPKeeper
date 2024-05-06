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


namespace ERPKeeperCore.Enterprise.Models.Taxes
{
    public class TaxPeriod
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }



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
        public int SalesCount { get; set; }
        public Decimal SalesBalance { get; set; }
        public Decimal SalesTaxBalance { get; set; }


        public Decimal PuchasesBalance { get; set; }
        public Decimal PurchasesTaxBalance { get; set; }
        public int PurchasesCount { get; set; }

        public Decimal ClosingAmount => PurchasesTaxBalance - SalesTaxBalance;

        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }


        public void UpdateBalance()
        {
            SalesTaxBalance = Sales.Select(t => t.Tax).DefaultIfEmpty(0).Sum();
            SalesBalance = Sales.Select(t => t.LinesTotal).DefaultIfEmpty(0).Sum();
            SalesCount = Sales.Count;


            PurchasesTaxBalance = Purchases.Select(t => t.Tax).DefaultIfEmpty(0).Sum();
            PuchasesBalance = Purchases.Select(t => t.LinesTotal).DefaultIfEmpty(0).Sum();
            PurchasesCount = Purchases.Count;
        }
    }
}
