﻿using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPKeeper.Node.Models.Items
{
    [Table("ERP_Items_KitMembers")]
    public class KitMember
    {
        [Key]
        public Guid Uid { get; set; }



        [Column("ParentGuid")]
        public Guid ParentGuid { get; set; }
        [ForeignKey("ParentGuid")]
        public Item Parent { get; set; }



        public Guid ItemGuid { get; set; }




        public int Amount { get; set; }

    }
}
