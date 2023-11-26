using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using ERPKeeper.Node.Models.Accounting.Enums;

namespace ERPKeeper.Node.Models.Transactions
{

    [Table("ERP_Transactions_Commercial_Histories")]
    public class CommercialDailyBalance
    {
        [Key]
        public Guid Uid { get; set; }

        [Column("TrnDate")]
        public DateTime TrnDate { get; set; }
        public ERPObjectType Type { get; set; }
        public decimal Balance { get; set; }


        public CommercialDailyBalance()
        {
            Uid = Guid.NewGuid();
        }
    }
}
