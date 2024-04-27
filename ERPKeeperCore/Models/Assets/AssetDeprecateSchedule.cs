using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel;
using ERPKeeperCore.Enterprise.Models.Enums;

namespace ERPKeeperCore.Enterprise.Models.Assets
{

    public class AssetDeprecateSchedule
    {
        [Key]
        public Guid Id { get; set; }
        public int No { get; set; }

        public Guid? AssetId { get; set; }
        [ForeignKey("AssetId")]
        public virtual Asset Asset { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalDays => Math.Abs((EndDate - StartDate).TotalDays) + 1;

        public DateTime TrnDate => EndDate;
        public String? Name => string.Format("{0}/{1}", TrnDate.ToString("yyMM"), No.ToString().PadLeft(3, '0'));



        public Decimal DepreciationValue { get; set; }


        public Decimal DepreciateOffsetValue { get; set; }


        public Decimal DepreciateAccumulation { get; set; }


        public Decimal RemainValue { get; set; }


        public Decimal OpeningValue { get; set; }


        public LedgerPostStatus PostStatus { get; set; }

        public AssetDeprecateSchedule()
        {

        }

    }
}
