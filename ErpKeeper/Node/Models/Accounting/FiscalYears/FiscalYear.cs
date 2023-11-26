using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ERPKeeper.Node.Models.Items;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Accounting.FiscalYears;
using ERPKeeper.Node.Models.Accounting.Enums;

namespace ERPKeeper.Node.Models.Accounting
{
    [Table("ERP_Fiscal_Years")]
    public class FiscalYear
    {
        [Key]
        public Guid Uid { get; set; }
        public Guid? PreviousFiscalUid { get; set; }
        [ForeignKey("PreviousFiscalUid")]
        public virtual FiscalYear PreviousFiscal { get; set; }
        public string Name => string.Format("{0}", this.EndDate.Year.ToString());

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public EnumFiscalYearStatus Status { get; set; }
        public String Memo { get; set; }

        public Decimal IncomeBalance { get; set; }
        public Decimal ExpenseBalance { get; set; }
        public Decimal AssetBalance { get; set; }
        public Decimal LiabilityBalance { get; set; }
        public Decimal EquityBalance { get; set; }
        public Decimal ProfitBalance { get; set; }

        public void UpdateBalance()
        {
            IncomeBalance = this.PeriodAccountBalances?
                .Where(a => a.Account.Type == AccountTypes.Income)
                .Select(l => l.CurrentBalance)
                .DefaultIfEmpty(0)
                .Sum() ?? 0;

            ExpenseBalance = this.PeriodAccountBalances?
                .Where(a => a.Account.Type == AccountTypes.Expense)
                .Select(l => l.CurrentBalance)
                .DefaultIfEmpty(0)
                .Sum() ?? 0;

            ProfitBalance = this.IncomeBalance - this.ExpenseBalance;

            AssetBalance = this.PeriodAccountBalances?
           .Where(p => p.Account.Type == AccountTypes.Asset)
           .Select(l => l.Balance)
           .DefaultIfEmpty(0)
           .Sum() ?? 0;

            LiabilityBalance = this.PeriodAccountBalances?
                .Where(p => p.Account.Type == AccountTypes.Liability)
                .Select(l => l.Balance)
                .DefaultIfEmpty(0)
                .Sum() ?? 0;

            EquityBalance = this.PeriodAccountBalances?
                .Where(p => p.Account.Type == AccountTypes.Capital)
                .Select(l => l.Balance)
                .DefaultIfEmpty(0)
                .Sum() ?? 0;
        }


        public Decimal? ProfitPercent
        {
            get
            {
                if (this.IncomeBalance == 0)
                    return 0;
                else
                    return (((this.IncomeBalance - this.ExpenseBalance) / this.IncomeBalance) * 100);
            }
        }

        public LedgerPostStatus PostStatus { get; set; }

        public Guid? ClosingAccountGuid { get; set; }
        [ForeignKey("ClosingAccountGuid")]
        public virtual AccountItem ClosingAccount { get; set; }

        public virtual ICollection<FiscalYears.PeriodAccountBalance> PeriodAccountBalances { get; set; }
        public virtual ICollection<FiscalYears.PeriodItemCOGS> PeriodItemsCOGS { get; set; }
        public int DayCount => (this.EndDate - this.StartDate).Days + 1;


        public FiscalYear()
        {
            this.Uid = Guid.NewGuid();
        }

        public FiscalYear(DateTime fiscalFirstDate)
        {
            StartDate = fiscalFirstDate.Date;
            EndDate = this.StartDate.AddYears(1).AddDays(-1);
            this.Uid = Guid.NewGuid();
        }



        private List<PeriodAccountBalance> GetClosingBalanceAccounts() =>
            this
            .PeriodAccountBalances
            .Where(pab => pab.Account.Type == AccountTypes.Asset || pab.Account.Type == AccountTypes.Liability || pab.Account.Type == AccountTypes.Capital)
            .ToList();

        internal PeriodAccountBalance CreateAccountBalance(AccountItem account)
        {
            var newAccountBlance = new FiscalYears.PeriodAccountBalance()
            {
                Uid = Guid.NewGuid(),
                Account = account,
                AccountGuid = account.Uid,
            };

            this.PeriodAccountBalances.Add(newAccountBlance);
            return newAccountBlance;
        }

        internal void UpdateOpeningBalance(AccountItem account, decimal debit, decimal credit)
        {
            var accBalanceLine = this.PeriodAccountBalances
                .Where(l => l.AccountGuid == account.Uid)
                .FirstOrDefault();

            if (accBalanceLine == null)
                accBalanceLine = CreateAccountBalance(account);

            accBalanceLine.OpeningDebit = Math.Max(debit - credit, 0);
            accBalanceLine.OpeningCredit = Math.Max(credit - debit, 0);
        }

        internal void UpdateCurrentBalance(AccountItem account, decimal debit, decimal credit)
        {
            var accBalanceLine = this.PeriodAccountBalances
                .Where(l => l.AccountGuid == account.Uid)
                .FirstOrDefault();

            if (accBalanceLine == null)
                accBalanceLine = CreateAccountBalance(account);

            accBalanceLine.Debit = debit;// Math.Max(debit - credit, 0);
            accBalanceLine.Credit = credit;//  Math.Max(credit - debit, 0);
        }


        internal void UpdateClosingBalance(AccountItem account, decimal debit, decimal credit)
        {
            var accBalanceLine = this.PeriodAccountBalances
                .Where(l => l.AccountGuid == account.Uid)
                .FirstOrDefault();

            if (accBalanceLine == null)
                accBalanceLine = CreateAccountBalance(account);

            accBalanceLine.ClosingDebit = Math.Max(debit - credit, 0);
            accBalanceLine.ClosingCredit = Math.Max(credit - debit, 0);
        }

    }
}