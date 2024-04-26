using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


using System.Globalization;
using KeeperCore.ERPNode.Models.Enums;


namespace KeeperCore.ERPNode.Models
{
    public class TaxPeriod
    {
        [Key]
        public Guid Id { get; set; }
        public int? No { get; set; }
        public Guid? FiscalYearId { get; set; }
        public DateTime PeriodStartDate => new(TrnDate.Year, TrnDate.Month, 1);
        public DateTime TrnDate { get; set; }
        public string Name => "TP-" + this.TrnDate.ToString("yyyy/MM");
        public String PeriodName => this.TrnDate.ToString("yyyy/MM");

        public Guid? CloseToAccountId { get; set; }
        [ForeignKey("CloseToAccountId")]
        public virtual Account CloseToAccount { get; set; }

        public Guid? LiabilityPaymentId { get; set; }

        public String Memo { get; set; }
        public LedgerPostStatus PostStatus { get; set; }
        public virtual ICollection<Sale> Commercials { get; set; }


        public int CommercialsCount => InputCommercialCount + OutputCommercialCount;
        public int InputCommercialCount { get; private set; }
        public decimal InputBalance { get; private set; }
        public decimal InputTaxBalance { get; private set; }


        public decimal OutputBalance { get; private set; }
        public decimal OutputTaxBalance { get; private set; }
        public int OutputCommercialCount { get; private set; }

        public decimal ClosingAmount => this.InputTaxBalance - this.OutputTaxBalance;


        public List<Sale> GetCommercials(TaxDirection taxDirection) =>
            this.Commercials
                .Where(c => c.TaxCodeId != null && c.TaxCode.TaxDirection == taxDirection)
                .ToList();




        public void ReCalculate()
        {
            var InputCommercial = this.GetCommercials(TaxDirection.Input);
            this.InputTaxBalance = InputCommercial.Select(t => t.Tax).DefaultIfEmpty(0).Sum();
            this.InputBalance = InputCommercial.Select(t => t.LinesTotal).DefaultIfEmpty(0).Sum();
            this.InputCommercialCount = InputCommercial.Count;

            var OutputCommercial = this.GetCommercials(TaxDirection.Output);
            this.OutputTaxBalance = OutputCommercial.Select(t => t.Tax).DefaultIfEmpty(0).Sum();
            this.OutputBalance = OutputCommercial.Select(t => t.LinesTotal).DefaultIfEmpty(0).Sum();
            this.OutputCommercialCount = OutputCommercial.Count;

        }
    }
}
