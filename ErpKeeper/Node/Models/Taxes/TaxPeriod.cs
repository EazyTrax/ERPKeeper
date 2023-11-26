using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ERPKeeper.Node.Models.Transactions;
using ERPKeeper.Node.Models.Taxes.Enums;
using ERPKeeper.Node.Reports.Taxes;
using System.Globalization;
using ERPKeeper.Node.DAL;

namespace ERPKeeper.Node.Models.Taxes
{
    [NotMapped]
    public class CommercialTaxGroup
    {
        public Guid? TaxCodeUid { get; internal set; }
        public TaxCode TaxCode { get; internal set; }
        public decimal TaxBalance { get; internal set; }
        public int CommercialCount { get; internal set; }
        public List<Commercial> Commercials { get; internal set; }
    }

    [Table("ERP_Taxes_Periods")]
    public class TaxPeriod
    {
        [Key]
        public Guid Uid { get; set; }
        public int? No { get; set; }
        public const ERPObjectType TransactionType = ERPObjectType.TaxPeriod;
        public Guid? FiscalYearUid { get; set; }
        public DateTime PeriodStartDate => new DateTime(TrnDate.Year, TrnDate.Month, 1);
        public DateTime TrnDate { get; set; }
        public string Name => "TP-" + this.TrnDate.ToString("yyyy/MM");
        public String PeriodName => this.TrnDate.ToString("yyyy/MM");

        public Guid? CloseToAccountGuid { get; set; }
        [ForeignKey("CloseToAccountGuid")]
        public virtual AccountItem CloseToAccount { get; set; }

        public Guid? LiabilityPaymentUid { get; set; }

        public String Memo { get; set; }
        public LedgerPostStatus PostStatus { get; set; }
        public virtual ICollection<Commercial> Commercials { get; set; }

        public List<CommercialTaxGroup> GetCommercialTaxGroups()
        {
            var commercialTaxGroups = this.Commercials
                .Where(c=>c.TaxCodeId!=null)
                .GroupBy(c => c.TaxCodeId)

                .Select(go => new CommercialTaxGroup()
                {
                    TaxCodeUid = go.Key,
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


        public List<Commercial> GetCommercials(TaxDirection taxDirection) =>
            this.Commercials
                .Where(c => c.TaxCodeId != null && c.TaxCode.TaxDirection == taxDirection)
                .ToList();



        public ERPKeeper.Node.Reports.Taxes.InputTax getInputTaxReport(Organization organization)
        {
            var report = new ERPKeeper.Node.Reports.Taxes.InputTax()
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

            report.Parameters["Title_Line3"].Value = $"{organization.DataItems.OrganizationName} เลขประจำผู้เสียภาษีอากร {organization.DataItems.TaxID}";
            report.Parameters["Title_Line3"].Visible = false;

            report.Parameters["Title_Line4"].Value = "สาขา สำนักงานใหญ่ เลขที่ 00000";
            report.Parameters["Title_Line4"].Visible = false;

            return report;
        }

        public ERPKeeper.Node.Reports.Taxes.OutputTax getOutputTaxReport(Organization organization)
        {
            var report = new ERPKeeper.Node.Reports.Taxes.OutputTax()
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

            report.Parameters["Title_Line3"].Value = $"{organization.DataItems.OrganizationName} เลขประจำผู้เสียภาษีอากร {organization.DataItems.TaxID}";
            report.Parameters["Title_Line3"].Visible = false;

            report.Parameters["Title_Line4"].Value = "สาขา สำนักงานใหญ่ เลขที่ 00000";
            report.Parameters["Title_Line4"].Visible = false;

            return report;
        }


        public void ReCalculate()
        {
            var InputCommercial = this.GetCommercials(TaxDirection.Input);
            this.InputTaxBalance = InputCommercial.Select(t => t.Tax).DefaultIfEmpty(0).Sum();
            this.InputBalance = InputCommercial.Select(t => t.LinesTotal).DefaultIfEmpty(0).Sum();
            this.InputCommercialCount = InputCommercial.Count();

            var OutputCommercial = this.GetCommercials(TaxDirection.Output);
            this.OutputTaxBalance = OutputCommercial.Select(t => t.Tax).DefaultIfEmpty(0).Sum();
            this.OutputBalance = OutputCommercial.Select(t => t.LinesTotal).DefaultIfEmpty(0).Sum();
            this.OutputCommercialCount = OutputCommercial.Count();


            int order = 0;
            InputCommercial.OrderBy(c => c.TrnDate).ThenBy(c => c.NoInMonth).ToList().ForEach(c => c.TaxPeriodOrder = ++order);
            order = 0;
            OutputCommercial.OrderBy(c => c.TrnDate).ThenBy(c => c.NoInMonth).ToList().ForEach(c => c.TaxPeriodOrder = ++order);
        }
    }
}
