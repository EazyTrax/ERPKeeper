﻿using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeper.Node.Models.Items
{



    [Table("ERP_Items_PriceRanges")]
    public class PriceRange
    {
        [Key]
        public Guid Uid { get; set; }

        public String Name { get; set; }

        public virtual ICollection<PriceRangeItem> RangeItems { get; set; }

        public virtual ICollection<Item> Items { get; set; }

    }
}