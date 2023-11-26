
using ERPKeeper.Node.DAL.Company;
using ERPKeeper.Node.DBContext;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting.FiscalYears;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ERPKeeper.Node.DAL.Accounting
{
    public class FiscalYears : ERPNodeDalRepository
    {
        public FiscalYears(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.FiscalYearClosing;
        }

        public List<FiscalYear> Periods { get; set; }
        public List<FiscalYear> GetAll() => erpNodeDBContext.FiscalYears.ToList();
        public List<FiscalYear> GetReadyForPost() => erpNodeDBContext.FiscalYears
            .Where(s => s.Status == EnumFiscalYearStatus.Close)
            .Where(s => s.PostStatus == LedgerPostStatus.Editable)
            .ToList();

        public FiscalYear FirstPeriod => erpNodeDBContext.FiscalYears.OrderBy(f => f.StartDate).FirstOrDefault();
        public FiscalYear CurrentPeriod => Find(DateTime.Now);

        public List<FiscalYear> GetHistoryList() => erpNodeDBContext.FiscalYears
            .OrderBy(period => period.StartDate)
            .ToList();

        public FiscalYear Find(Guid uid) => erpNodeDBContext.FiscalYears.Find(uid);

        public FiscalYear Find(DateTime date)
        {
            date = date.Date;

            if (this.Periods == null)
                this.Periods = this.erpNodeDBContext.FiscalYears.ToList();

            FiscalYear fiscalYear = this.Periods
                .Where(fy => date >= fy.StartDate.Date && date <= fy.EndDate.Date)
                .FirstOrDefault();

            if (fiscalYear == null)
                this.Create(date);

            return fiscalYear;
        }

        public void UpdatePreviousFiscalYear(FiscalYear fy)
        {
            var previousFiscal = this.Find(fy.StartDate.AddDays(-1));
            fy.PreviousFiscal = previousFiscal;
            this.SaveChanges();
        }

        private FiscalYear Create(DateTime targetDate)
        {
            if (targetDate < organization.FirstDate)
                return null;

            var firstDate = organization.FirstDate;
            DateTime fiscalFirstDate = new DateTime(targetDate.Year, firstDate.Month, firstDate.Day);

            if (fiscalFirstDate > targetDate)
                fiscalFirstDate = new DateTime(targetDate.Year - 1, firstDate.Month, firstDate.Day);

            var fiscalYear = erpNodeDBContext.FiscalYears
                .Where(p => p.StartDate == fiscalFirstDate.Date)
                .FirstOrDefault();

            if (fiscalYear == null)
            {
                fiscalYear = new FiscalYear(fiscalFirstDate)
                {
                    Status = EnumFiscalYearStatus.Open
                };
                erpNodeDBContext.FiscalYears.Add(fiscalYear);
                erpNodeDBContext.SaveChanges();
            }

            return fiscalYear;
        }

        public bool IsFirstPeriod(FiscalYear fy) => fy.Uid == this.FirstPeriod.Uid;
        public void Save(FiscalYear fy)
        {
            var existFiscalYear = Find(fy.Uid);
            fy.ClosingAccountGuid = fy.ClosingAccountGuid ?? this.organization.SystemAccounts.RetainedEarning?.Uid;

            if (existFiscalYear.Status != EnumFiscalYearStatus.Close)
                existFiscalYear.ClosingAccountGuid = fy.ClosingAccountGuid;
            else
                throw new System.Exception("Cannot save closed periods.");

            erpNodeDBContext.SaveChanges();
        }
        public List<TransactionLedger> GetTransactionLedgers(FiscalYear fy, ERPObjectType trType)
        {
            var transactionLedgers = this.erpNodeDBContext
                .TransactionLedgers
                .Where(l => l.TransactionType == trType && DbFunctions.TruncateTime(l.TrnDate) >= fy.StartDate && DbFunctions.TruncateTime(l.TrnDate) <= fy.EndDate)
                .ToList();
            return transactionLedgers;
        }
        public void UpdateBalance(FiscalYear fiscalYear)
        {
            Console.WriteLine($"> {fiscalYear.Name} > Update > Balance > Prepare");
            var periodAccountBalances = fiscalYear.PeriodAccountBalances.ToList();
            this.erpNodeDBContext.PeriodAccountsBalances.RemoveRange(periodAccountBalances);
            this.SaveChanges();

            var accounts = this.erpNodeDBContext.AccountItems.ToList();
            accounts.ForEach(accountItem =>
            {
                fiscalYear.CreateAccountBalance(accountItem);
            });
            this.SaveChanges();


            this.UpdateOpeningBalance(fiscalYear);
            this.UpdateCurrentBalances(fiscalYear);
            this.UpdateClosingBalances(fiscalYear);
            fiscalYear.UpdateBalance();
            this.SaveChanges();
        }
        private void UpdateClosingBalances(FiscalYear fiscalYear)
        {
            Console.WriteLine($"> {fiscalYear.Name} > Update > Balance > Closing");
            var tempPeriodBalances = erpNodeDBContext.Ledgers
                .Where(j =>
                    DbFunctions.TruncateTime(j.TrnDate) >= DbFunctions.TruncateTime(fiscalYear.StartDate) &&
                    DbFunctions.TruncateTime(j.TrnDate) <= DbFunctions.TruncateTime(fiscalYear.EndDate)
                )
                .Where(j => j.TransactionLedger.TransactionType == ERPObjectType.FiscalYearClosing)
                .GroupBy(o => o.AccountUid)
                .Select(go => new TempClosingEntry
                {
                    Uid = Guid.NewGuid(),
                    FiscalYearUid = fiscalYear.Uid,
                    AccountGuid = go.Key,
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
                fiscalYear.UpdateClosingBalance(tempBalance.Account, tempBalance.Debit, tempBalance.Credit);

            erpNodeDBContext.SaveChanges();
        }
        private void UpdateOpeningBalance(FiscalYear fiscalYear)
        {
            Console.WriteLine($"> {fiscalYear.Name} > Update > Balance > Opening");
            var tempOpeningFiscalBalances = erpNodeDBContext.Ledgers
                .Where(j => DbFunctions.TruncateTime(j.TrnDate) < DbFunctions.TruncateTime(fiscalYear.StartDate))
                .Where(j => j.accountItem.Type != AccountTypes.Income && j.accountItem.Type != AccountTypes.Expense)
                .GroupBy(o => o.AccountUid)
                .Select(go => new TempClosingEntry
                {
                    Uid = Guid.NewGuid(),
                    FiscalYearUid = fiscalYear.Uid,
                    AccountGuid = go.Key,
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
                fiscalYear.UpdateOpeningBalance(tempBalance.Account, tempBalance.Debit, tempBalance.Credit);

            erpNodeDBContext.SaveChanges();
        }

        public void Close(FiscalYear fy)
        {
            fy.Status = EnumFiscalYearStatus.Close;
            this.UpdateBalance(fy);
            this.erpNodeDBContext.SaveChanges();
        }


        private void UpdateCurrentBalances(FiscalYear fiscalYear)
        {
            Console.WriteLine($"> {fiscalYear.Name} > Update > Balance > Current");
            var tempPeriodBalances = erpNodeDBContext.Ledgers
                .Where(j =>
                    DbFunctions.TruncateTime(j.TrnDate) >= DbFunctions.TruncateTime(fiscalYear.StartDate) &&
                    DbFunctions.TruncateTime(j.TrnDate) <= DbFunctions.TruncateTime(fiscalYear.EndDate)
                )
                .Where(j => j.TransactionType != ERPObjectType.FiscalYearClosing)
                .GroupBy(o => o.AccountUid)
                .Select(go => new TempClosingEntry
                {
                    Uid = Guid.NewGuid(),
                    FiscalYearUid = fiscalYear.Uid,
                    AccountGuid = go.Key,
                    Account = go.FirstOrDefault().accountItem,
                    Credit = go.Select(l => l.Credit).DefaultIfEmpty(0).Sum() ?? 0,
                    Debit = go.Select(l => l.Debit).DefaultIfEmpty(0).Sum() ?? 0,
                })
                .ToList();

            foreach (var tempBalance in tempPeriodBalances)
                fiscalYear.UpdateCurrentBalance(tempBalance.Account, tempBalance.Debit, tempBalance.Credit);

            erpNodeDBContext.SaveChanges();
        }

        public void UnPost()
        {
            Console.WriteLine("> Un Post " + this.transactionType.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            sqlCommand = "UPDATE [dbo].[ERP_Fiscal_Years] SET  [PostStatus] = '0'";
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);
        }
        public void UnPost(FiscalYear fy)
        {
            if (fy.PostStatus == LedgerPostStatus.Locked)
            {
                organization.LedgersDal.RemoveTransaction(fy.Uid);
                fy.PostStatus = LedgerPostStatus.Editable;
                fy.Status = EnumFiscalYearStatus.Open;
                erpNodeDBContext.SaveChanges();
            }

            this.erpNodeDBContext.SaveChanges();
        }
        public void PostLedger()
        {
            Console.WriteLine($"> Post {this.transactionType}");

            this.GetReadyForPost().ForEach(fiscalYear =>
            {
                this.PostLedger(fiscalYear, false);
                organization.FiscalYears.UpdateBalance(fiscalYear);
                erpNodeDBContext.SaveChanges();
            });
        }
        public bool PostLedger(FiscalYear fiscalYear, bool SaveImmediately = true)
        {
            if (fiscalYear.PostStatus == LedgerPostStatus.Locked)
                return false;

            var trLedger = new TransactionLedger()
            {
                Uid = fiscalYear.Uid,
                TrnDate = fiscalYear.EndDate,
                TransactionName = fiscalYear.Name,
                TransactionType = transactionType
            };

            fiscalYear.PeriodAccountBalances
                .Where(b => b.Account.Type == AccountTypes.Income || b.Account.Type == AccountTypes.Expense)
                .ToList().ForEach(b =>
                {
                    trLedger.Debit(b.Account, b.Credit);
                    trLedger.Credit(b.Account, b.Debit);
                });

            if (fiscalYear.ProfitBalance > 0)
                trLedger.Credit(fiscalYear.ClosingAccount, fiscalYear.ProfitBalance);

            if (fiscalYear.ProfitBalance < 0)
                trLedger.Debit(fiscalYear.ClosingAccount, Math.Abs(fiscalYear.ProfitBalance));

            if (trLedger.FinalValidate())
            {
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
                fiscalYear.PostStatus = LedgerPostStatus.Locked;
            }
            else return false;


            if (SaveImmediately)
                erpNodeDBContext.SaveChanges();

            return true;
        }
    }
}