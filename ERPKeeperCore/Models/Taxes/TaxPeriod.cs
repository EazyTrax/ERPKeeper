using KeeperCore.ERPNode.Models.Accounting.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using KeeperCore.ERPNode.Models.Transactions;
using KeeperCore.ERPNode.Models.Taxes.Enums;

using System.Globalization;
using KeeperCore.ERPNode.Models.Accounting;


namespace KeeperCore.ERPNode.Models.Taxes
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
        public virtual ICollection<Transaction> Commercials { get; set; }

        public List<CommercialTaxGroup> GetCommercialTaxGroups()
        {
            var commercialTaxGroups = this.Commercials
                .GroupBy(c => c.TaxCodeId)
                .Select(go => new CommercialTaxGroup()
                {
                    TaxCodeId = go.Key,
                    TaxCode = go.Select(i => i.TaxCode).FirstOrDefault(),
                    TaxBalance = go.Sum(i => i.Tax),
                    CommercialCount = go.Count(),
                    Commercials = go.ToList()
                })
                .ToList();

            return commercialTaxGroups;
        }

        public int CommercialsCount => InputCommercialCount + OutputCommercialCount;
        public int InputCommercialCount { get; private set; }
        public decimal InputBalance { get; private set; }
        public decimal InputTaxBalance { get; private set; }


        public decimal OutputBalance { get; private set; }
        public decimal OutputTaxBalance { get; private set; }
        public int OutputCommercialCount { get; private set; }

        public decimal ClosingAmount => this.InputTaxBalance - this.OutputTaxBalance;


        public List<Transaction> GetCommercials(TaxDirection taxDirection) =>
            this.Commercials
                .Where(c => c.TaxCodeId != null && c.TaxCode.TaxDirection == taxDirection)
                .ToList();



        public KeeperCore.ERPNode.Reports.Taxes.InputTax GetInputTaxReport()
        {
            var report = new KeeperCore.ERPNode.Reports.Taxes.InputTax()
            {
                DataSource = this.GetCommercials(TaxDirection.Input),
                Name = this.Name
            };

            report.Parameters["Title_Line1"].Value = $"รายงานภาษีซื้อ";
            report.Parameters["Title_Line1"].Visible = false;

            string fullMonthName = this.PeriodStartDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("th"));
            string fullYearName = this.PeriodStartDate.ToString("yyyy", CultureInfo.CreateSpecificCulture("th"));

            report.Parameters["Title_Line2"].Value = $"เดือนภาษี {fullMonthName} ปี {fullYearName}";
            report.Parameters["Title_Line2"].Visible = false;

            //report.Parameters["Title_Line3"].Value = $"{organization.DataItems.OrganizationName} เลขประจำผู้เสียภาษีอากร {organization.DataItems.TaxID}";
            //report.Parameters["Title_Line3"].Visible = false;

            report.Parameters["Title_Line4"].Value = "สาขา สำนักงานใหญ่ เลขที่ 00000";
            report.Parameters["Title_Line4"].Visible = false;

            return report;
        }

        public KeeperCore.ERPNode.Reports.Taxes.OutputTax GetOutputTaxReport()
        {
            var report = new KeeperCore.ERPNode.Reports.Taxes.OutputTax()
            {
                DataSource = this.GetCommercials(TaxDirection.Output),
                Name = this.Name
            };

            report.Parameters["Title_Line1"].Value = $"รายงานภาษีขาย";
            report.Parameters["Title_Line1"].Visible = false;

            string fullMonthName = this.PeriodStartDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("th"));
            string fullYearName = this.PeriodStartDate.ToString("yyyy", CultureInfo.CreateSpecificCulture("th"));

            report.Parameters["Title_Line2"].Value = $"เดือนภาษี {fullMonthName} ปี {fullYearName}";
            report.Parameters["Title_Line2"].Visible = false;

            //report.Parameters["Title_Line3"].Value = $" เลขประจำผู้เสียภาษีอากร {organization.DataItems.TaxID}";
            //report.Parameters["Title_Line3"].Visible = false;

            report.Parameters["Title_Line4"].Value = "สาขา สำนักงานใหญ่ เลขที่ 00000";
            report.Parameters["Title_Line4"].Visible = false;

            return report;
        }


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


            //int order = 0;
            //InputCommercial.OrderBy(c => c.TrnDate).ThenBy(c => c.NoInMonth).ToList().ForEach(c => c.TaxPeriodOrder = ++order);
            //order = 0;
            //OutputCommercial.OrderBy(c => c.TrnDate).ThenBy(c => c.NoInMonth).ToList().ForEach(c => c.TaxPeriodOrder = ++order);
        }
    }
}
