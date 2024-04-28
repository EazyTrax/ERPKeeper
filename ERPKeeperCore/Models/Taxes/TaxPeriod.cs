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


namespace ERPKeeperCore.Enterprise.Models.Taxes
{
    public class TaxPeriod
    {
        [Key]
        public Guid Id { get; set; }
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



        public int CommercialsCount => InputCommercialCount + OutputCommercialCount;
        public int InputCommercialCount { get; set; }
        public Decimal InputBalance { get; set; }
        public Decimal InputTaxBalance { get; set; }


        public Decimal OutputBalance { get; set; }
        public Decimal OutputTaxBalance { get; set; }
        public int OutputCommercialCount { get; set; }

        public Decimal ClosingAmount => InputTaxBalance - OutputTaxBalance;


        //public List<Sale> GetCommercials(TaxDirection taxDirection) =>
        //    Commercials
        //        .Where(c => c.TaxCodeId != null && c.TaxCode.TaxDirection == taxDirection)
        //        .ToList();




        public void ReCalculate()
        {
            //var InputCommercial = GetCommercials(TaxDirection.Input);
            //InputTaxBalance = InputCommercial.Select(t => t.Tax).DefaultIfEmpty(0).Sum();
            //InputBalance = InputCommercial.Select(t => t.LinesTotal).DefaultIfEmpty(0).Sum();
            //InputCommercialCount = InputCommercial.Count;

            //var OutputCommercial = GetCommercials(TaxDirection.Output);
            //OutputTaxBalance = OutputCommercial.Select(t => t.Tax).DefaultIfEmpty(0).Sum();
            //OutputBalance = OutputCommercial.Select(t => t.LinesTotal).DefaultIfEmpty(0).Sum();
            //OutputCommercialCount = OutputCommercial.Count;

        }
    }
}
