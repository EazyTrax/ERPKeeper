using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeperCore.Enterprise.Models.Accounting
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsActive { get; set; }


        public String? No { get; set; }
        public String? Code => No;

        [MaxLength(256)]
        public String? Name { get; set; }
        public bool IsLiquidity { get; set; } = false;
        public bool IsCashEquivalent { get; set; }

        [MaxLength(512)]
        public String? Description { get; set; }
        public bool IsFolder { get; set; } = false;

        public AccountTypes Type { get; set; }
        public AccountSubTypes? SubType { get; set; }


        public Decimal CurrentBalance
        {
            get
            {
                switch (Type)
                {
                    case AccountTypes.Asset:
                    case AccountTypes.Expense:
                        return CurrentDebit - CurrentCredit;
                    case AccountTypes.Liability:
                    case AccountTypes.Capital:
                    case AccountTypes.Income:
                        return CurrentCredit - CurrentDebit;
                    default:
                        return 0;

                }
            }
        }

        public DateTime? BalanceCalulatedDate { get; set; }

        public Decimal OpeningCredit { get; set; } = 0;
        public Decimal OpeningDebit { get; set; } = 0;
        public Decimal CurrentCredit { get; set; } = 0;
        public Decimal CurrentDebit { get; set; } = 0;

        public Decimal TotalDebit { get { return Math.Max(CurrentDebit - CurrentCredit, 0); } }
        public Decimal TotalCredit { get { return Math.Max(CurrentCredit - CurrentDebit, 0); } }

        public virtual ICollection<TransactionLedger> TransactionLedgers { get; set; }

        public void Refresh()
        {
       
            CurrentDebit = this.TransactionLedgers.Sum(x => x.Debit);
            CurrentCredit = this.TransactionLedgers.Sum(x => x.Credit);

            Console.WriteLine(this.TransactionLedgers.Count);
            Console.WriteLine($"Dr.{CurrentDebit} Cr.{CurrentCredit}");
        }
    }
}