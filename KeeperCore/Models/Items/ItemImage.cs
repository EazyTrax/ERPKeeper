using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KeeperCore.ERPNode.Models.Items
{
    [Table("ERP_Items_Images")]
    public class ItemImage
    {
        [Key]
        public Guid Id { get; set; }
        public byte[] Image { get; set; }
        public Guid ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
        public string Name { get; set; }
    }
}