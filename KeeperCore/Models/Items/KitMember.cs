using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeeperCore.ERPNode.Models.Items
{
    [Table("ERP_Items_KitMembers")]
    public class KitMember
    {
        [Key]
        public Guid Id { get; set; }



        [Column("ParentId")]
        public Guid ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Item Parent { get; set; }



        public Guid ItemId { get; set; }




        public int Amount { get; set; }

    }
}
