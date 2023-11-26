using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Taxes;
using ERPKeeper.Node.Models.Transactions.Commercials;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace ERPKeeper.Node.Models.Estimations
{
    [Table("ERP_Quotes_Items")]
    public partial class QuoteItem
    {
        [Key]
        public Guid Uid { get; set; }


        [Column("QuoteId")]
        public Guid? QuoteId { get; set; }
        [ForeignKey("QuoteId")]
        public virtual Quote Quote { get; set; }


        [Index]
        public Guid? ItemGuid { get; set; }
        [ForeignKey("ItemGuid")]
        public virtual Items.Item Item { get; set; }

        public Guid? ItemBrandId { get; set; }
        

        public String ItemPartNumber { get; set; }
        [MaxLength(2048)]
        public String ItemDescription { get; set; }

        public int Order { get; set; }
        public String Memo { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }



        public Decimal DiscountPercent { get; set; }
        public Decimal UnitPriceAfterDiscount => this.UnitPrice * (((decimal)100 - DiscountPercent) / (decimal)100);
        public decimal LineTotal => this.UnitPriceAfterDiscount * this.Amount;

        public void Update(QuoteItem estimateItem)
        {
            this.Amount = estimateItem.Amount;
            this.ItemDescription = estimateItem.ItemDescription;
            this.ItemPartNumber = estimateItem.ItemPartNumber;
            this.UnitPrice = estimateItem.UnitPrice;
            this.DiscountPercent = estimateItem.DiscountPercent;
            this.Order = estimateItem.Order;
            this.Memo = estimateItem.Memo;
        }
    }
}





