using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Financial.Transfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeeperCore.ERPNode.Models.Accounting
{
    [Table("ERP_Accounting_Account_History_Balance")]

    public class AccountBalance
    {
        [Key]
        public Guid Id { get; set; }


        public DateTime TrnDate { get; set; }

        [Column("AccountId")]
        public Guid AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account AccountItem { get; set; }

        public int Count { get; set; }

        [Column(TypeName = "Money")]
        public decimal Balance { get; set; }


        [Column(TypeName = "Money")]
        public decimal Credit { get; set; }

        [Column(TypeName = "Money")]
        public decimal Debit { get; set; }

        [Column(TypeName = "Money")]
        public decimal Total
        {
            get
            {
                if (AccountItem.Type == AccountTypes.Asset || AccountItem.Type == AccountTypes.Expense)
                    return Debit - Credit;
                else
                    return Credit - Debit;
            }
        }


    }
}