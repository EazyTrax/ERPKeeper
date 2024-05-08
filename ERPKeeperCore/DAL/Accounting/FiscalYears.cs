
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
        public FiscalYear CurrentPeriod => Find(DateTime.Now);

        public List<FiscalYear> GetHistoryList() => erpNodeDBContext.FiscalYears
            .Where(period => (period.StartDate) <= DateTime.Now)
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
                    Status = FiscalYearStatus.Open
                };


                erpNodeDBContext.FiscalYears.Add(fiscalYear);
                erpNodeDBContext.SaveChanges();
            }

            return fiscalYear;
        }
        public bool IsFirstPeriod(FiscalYear fy) => fy.Id == this.FirstPeriod.Id;

        public void UpdateBalance(FiscalYear fy)
        {
            Console.WriteLine($"> Update {fy.Name} Balance");
            this.SaveChanges();
        }

        public void Close(FiscalYear fy)
        {
            fy.Status = FiscalYearStatus.Close;
            this.erpNodeDBContext.SaveChanges();
        }


        public void PrepareFiscalYearBalances()
        {
            Console.WriteLine("> Prepare Fiscal Year Balances");
            var accounts = organization.ChartOfAccount.GetAccountsList();

            foreach (var fiscalYear in this.GetAll())
            {
                Console.WriteLine($"> Prepare Fiscal Year Balance {fiscalYear.Name}");
                fiscalYear.PrepareFiscalBalance(accounts, true);
                organization.SaveChanges();
            }

        }


        public void UpdateTransactionsFiscalYears()
        {
            Console.WriteLine("> Update TR with Fiscal Years");

            foreach (var fiscalYear in this.GetAll())
            {
                Console.WriteLine($"> FiscalYear:{fiscalYear.Name}");

                var transactions = organization.ErpCOREDBContext.Transactions
                    .Where(t => t.FiscalYearId == null && t.Date >= fiscalYear.StartDate.Date && t.Date < fiscalYear.EndDate.Date.AddDays(1))
                    .Update(t => new Transaction { FiscalYearId = fiscalYear.Id });

                organization.SaveChanges();
            }
        }

        public void UpdateAccountBalance()
        {
            // organization.ErpCOREDBContext.FiscalYearAccountBalances.Delete();
            organization.SaveChanges();

            Console.WriteLine("> Update Income Expense");
            this.PrepareFiscalYearBalances();
            this.UpdateTransactionsFiscalYears();

            var fiscalYears = this.GetAll();

            foreach (var fiscalYear in fiscalYears)
            {
                Console.Write($"FISCAL: {fiscalYear.Name}");

                // Step 1. Clear Account Balance

                organization.SaveChanges();

                // Step 2. Collecting Account Balance
                var accountsBalance = organization.ErpCOREDBContext
                    .TransactionLedgers
                    .Where(t => t != null && t.Transaction.FiscalYearId == fiscalYear.Id)
                    .GroupBy(t => t.AccountId)
                    .Select(t => new
                    {
                        AccountId = t.Key,
                        Account = t.First().Account,
                        Debit = t.Sum(x => x.Debit),
                        Credit = t.Sum(x => x.Credit)
                    })
                    .ToList();

                // Step 3. Collecting Account Balance
                accountsBalance.ForEach(x =>
                {
                    var accountBalance = organization.ErpCOREDBContext.FiscalYearAccountBalances
                        .Where(b => b.AccountId == x.AccountId && b.FiscalYearId == fiscalYear.Id)
                        .FirstOrDefault();

                    accountBalance.Debit = Math.Max(x.Debit - x.Credit, 0);
                    accountBalance.Credit = Math.Max(x.Credit - x.Debit, 0);

                    //Console.WriteLine($"> {x.Account.Name}");
                    //Console.WriteLine($"> Debit:{x.Debit}");
                    //Console.WriteLine($"> Credit:{x.Credit}");
                });

                organization.SaveChanges();

                fiscalYear.UpdateBalance();
                organization.SaveChanges();
            }
        }
    }
}