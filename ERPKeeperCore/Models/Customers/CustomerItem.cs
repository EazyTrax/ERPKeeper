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
    public class CustomerItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public Guid ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Items.Item Item { get; set; }

        public int Order { get; set; }
        public int AmountSaleQuote { get; set; }
        public int AmountOrdered { get; set; }

        public int AmountSale { get; set; }

        public DateTime LastUpdated { get; set; }


        
        public CustomerItem(Guid itemId,Guid customerId)
        {
            ItemId = itemId;
            CustomerId = customerId;
        }
    }
}
