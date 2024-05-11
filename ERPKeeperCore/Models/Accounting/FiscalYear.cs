using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ERPKeeperCore.Enterprise.Models.Items;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ERPKeeperCore.Enterprise.Models.Accounting
{
    public class FiscalYear
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }


        public String Name => string.Format("{0}", EndDate.Year.ToString());

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public FiscalYearStatus Status { get; set; }
        public String? Memo { get; set; }


        public decimal IncomeBalance { get; set; }
        public decimal ExpenseBalance { get; set; }
        public decimal ProfitBalance => IncomeBalance - ExpenseBalance;

        public decimal ProfitPercent
        {
            get
            {
                if (IncomeBalance == 0)
                    return 0;
                else
                    return (IncomeBalance - ExpenseBalance) / IncomeBalance * 100;
            }
        }
        public virtual ICollection<FiscalYearAccountBalance> FiscalYearAccountBalances { get; set; }
        public int DayCount => (EndDate - StartDate).Days + 1;

        public decimal Debit { get; private set; }
        public decimal Credit { get; private set; }

        public FiscalYear()
        {

        }

        internal void ClearAccountBalances()
        {
            Console.WriteLine($"> Clear Accounts Balance");
            FiscalYearAccountBalances
                .ToList()
                .ForEach(p => FiscalYearAccountBalances.Remove(p));
        }
        private List<FiscalYearAccountBalance> GetClosingBalanceAccounts() =>
            FiscalYearAccountBalances
            .Where(pab => pab.Account.Type == AccountTypes.Asset || pab.Account.Type == AccountTypes.Liability || pab.Account.Type == AccountTypes.Capital)
            .ToList();


        public void PrepareFiscalBalance(List<Account> accounts, bool clearValue = false)
        {
            Console.WriteLine($"> FISCAL: {this.Name} > Prepare Balance ");

            foreach (var account in accounts)
            {
                var accBalance = FiscalYearAccountBalances
                    .Where(a => a.AccountId == account.Id)
                    .FirstOrDefault();

                if (accBalance == null)
                {
                    accBalance = new FiscalYearAccountBalance()
                    {
                        AccountId = account.Id,
                    };

                    this.FiscalYearAccountBalances.Add(accBalance);
                }

                if (clearValue)
                {
                    accBalance.ClearBalance();
                }
            }
        }
        public void UpdateBalance()
        {
            var balanceSummary = FiscalYearAccountBalances?
                .GroupBy(a => a.Account.Type)
                .Select(g => new { Type = g.Key, TotalBalance = g.Sum(x => x.Balance) })
                .ToList();

            IncomeBalance = balanceSummary?
                .FirstOrDefault(x => x.Type == AccountTypes.Income)?
                .TotalBalance ?? 0;

            ExpenseBalance = balanceSummary?
                .FirstOrDefault(x => x.Type == AccountTypes.Expense)?
                .TotalBalance ?? 0;

            foreach (var accBalance in FiscalYearAccountBalances.ToList())
            {
                if (accBalance == null)
                    continue;
                else if (accBalance.Account.Type == AccountTypes.Income || accBalance.Account.Type == AccountTypes.Expense)
                {
                    accBalance.ClosingDebit = accBalance.Credit;
                    accBalance.ClosingCredit = accBalance.Debit;
                }
                else if (accBalance.Account.Type == AccountTypes.Capital && accBalance.Account.SubType == AccountSubTypes.Equity_RetainEarning)
                {
                    if (this.ProfitBalance > 0)
                    {
                        accBalance.ClosingCredit = this.ProfitBalance;
                        accBalance.ClosedDebit = 0;
                    }
                    else
                    {
                        accBalance.ClosingCredit = 0;
                        accBalance.ClosedDebit = this.ProfitBalance;
                    }
                }
                else
                {
                    accBalance.ClosingDebit = 0;
                    accBalance.ClosingCredit = 0;
                }

                var ClosedDebit = accBalance.OpeningDebit + accBalance.Debit + accBalance.ClosingDebit;
                var ClosedCredit = accBalance.OpeningCredit + accBalance.Credit + accBalance.ClosingCredit;

                accBalance.ClosedDebit = Math.Max(ClosedDebit - ClosedCredit, 0);
                accBalance.ClosedCredit = Math.Max(ClosedCredit - ClosedDebit, 0);



            }

            this.Debit = FiscalYearAccountBalances.Sum(a => a.Debit);
            this.Credit = FiscalYearAccountBalances.Sum(a => a.Debit);
        }

        public void PostToTransaction()
        {
            Console.WriteLine($">Post >FiscalYear:{this.Name}");

            if (this.Transaction == null)
                return;

            this.Transaction.ClearLedger();
            this.Transaction.Date = this.EndDate;
            this.Transaction.Reference = this.Name;

            // Dr.
            this.FiscalYearAccountBalances
                .Where(a => a.ClosingDebit > 0)
                .ToList()
                .ForEach(a => this.Transaction.AddDebit(a.Account, a.ClosingDebit));

            // Cr.
            this.FiscalYearAccountBalances
                .Where(a => a.ClosingCredit > 0)
                .ToList()
                .ForEach(a => this.Transaction.AddCredit(a.Account, a.ClosingCredit));


            this.IsPosted = this.Transaction.UpdateBalance();
            this.Transaction.PostedDate = DateTime.Now;

        }

    }
}