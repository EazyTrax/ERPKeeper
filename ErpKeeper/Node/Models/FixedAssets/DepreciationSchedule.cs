using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ERPKeeper.Node.Models.Accounting;
using System.ComponentModel;
using ERPKeeper.Node.Models.Accounting.Enums;

namespace ERPKeeper.Node.Models.Assets
{
    [Table("ERP_FixedAssets_DeprecateSchedules")]
    public class DeprecateSchedule
    {
        [Key]
        public Guid Uid { get; set; }
        public int No { get; set; }

        public Guid? FixedAssetUid { get; set; }
        [ForeignKey("FixedAssetUid")]
        public virtual FixedAsset FixedAsset { get; set; }


        public Guid? FiscalYearUid { get; set; }
        [ForeignKey("FiscalYearUid")]
        public virtual ERPKeeper.Node.Models.Accounting.FiscalYear FiscalYear { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalDays => Math.Abs((EndDate - StartDate).TotalDays) + 1;

        public DateTime TrnDate => EndDate;
        public string Name => string.Format("{0}/{1}", this.TrnDate.ToString("yyMM"), this.No.ToString().PadLeft(3, '0'));


        [Column(TypeName = "money")]
        public Decimal DepreciationValue { get; set; }

        [Column(TypeName = "money")]
        public decimal DepreciateOffsetValue { get; set; }
        [Column(TypeName = "money")]
        public decimal DepreciateAccumulation { get; set; }
        public Decimal RemainValue { get; set; }
        public Decimal OpeningValue { get; set; }


        public LedgerPostStatus PostStatus { get; set; }

        public DeprecateSchedule()
        {
            this.Uid = Guid.NewGuid();
        }

    }
}
