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
    public class SaleQuoteItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid QuoteId { get; set; }
        [ForeignKey("QuoteId")]
        public virtual SaleQuote Quote { get; set; }

        public Guid ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Items.Item Item { get; set; }
        public int Order { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public Decimal DiscountPercent { get; set; }
        public Decimal PriceAfterDiscount => this.Price * (((decimal)100 - DiscountPercent) / (decimal)100);
        public String? Memo { get; set; }
        public String? Serial { get; set; }
        public Decimal LineTotal => Quantity * PriceAfterDiscount;








        public String? PartNumber { get; set; }
        public String? Description { get; set; }
        public String? Group { get; set; } = string.Empty;

        public SaleQuoteItem()
        {

        }

        internal SaleItem GetSaleItem()
        {
            return new SaleItem()
            {
                ItemId = this.ItemId,
                PartNumber = this.PartNumber,
                Description = this.Description,
                Memo = this.Memo,
                Price = this.Price,
                Quantity = this.Quantity,
                Order = this.Order,
                DiscountPercent = this.DiscountPercent
            };
        }
    }
}
