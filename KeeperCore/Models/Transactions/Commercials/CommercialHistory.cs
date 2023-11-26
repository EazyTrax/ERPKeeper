using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using KeeperCore.ERPNode.Models.Accounting.Enums;

namespace KeeperCore.ERPNode.Models.Transactions
{

    [Table("ERP_Transactions_Commercial_Histories")]
    public class CommercialDailyBalance
    {
        [Key]
        public Guid Id { get; set; }

        
        public DateTime TrnDate { get; set; }
        public ERPObjectType Type { get; set; }
        public decimal Balance { get; set; }


        public CommercialDailyBalance()
        {
            
        }
    }
}
