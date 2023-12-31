﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace KeeperCore.ERPNode.Models
{
   
    public class AssetType
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String CodePrefix { get; set; }
        public bool DeprecatedAble { get; set; }



        [Column(TypeName = "Money")]
        public Decimal ScrapValue { get; set; }
        [DefaultValue(5)]
        public int UseFulLifeYear { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }

        public virtual int AssetCount => Assets?.Count() ?? 0;
        public virtual decimal AssetValue => Assets?.Select(f => f.AssetValue).DefaultIfEmpty(0).Sum() ?? 0;
        public String Description { get; set; }
        public EnumDepreciationMethod DepreciationMethod { get; set; }


        [Column("AwaitDeprecateAccId")]
        public Guid? AwaitDeprecateAccId { get; set; }
        [ForeignKey("AwaitDeprecateAccId")]
        public virtual Account AwaitDeprecateAccount { get; set; }



        [Column("PurchaseAccId")]
        public Guid? PurchaseAccId { get; set; }
        [ForeignKey("PurchaseAccId")]
        public virtual Account AssetAccount { get; set; }

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
            this.Name = model.Name;
            this.CodePrefix = model.CodePrefix;
            this.DeprecatedAble = model.DeprecatedAble;

            this.Description = model.Description;
            this.AwaitDeprecateAccId = model.AwaitDeprecateAccId;
            this.PurchaseAccId = model.PurchaseAccId;

            if (model.DeprecatedAble)
            {
                this.UseFulLifeYear = model.UseFulLifeYear;
                this.AccumulateDeprecateAccId = model.AccumulateDeprecateAccId;
                this.AmortizeExpenseAccId = model.AmortizeExpenseAccId;
            }
        }
    }
}
