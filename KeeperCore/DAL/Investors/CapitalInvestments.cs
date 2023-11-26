
using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Equity;
using KeeperCore.ERPNode.Models.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KeeperCore.ERPNode.DAL.Investors
{
    public class CapitalActivities : ERPNodeDalRepository
    {
        public CapitalActivities(Organization organization) : base(organization)
        {
            transactionType = Models.Accounting.Enums.ERPObjectType.CapitalActivity;
        }

        public List<CapitalActivity> GetAll()
        {
            return erpNodeDBContext.CapitalActivities.ToList();
        }
        public CapitalActivity Find(Guid transactionId) => erpNodeDBContext.CapitalActivities.Find(transactionId);
        public CapitalActivity CreateNew(Guid investorProfileId)
        {
            var newInvestment = new CapitalActivity()
            {
                Id = Guid.NewGuid(),
                InvestorId = investorProfileId,
                TrnDate = DateTime.Now
            };

            erpNodeDBContext.CapitalActivities.Add(newInvestment);
            erpNodeDBContext.SaveChanges();
            return newInvestment;
        }
        public int GetNextNumber()
        {
            return (erpNodeDBContext.CapitalActivities.Max(e => (int?)e.No) ?? 0) + 1;
        }
        private List<CapitalActivity> GetReadyForPost()
        {
            return erpNodeDBContext.CapitalActivities
.Where(s => s.PostStatus == LedgerPostStatus.Editable)
.ToList()
.Where(t => t.TrnDate >= organization.DataItems.FirstDate)
.ToList();
        }
        public void PostLedger()
        {
            var postingTransactions = this.GetReadyForPost();

            string logTitle = string.Format("> Post {0} [{1}]", this.trString, postingTransactions.Count());
            organization.EventLogs.NewEventLog(EventLogLevel.Information, "00", logTitle, null, "");



            postingTransactions.ForEach(s =>
            {
                this.PostLedger(s, true);
            });

            erpNodeDBContext.SaveChanges();
        }
        public void ReOrder()
        {
            var transactions = erpNodeDBContext.CapitalActivities
                .OrderBy(t => t.TrnDate)
                .OrderBy(t => t.No)
                .ToList();

            int i = 1;

            foreach (var transaction in transactions)
            {
                transaction.No = i++;
            }

            erpNodeDBContext.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var capitalActivity = organization.CapitalActivities.Find(id);
            erpNodeDBContext.CapitalActivities.Remove(capitalActivity);
            organization.SaveChanges();
        }
        public CapitalActivity Save(CapitalActivity capitalInvestment)
        {
            var existCapitalActivity = erpNodeDBContext.CapitalActivities.Find(capitalInvestment.Id);

            if (existCapitalActivity == null)
            {
                existCapitalActivity = capitalInvestment;
                existCapitalActivity.FiscalYear = organization.FiscalYears.Find(existCapitalActivity.TrnDate);
                existCapitalActivity.Id = Guid.NewGuid();
                existCapitalActivity.No = this.GetNextNumber();
                existCapitalActivity.AssetAccountId = existCapitalActivity.AssetAccountId ?? organization.SystemAccounts.Cash.Id;
                existCapitalActivity.EquityAccountId = existCapitalActivity.EquityAccountId ?? organization.SystemAccounts.EquityStock.Id;
                existCapitalActivity.CaluculateTotal();
                erpNodeDBContext.CapitalActivities.Add(existCapitalActivity);
            }
            else
            {
                if (existCapitalActivity.PostStatus != LedgerPostStatus.Locked)
                {
                    existCapitalActivity.FiscalYear = organization.FiscalYears.Find(existCapitalActivity.TrnDate);
                    existCapitalActivity.TrnDate = capitalInvestment.TrnDate;
                    existCapitalActivity.Type = capitalInvestment.Type;
                    existCapitalActivity.AssetAccountId = capitalInvestment.AssetAccountId ?? organization.SystemAccounts.Cash.Id;
                    existCapitalActivity.EquityAccountId = capitalInvestment.EquityAccountId ?? organization.SystemAccounts.EquityStock.Id;
                    existCapitalActivity.StockAmount = capitalInvestment.StockAmount;
                    existCapitalActivity.EachStockParValue = capitalInvestment.EachStockParValue;
                    existCapitalActivity.InvestorId = capitalInvestment.InvestorId;
                    existCapitalActivity.CaluculateTotal();
                }
            }




            erpNodeDBContext.SaveChanges();

            return existCapitalActivity;
        }
        public void UnPost(CapitalActivity capitalInvestment)
        {
            Console.WriteLine("> Un Posting " + capitalInvestment.No);
            organization.LedgersDal.RemoveTransaction(capitalInvestment.Id);
            capitalInvestment.PostStatus = LedgerPostStatus.Editable;

            erpNodeDBContext.SaveChanges();
        }
        public void UnPost()
        {
            Console.WriteLine("> Un Post" + Models.Accounting.Enums.ERPObjectType.CapitalActivity.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);


            var trs = erpNodeDBContext.CapitalActivities.ToList();
            trs.ForEach(s =>
            {
                s.PostStatus = LedgerPostStatus.Editable;
            });
            erpNodeDBContext.SaveChanges();
        }
        public void PostLedger(CapitalActivity tr, bool SaveImmediately = true)
        {
            if (tr.PostStatus == LedgerPostStatus.Locked)
                return;

            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Id = tr.Id,
                TrnDate = tr.TrnDate,
                TransactionName = tr.Name,
                TransactionNo = tr.No ?? 0,
                TransactionType = transactionType
            };
            erpNodeDBContext.TransactionLedgers.Add(trLedger);


            if (tr.Type == Models.Equity.Enums.CapitalActivityType.Invest)
            {
                trLedger.Debit(tr.AssetAccount, tr.TotalStockParValue);
                trLedger.Credit(tr.EquityAccount, tr.TotalStockParValue);
            }
            else
            {
                trLedger.Credit(tr.AssetAccount, Math.Abs(tr.TotalStockParValue));
                trLedger.Debit(tr.EquityAccount, Math.Abs(tr.TotalStockParValue));
            }

            tr.PostStatus = LedgerPostStatus.Locked;

            if (SaveImmediately)
                erpNodeDBContext.SaveChanges();
        }
    }
}