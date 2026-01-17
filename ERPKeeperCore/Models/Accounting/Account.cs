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


    public enum AccountReportGroup
    {
        NA = 0,
        Asset_Current = 1,
        Asset_NonCurrent = 2,

        Liability_Current = 11,
        Liability_NonCurrent = 12,


        Income_Operating = 41,
        Income_NonOperating = 42,

        Expense_COSG = 51,
        Expense_Operating = 52,
        Expense_NonOperating = 53,
    }



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

        public bool IsReceivable { get; set; }
        public String? ReceivableDisplayName { get; set; }




        [MaxLength(512)]
        public String? Description { get; set; }
        public bool IsFolder { get; set; } = false;

        public AccountTypes Type { get; set; }
        public AccountSubTypes? SubType { get; set; }
        public AccountReportGroup? Group { get; set; }

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

        [Precision(18, 2)]
        public Decimal OpeningCredit { get; set; } = 0;

        [Precision(18, 2)]
        public Decimal OpeningDebit { get; set; } = 0;

        [Precision(18, 2)]
        public Decimal CurrentCredit { get; set; } = 0;

        [Precision(18, 2)]
        public Decimal CurrentDebit { get; set; } = 0;

        public Decimal TotalDebit { get { return Math.Max(CurrentDebit - CurrentCredit, 0); } }
        public Decimal TotalCredit { get { return Math.Max(CurrentCredit - CurrentDebit, 0); } }

        public virtual ICollection<TransactionLedger> TransactionLedgers { get; set; }
        public virtual ICollection<AccountBalance> AccountBalances { get; set; }

        public void Refresh()
        {

            CurrentDebit = this.TransactionLedgers.Sum(x => x.Debit);
            CurrentCredit = this.TransactionLedgers.Sum(x => x.Credit);

            Console.WriteLine(this.TransactionLedgers.Count);
            Console.WriteLine($"Dr.{CurrentDebit} Cr.{CurrentCredit}");
        }

        public void CreateHostoriesBalances()
        {
            // Clear existing balances
            this.AccountBalances.Clear();

            // Group transactions by date
            var groupedTransactions = TransactionLedgers.GroupBy(d => d.Date.Date).OrderBy(x => x.Key);

            Decimal TotalDebit = 0;
            Decimal TotalCredit = 0;
            // Iterate over each group
            foreach (var group in groupedTransactions)
            {
                // Calculate totals
                var Debit = group.Sum(x => x.Debit);
                var Credit = group.Sum(x => x.Credit);

                TotalDebit = TotalDebit + Debit;
                TotalCredit = TotalCredit + Credit;

                // Create new balance
                var balance = new AccountBalance
                {
                    AccountId = this.Id,
                    Date = group.Key,
                    Credit = Credit,
                    Debit = Debit,
                    TotalCredit = Math.Max(TotalCredit - TotalDebit, 0),
                    TotalDebit = Math.Max(TotalDebit - TotalCredit, 0),
                };

                // Add to account balances
                this.AccountBalances.Add(balance);
            }
        }
    }
}