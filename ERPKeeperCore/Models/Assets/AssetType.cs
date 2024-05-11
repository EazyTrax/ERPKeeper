using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Assets.Enums;

namespace ERPKeeperCore.Enterprise.Models.Assets
{

    public class AssetType
    {
        [Key]
        public Guid Id { get; set; }
        public String? Name { get; set; }
        public String? CodePrefix { get; set; }
        public bool DeprecatedAble { get; set; }




        public Decimal ScrapValue { get; set; }
        [DefaultValue(5)]
        public int UseFulLifeYear { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }

        public virtual int AssetCount => Assets?.Count() ?? 0;
        public virtual decimal AssetValue => Assets?.Select(f => f.AssetValue).DefaultIfEmpty(0).Sum() ?? 0;
        public String? Description { get; set; }
        public EnumDepreciationMethod DepreciationMethod { get; set; }


        public Guid? Purchase_Asset_AccountId { get; set; }
        [ForeignKey("Purchase_Asset_AccountId")]
        public virtual Account Purchase_Asset_Account { get; set; }


        [Column("AwaitDeprecateAccId")]
        public Guid? AwaitDeprecateAccId { get; set; }
        [ForeignKey("AwaitDeprecateAccId")]
        public virtual Account AwaitDeprecateAccount { get; set; }

        [Column("AmortizeExpenseAccId")]
        public Guid? AmortizeExpenseAccId { get; set; }
        [ForeignKey("AmortizeExpenseAccId")]
        public virtual Account AmortizeExpenseAccount { get; set; }

        [Column("AccumulateDeprecateAccId")]
        public Guid? AccumulateDeprecateAccId { get; set; }
        [ForeignKey("AccumulateDeprecateAccId")]
        public virtual Account AccumulateDeprecateAcc { get; set; }

        public AssetType()
        {

        }
        public void Update(AssetType model)
        {
            Name = model.Name;
            CodePrefix = model.CodePrefix;
            DeprecatedAble = model.DeprecatedAble;

            Description = model.Description;
            AwaitDeprecateAccId = model.AwaitDeprecateAccId;
            Purchase_Asset_AccountId = model.Purchase_Asset_AccountId;

            if (model.DeprecatedAble)
            {
                UseFulLifeYear = model.UseFulLifeYear;
                AccumulateDeprecateAccId = model.AccumulateDeprecateAccId;
                AmortizeExpenseAccId = model.AmortizeExpenseAccId;
            }
        }
    }
}
