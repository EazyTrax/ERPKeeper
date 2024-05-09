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
    public class PurchaseItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? PurchaseId { get; set; }
        [ForeignKey("PurchaseId")]
        public virtual Purchase Purchase { get; set; }

        public Guid ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Items.Item Item { get; set; }



        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public Decimal DiscountPercent { get; set; }
        public Decimal PriceAfterDiscount => this.Price * (((decimal)100 - DiscountPercent) / (decimal)100);
        public Decimal LineTotal => Quantity * PriceAfterDiscount;



        public String? PartNumber { get; set; }
        public String? Description { get; set; }
        public String? Memo { get; set; }
        public String? Serial { get; set; }





        public PurchaseItem()
        {

        }
    }
}
