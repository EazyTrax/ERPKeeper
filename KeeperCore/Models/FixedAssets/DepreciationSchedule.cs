using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using System.ComponentModel;
using KeeperCore.ERPNode.Models.Accounting.Enums;

namespace KeeperCore.ERPNode.Models.Assets
{
    [Table("ERP_FixedAssets_DeprecateSchedules")]
    public class DeprecateSchedule
    {
        [Key]
        public Guid Id { get; set; }
        public int No { get; set; }

        public Guid? FixedAssetId { get; set; }
        [ForeignKey("FixedAssetId")]
        public virtual FixedAsset FixedAsset { get; set; }


        public Guid? FiscalYearId { get; set; }
        [ForeignKey("FiscalYearId")]
        public virtual KeeperCore.ERPNode.Models.Accounting.FiscalYear FiscalYear { get; set; }

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
         
        }

    }
}
