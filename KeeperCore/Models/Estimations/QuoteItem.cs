using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Taxes;
using KeeperCore.ERPNode.Models.Transactions.Commercials;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace KeeperCore.ERPNode.Models.Estimations
{
    [Table("ERP_Quotes_Items")]
    public partial class QuoteItem
    {
        [Key]
        public Guid Id { get; set; }


        public Guid? QuoteId { get; set; }
        [ForeignKey("QuoteId")]
        public virtual Quote Quote { get; set; }



        public Guid? ItemId { get; set; }
        [ForeignKey("ItemId")]
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





