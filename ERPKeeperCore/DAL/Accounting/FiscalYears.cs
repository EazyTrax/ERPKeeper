
using KeeperCore.ERPNode.DAL.Company;
using KeeperCore.ERPNode.DBContext;
using KeeperCore.ERPNode.Models.Accounting;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Accounting.FiscalYears;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KeeperCore.ERPNode.DAL.Accounting
{
    public class FiscalYears : ERPNodeDalRepository
    {
        public FiscalYears(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.FiscalYearClosing;
        }

        public List<FiscalYear> Periods { get; set; }
        public List<FiscalYear> GetAll()
        {
            return erpNodeDBContext.FiscalYears.ToList();
        }
        public List<FiscalYear> GetReadyForPost() => erpNodeDBContext.FiscalYears
            .Where(s => s.Status == EnumFiscalYearStatus.Close)
            .Where(s => s.PostStatus == LedgerPostStatus.Editable)
            .ToList();
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
        public void UpdatePreviousFiscalYear(FiscalYear fy)
        {
            var previousFiscal = this.Find(fy.StartDate.AddDays(-1));
            fy.PreviousFiscal = previousFiscal;
            this.SaveChanges();
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
                    Status = EnumFiscalYearStatus.Open
                };


                erpNodeDBContext.FiscalYears.Add(fiscalYear);
                erpNodeDBContext.SaveChanges();
            }

            return fiscalYear;
        }
        public bool IsFirstPeriod(FiscalYear fy) => fy.Id == this.FirstPeriod.Id;
        public void Save(FiscalYear fy)
        {
            var existFiscalYear = Find(fy.Id);

            fy.CloseToAccountId = fy.CloseToAccountId ?? this.organization.SystemAccounts.RetainedEarning?.Id;

            if (existFiscalYear.Status != EnumFiscalYearStatus.Close)
                existFiscalYear.CloseToAccountId = fy.CloseToAccountId;
            else
                throw new System.Exception("Cannot save closed periods.");

            erpNodeDBContext.SaveChanges();
        }
        public List<TransactionLedger> GetTransactionLedgers(FiscalYear fy, ERPObjectType trType)
        {
            var transactionLedgers = this.erpNodeDBContext
                .TransactionLedgers
                .Where(l => l.TransactionType == trType && (l.TrnDate) >= fy.StartDate && (l.TrnDate) <= fy.EndDate)
                .ToList();
            return transactionLedgers;
        }

        public void UpdateBalance(FiscalYear fy)
        {
            Console.WriteLine($"> Update {fy.Name} Balance");
            fy.ClearAccountBalances();
            this.SaveChanges();

            this.UpdateOpeningBalance(fy);
            this.UpdateCurrentBalances(fy);
            this.UpdateClosingBalances(fy);


            this.SaveChanges();
        }

        private void UpdateClosingBalances(FiscalYear fy)
        {
            Console.WriteLine("> Update Period Balances");
            var tempPeriodBalances = erpNodeDBContext.Ledgers
                .Where(j =>
                    (j.TrnDate) >= (fy.StartDate) &&
                    (j.TrnDate) <= (fy.EndDate)
                )
                .Where(j => j.TransactionLedger.TransactionType == ERPObjectType.FiscalYearClosing)
                .GroupBy(o => o.AccountId)
                .Select(go => new TempClosingEntry
                {
                    Id = Guid.NewGuid(),
                    FiscalYearId = fy.Id,
                    AccountId = go.Key,
                    Account = go.FirstOrDefault().accountItem,
                    Credit = go.Select(l => l.Credit)
                      .DefaultIfEmpty(0)
                      .Sum() ?? 0,
                    Debit = go.Select(l => l.Debit)
                     .DefaultIfEmpty(0)
                     .Sum() ?? 0,
                })
                .ToList();

            foreach (var tempBalance in tempPeriodBalances)
                fy.UpdateClosingFiscalBalance(tempBalance.Account, tempBalance.Debit, tempBalance.Credit);

            erpNodeDBContext.SaveChanges();
        }

        private void UpdateOpeningBalance(FiscalYear fy)
        {

            Console.WriteLine("> Update Period Balances");
            var tempOpeningFiscalBalances = erpNodeDBContext.Ledgers
                .Where(j => (j.TrnDate) < (fy.StartDate))
                .Where(j => j.accountItem.Type != AccountTypes.Income && j.accountItem.Type != AccountTypes.Expense)
                .GroupBy(o => o.AccountId)
                .Select(go => new TempClosingEntry
                {
                    Id = Guid.NewGuid(),
                    FiscalYearId = fy.Id,
                    AccountId = go.Key,
                    Account = go.FirstOrDefault().accountItem,
                    Credit = go.Select(l => l.Credit)
                      .DefaultIfEmpty(0)
                      .Sum() ?? 0,
                    Debit = go.Select(l => l.Debit)
                     .DefaultIfEmpty(0)
                     .Sum() ?? 0,
                })
                .ToList();

            foreach (var tempBalance in tempOpeningFiscalBalances)
                fy.UpdateOpeningFiscalBalance(tempBalance.Account, tempBalance.Debit, tempBalance.Credit);

            erpNodeDBContext.SaveChanges();
        }

        public void Close(FiscalYear fy)
        {
            fy.Status = EnumFiscalYearStatus.Close;
            this.UpdateBalance(fy);
            this.erpNodeDBContext.SaveChanges();
        }


        private void UpdateCurrentBalances(FiscalYear fy)
        {
            Console.WriteLine("> Update Period Balances");
            var tempPeriodBalances = erpNodeDBContext.Ledgers
                .Where(j => j.TrnDate >= fy.StartDate && j.TrnDate <= fy.EndDate)
                .Where(j => j.TransactionType != ERPObjectType.FiscalYearClosing)
                .GroupBy(o => o.AccountId)
                .Select(go => new TempClosingEntry
                {
                    Id = Guid.NewGuid(),
                    FiscalYearId = fy.Id,
                    AccountId = go.Key,
                    Account = go.FirstOrDefault().accountItem,
                    Credit = go.Select(l => l.Credit)
                      .DefaultIfEmpty(0)
                      .Sum() ?? 0,
                    Debit = go.Select(l => l.Debit)
                     .DefaultIfEmpty(0)
                     .Sum() ?? 0,
                })
                .ToList();

            foreach (var tempBalance in tempPeriodBalances)
                fy.UpdateCurrentFiscalBalance(tempBalance.Account, tempBalance.Debit, tempBalance.Credit);

            erpNodeDBContext.SaveChanges();
        }

        public void UnPost()
        {
            Console.WriteLine("> Un Post " + this.transactionType.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            sqlCommand = "UPDATE [dbo].[ERP_Fiscal_Years] SET  [PostStatus] = '0'";
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);
        }

        public void UnPost(FiscalYear fy)
        {
            if (fy.PostStatus == LedgerPostStatus.Locked)
            {
                organization.LedgersDal.RemoveTransaction(fy.Id);
                fy.PostStatus = LedgerPostStatus.Editable;
                fy.Status = EnumFiscalYearStatus.Open;
                erpNodeDBContext.SaveChanges();
            }

            this.erpNodeDBContext.SaveChanges();
        }
        public void PostLedger()
        {
            Console.WriteLine($"> Post {this.transactionType}");

            this.GetReadyForPost().ForEach(s =>
            {
                this.PostLedger(s, false);
                organization.FiscalYears.UpdateBalance(s);
                erpNodeDBContext.SaveChanges();
            });
        }
        public bool PostLedger(FiscalYear tr, bool SaveImmediately = true)
        {
            if (tr.PostStatus == LedgerPostStatus.Locked)
                return false;

            var trLedger = new TransactionLedger()
            {
                Id = tr.Id,
                TrnDate = tr.EndDate,
                TransactionName = tr.Name,
                TransactionType = transactionType
            };

            tr.PeriodAccountBalances
                .Where(b => b.Account.Type == AccountTypes.Income || b.Account.Type == AccountTypes.Expense)
                .ToList().ForEach(b =>
                {
                    trLedger.Debit(b.Account, b.Credit);
                    trLedger.Credit(b.Account, b.Debit);
                });

            if (tr.Profit > 0)
                trLedger.Credit(tr.CloseToAccount, tr.Profit);

            if (tr.Profit < 0)
                trLedger.Debit(tr.CloseToAccount, Math.Abs(tr.Profit));

            if (trLedger.FinalValidate())
            {
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
                tr.PostStatus = LedgerPostStatus.Locked;
            }
            else return false;


            if (SaveImmediately)
                erpNodeDBContext.SaveChanges();

            return true;
        }
    }
}