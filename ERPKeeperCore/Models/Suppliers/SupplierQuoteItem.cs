﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ERPKeeperCore.Enterprise.Models.Suppliers
{
    public class SupplierQuoteItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? SupplierQuoteId { get; set; }
        [ForeignKey("SupplierQuoteId")]
        public virtual SupplierQuote SupplierQuote { get; set; }

        public Guid ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Items.Item Item { get; set; }



        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public String? Memo { get; set; }
        public String? Serial { get; set; }
        public Decimal LineTotal => Quantity * Price;

        public String? PartNumber { get; set; }
        public String? Description { get; set; }







        public SupplierQuoteItem()
        {

        }
    }
}