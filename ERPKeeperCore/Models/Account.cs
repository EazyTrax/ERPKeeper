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

        public Guid? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Account Parent { get; set; }
        public virtual ICollection<Account> Child { get; set; }

        public bool IsActive { get; set; }

        public decimal No { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string EnglishName { get; set; }
        public bool IsLiquidity { get; set; } = false;

        [MaxLength(512)]
        public string Description { get; set; }

        public bool IsFolder { get; set; } = false;

        public AccountTypes Type { get; set; }
        public AccountSubTypes? SubType { get; set; }
        public bool IsFocus { get; set; } = false;

        public DateTime OpeningDate { get; set; }
        public decimal OpeningDebitBalance { get; set; } = 0;
        public decimal OpeningCreditBalance { get; set; } = 0;

        
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
                    default:
                        return CurentCredit - CurrentDebit;
                }
            }
        }

        public DateTime? BalanceCalulatedDate { get; set; }

        public decimal CurentCredit { get; set; } = 0;
        public decimal CurrentDebit { get; set; } = 0;

        public virtual ICollection<TaxCode> TaxCodeAssignAccounts { get; set; }
        public virtual ICollection<TaxCode> TaxCodeCloseToAccounts { get; set; }
        public virtual ICollection<TaxCode> TaxCodeTaxAccounts { get; set; }
        public virtual ICollection<TaxCode> TaxCodeOutputTaxAccounts { get; set; }
        public virtual ICollection<TaxCode> TaxCodeInputTaxAccounts { get; set; }
        public bool IsCashEquivalent { get; internal set; }
    }
}