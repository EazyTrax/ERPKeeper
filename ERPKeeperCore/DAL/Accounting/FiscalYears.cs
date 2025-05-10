
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using Z.EntityFramework.Plus;

namespace ERPKeeperCore.Enterprise.DAL.Accounting
{
    public class FiscalYears : ERPNodeDalRepository
    {
        public FiscalYears(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<FiscalYear> Periods { get; set; }
        public List<FiscalYear> GetAll() => erpNodeDBContext.FiscalYears.OrderBy(x => x.StartDate).ToList();
        public FiscalYear FirstPeriod => GetAll().OrderBy(f => f.StartDate).FirstOrDefault();
        public FiscalYear CurrentPeriod => Find(DateTime.Today);

        public List<FiscalYear> GetHistoryList() => erpNodeDBContext.FiscalYears
            .Where(period => (period.StartDate) <= DateTime.Today)
            .OrderBy(period => period.StartDate)
            .ToList();

        public FiscalYear Find(Guid uid) => erpNodeDBContext.FiscalYears.Find(uid);
        public FiscalYear Find(DateTime date)
        {
            if (date < organization.DataItems.FirstDate)
                return null;

            if (this.Periods == null)
                this.Periods = this.GetAll();

            FiscalYear fiscalYear = this.Periods
                        .Where(f => date >= f.StartDate && date <= f.EndDate)
                        .FirstOrDefault();

            if (fiscalYear == null)
                fiscalYear = this.Create(date);

            return fiscalYear;
        }
        public FiscalYear Create(DateTime date)
        {
            if (date < organization.DataItems.FirstDate)
                return null;

            var firstDate = organization.DataItems.FirstDate;

            DateTime tagetFiscalFirstDate = new DateTime(date.Year, firstDate.Month, firstDate.Day);

            if (tagetFiscalFirstDate > date)
                tagetFiscalFirstDate = new DateTime(date.Year - 1, firstDate.Month, firstDate.Day);

            var fiscalYear = erpNodeDBContext.FiscalYears
                .Where(p => p.StartDate == tagetFiscalFirstDate.Date)
                .FirstOrDefault();

            if (fiscalYear == null)
            {
                fiscalYear = new FiscalYear()
                {
                    StartDate = tagetFiscalFirstDate,
                };

                erpNodeDBContext.FiscalYears.Add(fiscalYear);
                erpNodeDBContext.SaveChanges();
            }

            return fiscalYear;
        }
        public bool IsFirstPeriod(FiscalYear fy) => fy.Id == this.FirstPeriod.Id;




        public void UpdateTransactionsFiscalYears(bool reUpdate)
        {
            Console.WriteLine($"> Trasnsactions > UpdateFiscalYear");

            foreach (var fiscalYear in this.GetAll())
            {
                DateTime startDate = fiscalYear.StartDate.Date;
                DateTime endDate = fiscalYear.EndDate.Date.AddDays(1);

                organization.ErpCOREDBContext.Transactions
                    .Where(t => (reUpdate || t.FiscalYearId == null) && t.Date >= startDate && t.Date < endDate)
                    .Update(t => new Transaction { FiscalYearId = fiscalYear.Id });

                organization.SaveChanges();
            }
        }

        public void Update_AllYearsAccountsBalance()
        {
            Console.WriteLine("> FISCALs > Update");
            this.UpdateTransactionsFiscalYears(false);

            foreach (var fiscalYear in this.GetAll())
            {
                this.Update_AccountsBalance(fiscalYear);
                organization.SaveChanges();
            }
        }


        public void Update_AccountsBalance(FiscalYear fiscalYear)
        {
            Console.WriteLine($"> FISCAL: {fiscalYear.Name} > UpdateBalance");
            var accounts = organization.ChartOfAccount.All();

            fiscalYear.Create_Empty_Accounts_Balance(accounts);
            erpNodeDBContext.SaveChanges();

            Update_OpeningBalance(fiscalYear);
            erpNodeDBContext.SaveChanges();

            Update_CurrentBalance(fiscalYear);
            erpNodeDBContext.SaveChanges();

        }
        private void Update_CurrentBalance(FiscalYear fiscalYear)
        {
            DateTime startDate = fiscalYear.StartDate.Date;
            DateTime endDate = fiscalYear.EndDate.Date.AddDays(1).AddSeconds(-1);


            // Step 1. Clear Account Balance
            organization.SaveChanges();

            // Step 2. Collecting Account Balance
            var current_AccountsBalance = organization.ErpCOREDBContext
                .TransactionLedgers
                .Where(t =>
                    t.Transaction.Date >= startDate &&
                    t.Transaction.Date <= endDate)
                .Where(t => t.Transaction.Type != Models.Accounting.Enums.TransactionType.FiscalYearClosing)
                .GroupBy(t => t.AccountId)
                .Select(t => new
                {
                    AccountId = t.Key,
                    Account = t.First().Account,
                    Debit = t.Sum(x => x.Debit),
                    Credit = t.Sum(x => x.Credit)
                })
                .ToList();


            organization.ErpCOREDBContext
           .TransactionLedgers
           //.Where(t =>
           //    t.Transaction.Date >= startDate.Date &&
           //    t.Transaction.Date <= endDate.Date)
           .Where(t => t.AccountId == Guid.Parse("e1f9daa9-28d6-48a6-abc1-bcda4c9b76b0"))
           .ToList()
           .ForEach(x =>
           {
               Console.WriteLine($"> {x.Transaction.Date} {x.Account.Name} {x.Debit} {x.Credit}");
           });


            //1628212.05
            var incomes_and_expenses_accounts_lists = current_AccountsBalance
                .Where(x =>
                x.Account.Type == AccountTypes.Income ||
                x.Account.Type == AccountTypes.Expense)
                .Sum(x => x.Credit - x.Debit);

            // Step 3. Update Current & Closing Balance
            var fiscalYearAccountBalances = organization.ErpCOREDBContext
                .FiscalYearAccountBalances
                    .Where(b => b.FiscalYearId == fiscalYear.Id)
                    .ToList();

            fiscalYearAccountBalances.ForEach(fiscalYearAccountBalance =>
            {
                var current_AccountBalance = current_AccountsBalance
                    .Where(x => x.AccountId == fiscalYearAccountBalance.AccountId)
                    .FirstOrDefault();

                if (current_AccountBalance != null)
                {
                    fiscalYearAccountBalance.Debit = current_AccountBalance.Debit;
                    fiscalYearAccountBalance.Credit = current_AccountBalance.Credit;
                }

                if (fiscalYearAccountBalance.Account.Type == AccountTypes.Income || fiscalYearAccountBalance.Account.Type == AccountTypes.Expense)
                {
                    fiscalYearAccountBalance.ClosingCredit = fiscalYearAccountBalance.TotalDebit;
                    fiscalYearAccountBalance.ClosingDebit = fiscalYearAccountBalance.TotalCredit;
                }
                else if (fiscalYearAccountBalance.Account.Type == AccountTypes.Capital && fiscalYearAccountBalance.Account.SubType == AccountSubTypes.Equity_RetainEarning)
                {
                    Console.WriteLine($">> {fiscalYearAccountBalance.Account.Name} {incomes_and_expenses_accounts_lists}");

                    fiscalYearAccountBalance.ClosingDebit = Math.Abs(Math.Min(incomes_and_expenses_accounts_lists, 0));
                    fiscalYearAccountBalance.ClosingCredit = Math.Max(incomes_and_expenses_accounts_lists, 0);
                }
            });

            organization.SaveChanges();
        }
        private void Update_OpeningBalance(FiscalYear fiscalYear)
        {
            var firstDate = organization.FiscalYears.FirstPeriod.StartDate.Date;

            // Step 3. Update Opening Balance
            var priorAccountBalances = erpNodeDBContext.TransactionLedgers
            .Where(t => t.Transaction.Date >= firstDate && t.Transaction.Date < fiscalYear.StartDate.Date)
              .GroupBy(t => t.AccountId)
               .Select(t => new { AccountId = t.Key, Debit = t.Sum(i => i.Debit), Credit = t.Sum(i => i.Credit) })
               .ToList();

            foreach (var priorAccBalance in priorAccountBalances)
            {
                var accBalance = organization.ErpCOREDBContext.FiscalYearAccountBalances
                  .Where(b => b.AccountId == priorAccBalance.AccountId && b.FiscalYearId == fiscalYear.Id)
                  .First();

                if (accBalance != null)
                {
                    accBalance.OpeningDebit = Math.Max(priorAccBalance.Debit - priorAccBalance.Credit, 0);
                    accBalance.OpeningCredit = Math.Max(priorAccBalance.Credit - priorAccBalance.Debit, 0);
                }
            }
            organization.SaveChanges();
        }

        public void UnPostLedger(FiscalYear fiscalYear)
        {
            Console.WriteLine($"{fiscalYear.Name} > Un Post Ledger: ");
            fiscalYear.Remove_AccountsBalance();
            fiscalYear.IsPosted = false;
        }

        public void PostLedger(FiscalYear fiscalYear)
        {
            Console.WriteLine($"{fiscalYear.Name} > Post Ledger: ");

            this.Update_AccountsBalance(fiscalYear);

            fiscalYear.PostToTransaction();
            fiscalYear.IsPosted = true;
            this.SaveChanges();

            Console.WriteLine($"{fiscalYear.Name} > Post Ledger: Done {fiscalYear.IsPosted}");
        }

        public void PostToTransactions(bool rePost = false)
        {
            this.CreateTransactions();

            var fys = erpNodeDBContext.FiscalYears
                .Where(s => s.TransactionId != null)
                .Where(s => s.EndDate < DateTime.Today)
                .Where(s => !s.IsPosted || rePost)
                .ToList();

            fys.OrderBy(x => x.EndDate)
                .ToList()
                .ForEach(fy =>
                {
                    this.Update_CurrentBalance(fy);
                    fy.PostToTransaction();

                    erpNodeDBContext.SaveChanges();
                });
        }
        public void CreateTransactions()
        {
            var FiscalYears = erpNodeDBContext
                .FiscalYears
                .Where(s => s.TransactionId == null)
                .ToList();

            FiscalYears.ForEach(FiscalYear =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(FiscalYear.Id);

                if (transaction == null)
                {
                    Console.WriteLine($">Create >FiscalYear:{FiscalYear.Name}");

                    transaction = new Transaction()
                    {
                        Id = FiscalYear.Id,
                        Date = FiscalYear.EndDate,
                        Reference = FiscalYear.Name,
                        Type = Models.Accounting.Enums.TransactionType.FiscalYearClosing,
                        FiscalYearClosing = FiscalYear,
                    };

                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();

                }
                else
                {
                    Console.WriteLine($">Exist >FiscalYears");
                    FiscalYear.TransactionId = transaction.Id;
                    erpNodeDBContext.SaveChanges();
                }
            });
        }

        public void UpdatePreviusFiscalYear()
        {
            erpNodeDBContext.FiscalYears.OrderBy(x => x.StartDate)
               .ToList()
               .ForEach(currentYear =>
               {
                   // Console.WriteLine($"> FiscalYear > UpdatePreviusFiscalYear > {currentYear.Name}");


                   var previuseYear = erpNodeDBContext.FiscalYears
                   .ToList()
                   .Where(x => x.EndDate == currentYear.StartDate.AddDays(-1)).FirstOrDefault();

                   if (previuseYear != null)
                   {
                       //  Console.WriteLine(previuseYear.Name);
                       currentYear.PreviusFiscalYear = previuseYear;
                   }
                   //    Console.WriteLine("-----------");
               });

            erpNodeDBContext.SaveChanges();
        }
    }
}