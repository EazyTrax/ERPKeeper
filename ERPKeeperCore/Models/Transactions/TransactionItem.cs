using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace KeeperCore.ERPNode.Models.Transactions
{
    public class TransactionItem
    {
        [Key]
        public Guid Id { get; set; }

        [Column("ItemId")]
        public Guid ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual KeeperCore.ERPNode.Models.Items.Item Item { get; set; }

        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }

        public int Amount { get; set; }
        public Decimal UnitPrice { get; set; }
        public string Memo { get; set; }
        public Decimal LineTotal => Amount * UnitPrice;

        public TransactionItem()
        {

        }
    }
}
