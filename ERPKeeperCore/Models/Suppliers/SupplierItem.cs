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
        [Key]
        public Guid Id { get; set; }

        public Guid SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }

        public Guid ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Items.Item Item { get; set; }

        public int AmountQuote { get; set; }
        public int AmountOrdered { get; set; }
        public int AmouuntPurchase { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
