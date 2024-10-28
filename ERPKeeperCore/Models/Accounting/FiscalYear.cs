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

        public Guid? PreviusFiscalYearId { get; set; }
        [ForeignKey("PreviusFiscalYearId")]
        public virtual FiscalYear? PreviusFiscalYear { get; set; }

        public virtual FiscalYear? NextFiscalYear { get; set; }


        public String Name => string.Format("{0}", EndDate.Year.ToString());

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public FiscalYearStatus Status { get; set; }
        public String? Memo { get; set; }

        public decimal IncomeBalance => FiscalYearAccountBalances
            .Where(b => b.Account.Type == AccountTypes.Income)
            .Sum(a => a.TotalCredit);

        public decimal ExpenseBalance => FiscalYearAccountBalances
            .Where(b => b.Account.Type == AccountTypes.Expense)
            .Sum(a => a.TotalDebit);


        public decimal AssetsBalance => FiscalYearAccountBalances
         .Where(b => b.Account.Type == AccountTypes.Asset)
         .Sum(a => a.ClosedDebit);

        public decimal LiabilityBalance => FiscalYearAccountBalances
            .Where(b => b.Account.Type == AccountTypes.Liability)
            .Sum(a => a.ClosedCredit);

        public decimal EquityBalance => FiscalYearAccountBalances
      .Where(b => b.Account.Type == AccountTypes.Capital)
      .Sum(a => a.ClosedCredit);


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

        public void UpdateClosingBalance()
        {
            this.Debit = FiscalYearAccountBalances.Sum(a => a.TotalDebit);
            this.Credit = FiscalYearAccountBalances.Sum(a => a.TotalDebit);
        }

        public void PostToTransaction()
        {
            Console.WriteLine($">Post >FiscalYear:{this.Name}");
            if (this.Transaction == null) return;

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
            this.Transaction.PostedDate = DateTime.Today;
        }
    }
}