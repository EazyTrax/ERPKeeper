using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ERPKeeper.Node.Models.Accounting;
using System.ComponentModel;

namespace ERPKeeper.Node.Models.Assets
{
    [Table("ERP_FixedAsset_Types")]
    public class FixedAssetType
    {
        [Key]
        public Guid Uid { get; set; }
        public String Name { get; set; }
        public String CodePrefix { get; set; }
        public bool DeprecatedAble { get; set; }



        [Column(TypeName = "Money")]
        public Decimal ScrapValue { get; set; }

        [DefaultValue(5)]
        public int UseFulLifeYear { get; set; }

        public virtual ICollection<FixedAsset> FixedAssets { get; set; }

        public int AssetCount { get; set; }
        public decimal AssetValue { get; set; }


        public String Description { get; set; }
        public EnumDepreciationMethod DepreciationMethod { get; set; }


        [Column("AwaitDeprecateAccUid")]
        public Guid? AwaitDeprecateAccUid { get; set; }
        [ForeignKey("AwaitDeprecateAccUid")]
        public virtual AccountItem AwaitDeprecateAccount { get; set; }



        [Column("PurchaseAccUid")]
        public Guid? PurchaseAccUid { get; set; }
        [ForeignKey("PurchaseAccUid")]
        public virtual AccountItem AssetAccount { get; set; }

        [Column("AmortizeExpenseAccUid")]
        public Guid? AmortizeExpenseAccUid { get; set; }
        [ForeignKey("AmortizeExpenseAccUid")]
        public virtual AccountItem AmortizeExpenseAccount { get; set; }

        [Column("AccumulateDeprecateAccUid")]
        public Guid? AccumulateDeprecateAccUid { get; set; }
        [ForeignKey("AccumulateDeprecateAccUid")]
        public virtual AccountItem AccumulateDeprecateAcc { get; set; }

        public FixedAssetType()
        {
            Uid = Guid.NewGuid();
        }
        public void Refresh()
        {
            AssetCount = FixedAssets?.Count() ?? 0;
            AssetValue = FixedAssets?.Select(f => f.AssetValue).DefaultIfEmpty(0).Sum() ?? 0;
        }

        public void Update(FixedAssetType model)
        {
            this.Name = model.Name;
            this.CodePrefix = model.CodePrefix;
            this.DeprecatedAble = model.DeprecatedAble;

            this.Description = model.Description;
            this.AwaitDeprecateAccUid = model.AwaitDeprecateAccUid;
            this.PurchaseAccUid = model.PurchaseAccUid;

            if (model.DeprecatedAble)
            {
                this.UseFulLifeYear = model.UseFulLifeYear;
                this.AccumulateDeprecateAccUid = model.AccumulateDeprecateAccUid;
                this.AmortizeExpenseAccUid = model.AmortizeExpenseAccUid;
            }
        }
    }
}
