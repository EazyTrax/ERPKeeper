using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ERPKeeperCore.Enterprise.Models.Items;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;

namespace ERPKeeperCore.Enterprise.Models.Accounting
{
    public class FiscalYear
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsPosted { get; set; }
        public String Name => string.Format("{0}", EndDate.Year.ToString());

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public FiscalYearStatus Status { get; set; }
        public String? Memo { get; set; }


        public decimal IncomeBalance { get; set; }

        public decimal ExpenseBalance { get; set; }

        public decimal ProfitBalance { get; set; }

        public Decimal Income => FiscalYearAccountBalances?
                .Where(a => a.Account.Type == AccountTypes.Income)
                .Select(l => l.Balance)
                .DefaultIfEmpty(0)
                .Sum() ?? 0;

        public Decimal Expense => FiscalYearAccountBalances?
                .Where(a => a.Account.Type == AccountTypes.Expense)
                .Select(l => l.Balance)
                .DefaultIfEmpty(0)
                .Sum() ?? 0;

        public Decimal AssetBalance => FiscalYearAccountBalances?
            .Where(p => p.Account.Type == AccountTypes.Asset)
            .Select(l => l.Balance)
            .DefaultIfEmpty(0)
            .Sum() ?? 0;

        public Decimal LiabilityBalance => FiscalYearAccountBalances?
            .Where(p => p.Account.Type == AccountTypes.Liability)
            .Select(l => l.Balance)
            .DefaultIfEmpty(0)
            .Sum() ?? 0;

        public Decimal EquityBalance => FiscalYearAccountBalances?
            .Where(p => p.Account.Type == AccountTypes.Capital)
            .Select(l => l.Balance)
            .DefaultIfEmpty(0)
            .Sum() ?? 0;


        public Decimal Profit => Income - Expense;

        public decimal? ProfitPercent
        {
            get
            {
                if (Income == 0)
                    return null;
                else
                    return (Income - Expense) / Income * 100;
            }
        }

        public virtual ICollection<FiscalYearAccountBalance> FiscalYearAccountBalances { get; set; }

        public int DayCount => (EndDate - StartDate).Days + 1;

        public FiscalYear()
        {

        }

        internal void ClearAccountBalances()
        {
            Console.WriteLine($"> Clear Accounts Balance");


            FiscalYearAccountBalances.ToList().ForEach(p =>
            {
                FiscalYearAccountBalances.Remove(p);
            });
        }


        private List<FiscalYearAccountBalance> GetClosingBalanceAccounts() => FiscalYearAccountBalances
                                     .Where(pab => pab.Account.Type == AccountTypes.Asset || pab.Account.Type == AccountTypes.Liability || pab.Account.Type == AccountTypes.Capital)
                                     .ToList();



        internal FiscalYearAccountBalance CreateFiscalBalanceLine(Account account)
        {
            var newAccountBlance = new FiscalYearAccountBalance()
            {
                Id = Guid.NewGuid(),
                Account = account,
                AccountId = account.Id,
            };

            FiscalYearAccountBalances.Add(newAccountBlance);
            return newAccountBlance;
        }

        internal void UpdateOpeningFiscalBalance(Account account, decimal debit, decimal credit)
        {
            var accBalanceLine = FiscalYearAccountBalances.Where(l => l.AccountId == account.Id).FirstOrDefault();

            if (accBalanceLine == null)
                accBalanceLine = CreateFiscalBalanceLine(account);

            accBalanceLine.OpeningDebit = Math.Max(debit - credit, 0);
            accBalanceLine.OpeningCredit = Math.Max(credit - debit, 0);
        }

        internal void UpdateCurrentFiscalBalance(Account account, decimal debit, decimal credit)
        {
            var accBalanceLine = FiscalYearAccountBalances.Where(l => l.AccountId == account.Id).FirstOrDefault();

            if (accBalanceLine == null)
                accBalanceLine = CreateFiscalBalanceLine(account);

            accBalanceLine.Debit = Math.Max(debit - credit, 0);
            accBalanceLine.Credit = Math.Max(credit - debit, 0);
        }


        internal void UpdateClosingFiscalBalance(Account account, decimal debit, decimal credit)
        {
            var accBalanceLine = FiscalYearAccountBalances.Where(l => l.AccountId == account.Id).FirstOrDefault();

            if (accBalanceLine == null)
                accBalanceLine = CreateFiscalBalanceLine(account);

            accBalanceLine.ClosingDebit = Math.Max(debit - credit, 0);
            accBalanceLine.ClosingCredit = Math.Max(credit - debit, 0);
        }

    }
}