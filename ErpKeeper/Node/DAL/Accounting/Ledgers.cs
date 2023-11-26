
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Reports.CompanyandFinancial;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting;
using System.Data.Entity;

namespace ERPKeeper.Node.DAL.Accounting
{
    public class Ledgers : ERPNodeDalRepository
    {

        public Ledgers(Organization organization) : base(organization)
        {


        }

        public IQueryable<Ledger> GetLedgers(Guid accountUid, DateTime? viewDate)
        {
            IQueryable<Ledger> ledgers;

            if (viewDate != null)
                ledgers = this.erpNodeDBContext
                    .Ledgers
                    .Where(l => l.AccountUid == accountUid && DbFunctions.TruncateTime(l.TrnDate) == viewDate);
            else
                ledgers = this.erpNodeDBContext.Ledgers
                    .Where(l => l.AccountUid == accountUid);

            ledgers = ledgers.OrderBy(i => i.TrnDate)
                .ThenBy(i => i.TransactionType)
                .ThenBy(i => i.TransactionName);

            return ledgers;
        }

        public IQueryable<Models.Accounting.Ledger> GetByTransactionGuid(Guid transactionUid)
        {
            return this.erpNodeDBContext.Ledgers.Where(journal => journal.TransactionGuid == transactionUid);
        }

        public List<Models.Accounting.Ledger> GetByTransactionType(ERPObjectType transactionType)
        {
            return erpNodeDBContext.Ledgers
                .Where(journal => journal.TransactionType == transactionType)
                .OrderBy(journal => journal.accountItem.No)
                .ToList();
        }

        public List<Ledger> GetList(Guid? id) => erpNodeDBContext.Ledgers
                     .Where(gl => gl.TransactionGuid == id)
                     .Include(gl => gl.accountItem)
                     .ToList();

        public IQueryable<Models.Accounting.Ledger> Query() => erpNodeDBContext.Ledgers;
        public IQueryable<Models.Accounting.Ledger> QueryBy(enumTrialBalaceViewType type, Guid? fiscalYearUid)
        {
            switch (type)
            {
                case enumTrialBalaceViewType.Default:
                    return erpNodeDBContext.Ledgers;
                case enumTrialBalaceViewType.Today:
                    return erpNodeDBContext.Ledgers.Where(gl => gl.TrnDate == DateTime.Now);
                case enumTrialBalaceViewType.Last7Day:
                    var Last7Day = DateTime.Now.AddDays(-7);
                    return erpNodeDBContext.Ledgers.Where(gl => gl.TrnDate > Last7Day);
                case enumTrialBalaceViewType.LastWeek:
                    return erpNodeDBContext.Ledgers;
                case enumTrialBalaceViewType.Last30Day:
                    var Last30Day = DateTime.Now.AddDays(-30);
                    return erpNodeDBContext.Ledgers.Where(gl => gl.TrnDate > Last30Day);
                case enumTrialBalaceViewType.LastMonth:
                    return erpNodeDBContext.Ledgers.Where(gl => gl.TrnDate.Year == DateTime.Now.Year && gl.TrnDate.Month == DateTime.Now.Month);
                case enumTrialBalaceViewType.Yeartodate:
                    return erpNodeDBContext.Ledgers.Where(gl => gl.TrnDate.Year == (DateTime.Now.Year));
                case enumTrialBalaceViewType.Lastyear:
                    return erpNodeDBContext.Ledgers.Where(gl => gl.TrnDate.Year == (DateTime.Now.Year - 1));
                case enumTrialBalaceViewType.Last365:
                    var Last365 = DateTime.Now.AddYears(-1);
                    return erpNodeDBContext.Ledgers.Where(gl => gl.TrnDate > Last365);
                case enumTrialBalaceViewType.FiscalYear:
                    var fiscalYear = erpNodeDBContext.FiscalYears.Find(fiscalYearUid);
                    return erpNodeDBContext.Ledgers.Where(gl => gl.TrnDate >= fiscalYear.StartDate && gl.TrnDate <= fiscalYear.EndDate);
                default:
                    return erpNodeDBContext.Ledgers;
            }
        }

        public void RemoveLedgers(Models.Accounting.Enums.ERPObjectType transactionType)
        {
            var removeLedgers = erpNodeDBContext.Ledgers
                .Where(ji => ji.TransactionType == transactionType);
            erpNodeDBContext.Ledgers.RemoveRange(removeLedgers);
            erpNodeDBContext.SaveChanges();
        }

        public void RemoveTransaction(Guid trUid)
        {
            var removeLdegers = erpNodeDBContext.Ledgers
                .Where(x => x.TransactionGuid == trUid);
            erpNodeDBContext.Ledgers.RemoveRange(removeLdegers);
            erpNodeDBContext.SaveChanges();

            var removeTrLedger = erpNodeDBContext.TransactionLedgers.Find(trUid);
            if (removeTrLedger != null)
                erpNodeDBContext.TransactionLedgers.Remove(removeTrLedger);
            erpNodeDBContext.SaveChanges();
        }

        public void UnPost()
        {
            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers]";
            sqlCommand = string.Format(sqlCommand);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);


            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] ";
            sqlCommand = string.Format(sqlCommand);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);


            organization.Purchases.UnPost();
            organization.Sales.Unpost();


            organization.ReceivePayments.UnPost();
            organization.SupplierPayments.UnPost();
            organization.LiabilityPayments.UnPost();


            organization.FundTransfers.UnPost();
            organization.CapitalActivities.UnPost();


            organization.TaxPeriods.UnPost();
            organization.IncomeTaxes.UnPost();


            organization.Loans.UnPost();
            organization.Lends.UnPost();


            organization.EmployeePayments.UnPost();
            organization.JournalEntries.UnPost();
            organization.OpeningEntries.UnPost();
            organization.FiscalYears.UnPost();


            organization.FixedAssets.UnPost();
            organization.FixedAssetDreprecateSchedules.UnPost();


            Console.WriteLine("> UnPost Complete ");
        }

        public Node.Reports.Accounting.AccountLedgers GetLedgerReport(FiscalYear fiscalYear, AccountItem account)
        {

            var ledgers = this.erpNodeDBContext.Ledgers
              .Where(l => l.AccountUid == account.Uid)

              .OrderBy(i => i.TrnDate)
              .ThenBy(i => i.TransactionName)
              .ToList();

            if (ledgers.Count == 0)
                return null;

            decimal currentBalance = 0;

            ledgers.ForEach(l =>
            {
                currentBalance = currentBalance + l.Balance;
                l.TotalBalance = currentBalance;
            });

            this.SaveChanges();

            ledgers = ledgers
                .Where(l =>
                    l.TrnDate.Date >= fiscalYear.StartDate.Date
                    &&
                    l.TrnDate.Date <= fiscalYear.EndDate.Date
                )
                .ToList();


            var Report = new ERPKeeper.Node.Reports.Accounting.AccountLedgers()
            {
                DataSource = ledgers,
                Name = "IncomeStatement",
            };
            Report.Parameters["AccountName"].Value = account.Name;
            Report.Parameters["TimeRange"].Value = fiscalYear.StartDate.ToString("dd-MMM-yy") + " - " + fiscalYear.EndDate.ToString("dd-MMM-yy");
            return Report;
        }

        public void PostLedgers()
        {
            organization.OpeningEntries.PostLedger();

            //Capital Activities
            organization.CapitalActivities.PostLedger();

            ////Commercial Section
            organization.Sales.Post();
            organization.Purchases.PostLedger();

            //Financial Section
            organization.ReceivePayments.PostLedger();
            organization.SupplierPayments.PostLedger();
            organization.LiabilityPayments.PostLedger();

            organization.FundTransfers.PostLedger();
            organization.Loans.PostLedger();
            organization.Lends.PostLedger();

            //Taxes Section
            organization.TaxPeriods.PostLedger();
            organization.IncomeTaxes.PostLedger();

            //Employee Section
            organization.EmployeePayments.PostLedger();

            //Other Section
            organization.JournalEntries.PostLedger();

            //Assets
            organization.FixedAssets.PostLedger();
            organization.FixedAssetDreprecateSchedules.PostLedger();

            //organization.RetirementFixedAssets.UnPost();
            organization.RetirementFixedAssets.PostLedger();

            organization.FiscalYears.PostLedger();
        }

        public void UnPost(ERPObjectType trType)
        {
            Console.WriteLine("> Un Post" + trType.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)trType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)trType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            erpNodeDBContext.SaveChanges();
        }
    }
}