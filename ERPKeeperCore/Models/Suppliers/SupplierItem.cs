using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ERPKeeperCore.Enterprise.Models.Suppliers
{
    public class SupplierItem
    {


        public SupplierItem(Guid itemId, Guid supplierId)
        {
            ItemId = itemId;
            SupplierId = supplierId;
        }

        [Key]
        public Guid Id { get; set; }

        public Guid SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }

        public Guid ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Items.Item Item { get; set; }
        public int Order { get; set; }
        public int AmountPurchaseQuote { get; set; }
        public int AmountOrdered { get; set; }
        public int AmountPurchase { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
