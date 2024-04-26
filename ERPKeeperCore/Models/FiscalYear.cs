using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using KeeperCore.ERPNode.Models.Items;
using KeeperCore.ERPNode.Models.Enums;

namespace KeeperCore.ERPNode.Models
{
    [Table("ERP_Fiscal_Years")]
    public class FiscalYear
    {
        [Key]
        public Guid Id { get; set; }
        public string Name => string.Format("{0}", EndDate.Year.ToString());

        public DateTime StartDate { get; set; }
        public DateTime EndDate => StartDate.AddYears(1).AddDays(-1);
        public EnumFiscalYearStatus Status { get; set; }

        [MaxLength(255)]
        public string Memo { get; set; }

        public decimal Income => FiscalYearAccountBalances?
                .Where(a => a.Account.Type == AccountTypes.Income)
                .Select(l => l.Balance)
                .DefaultIfEmpty(0)
                .Sum() ?? 0;

        public decimal Expense => FiscalYearAccountBalances?
                .Where(a => a.Account.Type == AccountTypes.Expense)
                .Select(l => l.Balance)
                .DefaultIfEmpty(0)
                .Sum() ?? 0;

        public decimal AssetBalance => FiscalYearAccountBalances?
            .Where(p => p.Account.Type == AccountTypes.Asset)
            .Select(l => l.Balance)
            .DefaultIfEmpty(0)
            .Sum() ?? 0;

        public decimal LiabilityBalance => FiscalYearAccountBalances?
            .Where(p => p.Account.Type == AccountTypes.Liability)
            .Select(l => l.Balance)
            .DefaultIfEmpty(0)
            .Sum() ?? 0;

        public decimal EquityBalance => FiscalYearAccountBalances?
            .Where(p => p.Account.Type == AccountTypes.Capital)
            .Select(l => l.Balance)
            .DefaultIfEmpty(0)
            .Sum() ?? 0;


        public decimal Profit => Income - Expense;

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