using KeeperCore.ERPNode.Models.Enums;
using KeeperCore.ERPNode.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KeeperCore.ERPNode.Models
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public decimal No { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }
        public bool IsLiquidity { get; set; } = false;
        public bool IsCashEquivalent { get; internal set; }

        [MaxLength(256)]
        public string Description { get; set; }
        public bool IsFolder { get; set; } = false;

        public AccountTypes Type { get; set; }
        public AccountSubTypes? SubType { get; set; }


        public decimal CurrentBalance
        {
            get
            {
                switch (Type)
                {
                    case AccountTypes.Asset:
                    case AccountTypes.Expense:
                        return CurrentDebit - CurentCredit;
                    case AccountTypes.Liability:
                    case AccountTypes.Capital:
                    case AccountTypes.Income:
                        return CurentCredit - CurrentDebit;
                    default:
                        return 0;
                      
                }
            }
        }

        public DateTime? BalanceCalulatedDate { get; set; }
        public decimal CurentCredit { get; set; } = 0;
        public decimal CurrentDebit { get; set; } = 0;

     


    }
}