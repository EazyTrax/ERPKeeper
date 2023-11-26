using KeeperCore.ERPNode.Models.Financial.Transfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeeperCore.ERPNode.Models.ChartOfAccount
{
    [Table("ERP_Accounting_Account_History_Balance")]

    public class HistoryBalanceItem
    {
        [Key]
        public Guid Id { get; set; }

        
        public DateTime TrnDate { get; set; }

        [Column("AccountId")]
        public Guid AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountItem AccountItem { get; set; }

        public int Count { get; set; }

        [Column(TypeName = "Money")]
        public Decimal Balance { get; set; }


        [Column(TypeName = "Money")]
        public Decimal Credit { get; set; }

        [Column(TypeName = "Money")]
        public Decimal Debit { get; set; }

        [Column(TypeName = "Money")]
        public Decimal Total
        {
            get
            {
                if (this.AccountItem.Type == AccountTypes.Asset || this.AccountItem.Type == AccountTypes.Expense)
                    return this.Debit - this.Credit;
                else
                    return this.Credit - this.Debit;
            }
        }


    }
}