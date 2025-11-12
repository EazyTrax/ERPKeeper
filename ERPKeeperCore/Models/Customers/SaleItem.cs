using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ERPKeeperCore.Enterprise.Models.Customers
{
    public class SaleItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid SaleId { get; set; }
        [ForeignKey("SaleId")]
        public virtual Sale Sale { get; set; }

        public Guid ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Items.Item Item { get; set; }

        public String? Group { get; set; } = string.Empty;


        public int Order { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public Decimal Price { get; set; }
        public String? Memo { get; set; }
        public String? Serial { get; set; }

        public Decimal DiscountPercent { get; set; }
        public Decimal PriceAfterDiscount => this.Price * (((decimal)100 - DiscountPercent) / (decimal)100);
        public Decimal LineTotal => Quantity * PriceAfterDiscount;

        public String? PartNumber { get; set; }
        public String? Description { get; set; }

        public SaleItem()
        {

        }
    }
}
