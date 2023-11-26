using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models.Items
{
    public class AssistItem
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }

        public Guid? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profiles.Profile Profile { get; set; }

        public Guid? ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

        public decimal Price { get; set; }
        public int Count { get; set; }
        public decimal Chance { get; set; }
        public string ItemPartNumber { get; set; }
    }
}