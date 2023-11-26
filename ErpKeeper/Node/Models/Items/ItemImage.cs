using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeper.Node.Models.Items
{
    [Table("ERP_Items_Images")]
    public class ItemImage
    {
        [Key]
        public Guid Uid { get; set; }

        public byte[] Image { get; set; }

        public Guid ItemGuid { get; set; }
        [ForeignKey("ItemGuid")]
        public Item Item { get; set; }
    }
}