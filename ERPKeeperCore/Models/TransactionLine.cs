using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace KeeperCore.ERPNode.Models
{
    public class TransactionLine
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }


        public Guid ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Items.Item Item { get; set; }

        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public string Memo { get; set; }
        public decimal LineTotal => Amount * UnitPrice;



        public TransactionLine()
        {

        }
    }
}
