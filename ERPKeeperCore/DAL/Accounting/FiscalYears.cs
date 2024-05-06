
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
        public List<FiscalYear> GetAll() => erpNodeDBContext.FiscalYears.ToList();
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
            fy.ClearAccountBalances();
            this.SaveChanges();
        }

        public void Close(FiscalYear fy)
        {
            fy.Status = FiscalYearStatus.Close;
            this.UpdateBalance(fy);
            this.erpNodeDBContext.SaveChanges();
        }


        public void PrepareFiscalYearBalances()
        {
            Console.WriteLine("> Prepare Fiscal Year Balances");
            var accounts = organization.ChartOfAccount.GetAccountsList();


            foreach (var fiscalYear in this.GetAll())
            {
                Console.WriteLine($"> Prepare Fiscal Year Balance {fiscalYear.Name}");
                fiscalYear.PrepareFiscalBalance(accounts);
                organization.SaveChanges();
            }

        }


        public void UpdateTransactionFiscalYears()
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

        public void UpdateIncomeExpense()
        {
            Console.WriteLine("> Update Income Expense");
            this.UpdateTransactionFiscalYears();

            foreach (var fiscalYear in this.GetAll())
            {
                organization.SaveChanges();
            }
        }
    }
}