using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using KeeperCore.ERPNode.Models.Items;
using KeeperCore.ERPNode.Models.Accounting.Enums;

namespace KeeperCore.ERPNode.Models.Accounting
{
    [Table("ERP_Fiscal_Years")]
    public class FiscalYear
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? PreviousFiscalId { get; set; }
        [ForeignKey("PreviousFiscalId")]
        public virtual FiscalYear PreviousFiscal { get; set; }

        public string Name => string.Format("{0}", this.EndDate.Year.ToString());

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate => this.StartDate.AddYears(1).AddDays(-1);
        public EnumFiscalYearStatus Status { get; set; }
        public String Memo { get; set; }
        public LedgerPostStatus PostStatus { get; set; }

        public Guid? CloseToAccountId { get; set; }
        [ForeignKey("CloseToAccountId")]
        public virtual Account CloseToAccount { get; set; }



        public Decimal Income => this.PeriodAccountBalances?
                .Where(a => a.Account.Type == AccountTypes.Income)
                .Select(l => l.CurrentBalance)
                .DefaultIfEmpty(0)
                .Sum() ?? 0;

        public Decimal Expense => this.PeriodAccountBalances?
                .Where(a => a.Account.Type == AccountTypes.Expense)
                .Select(l => l.CurrentBalance)
                .DefaultIfEmpty(0)
                .Sum() ?? 0;

        public Decimal AssetBalance => this.PeriodAccountBalances?
            .Where(p => p.Account.Type == AccountTypes.Asset)
            .Select(l => l.Balance)
            .DefaultIfEmpty(0)
            .Sum() ?? 0;

        public Decimal LiabilityBalance => this.PeriodAccountBalances?
            .Where(p => p.Account.Type == AccountTypes.Liability)
            .Select(l => l.Balance)
            .DefaultIfEmpty(0)
            .Sum() ?? 0;

        public Decimal EquityBalance => this.PeriodAccountBalances?
            .Where(p => p.Account.Type == AccountTypes.Capital)
            .Select(l => l.Balance)
            .DefaultIfEmpty(0)
            .Sum() ?? 0;


        public Decimal Profit => this.Income - this.Expense;

        public Decimal? ProfitPercent
        {
            get
            {
                if (this.Income == 0)
                    return null;
                else
                    return (((this.Income - this.Expense) / this.Income) * 100);
            }
        }





        public virtual ICollection<PeriodAccountBalance> PeriodAccountBalances { get; set; }
        public virtual ICollection<FiscalYears.PeriodItemCOGS> PeriodItemsCOGS { get; set; }


        public int DayCount => (this.EndDate - this.StartDate).Days + 1;

        public FiscalYear()
        {

        }



        internal void ClearAccountBalances()
        {
            Console.WriteLine($"> Clear Accounts Balance");


            this.PeriodAccountBalances.ToList().ForEach(p =>
            {
                this.PeriodAccountBalances.Remove(p);
            });
        }


        private List<PeriodAccountBalance> GetClosingBalanceAccounts() => this.PeriodAccountBalances
                                     .Where(pab => pab.Account.Type == AccountTypes.Asset || pab.Account.Type == AccountTypes.Liability || pab.Account.Type == AccountTypes.Capital)
                                     .ToList();



        internal PeriodAccountBalance CreateFiscalBalanceLine(Account account)
        {
            var newAccountBlance = new FiscalYears.PeriodAccountBalance()
            {
                Id = Guid.NewGuid(),
                Account = account,
                AccountId = account.Id,
            };

            this.PeriodAccountBalances.Add(newAccountBlance);
            return newAccountBlance;
        }

        internal void UpdateOpeningFiscalBalance(Account account, decimal debit, decimal credit)
        {
            var accBalanceLine = this.PeriodAccountBalances.Where(l => l.AccountId == account.Id).FirstOrDefault();

            if (accBalanceLine == null)
                accBalanceLine = CreateFiscalBalanceLine(account);

            accBalanceLine.OpeningDebit = Math.Max(debit - credit, 0);
            accBalanceLine.OpeningCredit = Math.Max(credit - debit, 0);
        }

        internal void UpdateCurrentFiscalBalance(Account account, decimal debit, decimal credit)
        {
            var accBalanceLine = this.PeriodAccountBalances.Where(l => l.AccountId == account.Id).FirstOrDefault();

            if (accBalanceLine == null)
                accBalanceLine = CreateFiscalBalanceLine(account);

            accBalanceLine.Debit = Math.Max(debit - credit, 0);
            accBalanceLine.Credit = Math.Max(credit - debit, 0);
        }


        internal void UpdateClosingFiscalBalance(Account account, decimal debit, decimal credit)
        {
            var accBalanceLine = this.PeriodAccountBalances.Where(l => l.AccountId == account.Id).FirstOrDefault();

            if (accBalanceLine == null)
                accBalanceLine = CreateFiscalBalanceLine(account);

            accBalanceLine.ClosingDebit = Math.Max(debit - credit, 0);
            accBalanceLine.ClosingCredit = Math.Max(credit - debit, 0);
        }

    }
}