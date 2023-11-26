using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeper.Node.Models.Items
{
    public partial class Item
    {
        [Column("FixedAssetTypeUid")]
        public Guid? FixedAssetTypeUid { get; set; }
        [ForeignKey("FixedAssetTypeUid")]
        public virtual Assets.FixedAssetType FixedAssetType { get; set; }
    }
}