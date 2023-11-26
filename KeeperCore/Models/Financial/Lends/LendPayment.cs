using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace KeeperCore.ERPNode.Models.Financial.Lends
{
    [Table("ERP_Finance_Lend_Payments")]
    public class LendPayment
    {
        [Key]
        public Guid Id { get; set; }
        public Models.Accounting.Enums.ERPObjectType TransactionType = Accounting.Enums.ERPObjectType.LendPayment;


        
        public DateTime TrnDate { get; set; }

        public int No { get; set; }
        public String Detail { get; set; }

        public Guid? LendId { get; set; }
        public virtual Lend Lend { get; set; }

        public Guid? AssetAccountId { get; set; }
        public virtual AccountItem AssetAccount { get; set; }


        public Guid? LendAccountId { get; set; }
        public virtual ChartOfAccount.AccountItem LendAccount { get; set; }

        public LedgerPostStatus PostStatus { get; set; }


    }

}
