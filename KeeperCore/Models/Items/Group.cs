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
        public Guid? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Item Parent { get; set; }
        public virtual ICollection<Item> Child { get; set; }
     
    }
}