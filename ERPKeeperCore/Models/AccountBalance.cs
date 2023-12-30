using KeeperCore.ERPNode.Models.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeeperCore.ERPNode.Models
{
    public class AccountBalance
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        public int Count { get; set; }

        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal Balance
        {
            get
            {
                if (Account.Type == AccountTypes.Asset || Account.Type == AccountTypes.Expense)
                    return Debit - Credit;
                else
                    return Credit - Debit;
            }
        }
    }
}