using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KeeperCore.ERPNode.Models.Items
{
    public partial class Item
    {
        [Column("FixedAssetTypeId")]
        public Guid? FixedAssetTypeId { get; set; }
        [ForeignKey("FixedAssetTypeId")]
        public virtual Assets.FixedAssetType FixedAssetType { get; set; }
    }
}