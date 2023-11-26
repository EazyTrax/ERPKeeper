using ERPKeeper.Node.Models.Financial.Transfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPKeeper.Node.Models.Accounting
{
    [Table("ERP_Accounting_Account_History_Balance")]

    public class HistoryBalanceItem
    {
        [Key]
        public Guid Uid { get; set; }

        [Column("TrnDate")]
        public DateTime TrnDate { get; set; }

        [Column("AccountUid")]
        public Guid AccountUid { get; set; }

        [ForeignKey("AccountUid")]
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