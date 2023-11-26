
using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.DAL.Company;
using KeeperCore.ERPNode.Models.AccountingEntries;
using KeeperCore.ERPNode.Models.Transactions;
using Microsoft.EntityFrameworkCore;
using KeeperCore.ERPNode.Models.Accounting.Enums;



namespace KeeperCore.ERPNode.DAL.Accounting
{

    public class JournalEntries : ERPNodeDalRepository
    {
        public JournalEntries(Organization organization) : base(organization)
        {
            this.transactionType = ERPObjectType.JournalEntry;
        }

        public List<JournalEntry> GetAll() => erpNodeDBContext.JournalEntries.Include(j => j.FiscalYear).ToList();

        public JournalEntry Find(Guid id) => erpNodeDBContext.JournalEntries.Find(id);

        public int GetNextNumber() => (erpNodeDBContext.JournalEntries.Max(b => (int?)b.No) ?? 0) + 1;


        public void ReOrder()
        {
            var journalEntries = erpNodeDBContext.JournalEntries
                .OrderBy(t => t.TrnDate)
                .ToList();

            int i = 1;
            foreach (var j in journalEntries)
            {
                j.No = i;
                i++;

                j.FiscalYear = this.organization.FiscalYears.Find(j.TrnDate);
            }

            erpNodeDBContext.SaveChanges();
        }

        public void Delete(JournalEntry jourmalEntry)
        {
            if (jourmalEntry.PostStatus == LedgerPostStatus.Locked)
                this.UnPost(jourmalEntry);

            erpNodeDBContext.JournalEntries.Remove(jourmalEntry);
            erpNodeDBContext.SaveChanges();
        }

        public void Save(JournalEntry journalEntry)
        {
            var existJourmalEntry = this.Find(journalEntry.Id);

            existJourmalEntry.FiscalYear = organization.FiscalYears.Find(journalEntry.TrnDate);
            existJourmalEntry.TrnDate = journalEntry.TrnDate;
            existJourmalEntry.Memo = journalEntry.Memo;
            existJourmalEntry.UpdateAmount();

            erpNodeDBContext.SaveChanges();
        }

        public void RemoveNull()
        {
            var zeroAccountsBalance = this.erpNodeDBContext.JournalEntryItems
                 .Where(z => z.Debit == null && z.Credit == null)
                 .ToList();


            erpNodeDBContext.JournalEntryItems.RemoveRange(zeroAccountsBalance);
            erpNodeDBContext.SaveChanges();

        }

        public void UnPost()
        {
            Console.WriteLine("> Un Post" + this.transactionType.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlRaw(sqlCommand);

            erpNodeDBContext.JournalEntries.ToList().ForEach(s => s.PostStatus = LedgerPostStatus.Editable);
            erpNodeDBContext.SaveChanges();

        }

        public JournalEntry CreateNew(Guid entryTypeId)
        {
            JournalEntry jourmalEntry = new JournalEntry()
            {
                Id = Guid.NewGuid(),
                JournalEntryTypeId = entryTypeId,
                TrnDate = DateTime.Now,
                No = this.GetNextNumber(),
            };


            erpNodeDBContext.JournalEntries.Add(jourmalEntry);
            erpNodeDBContext.SaveChanges();

            return jourmalEntry;
        }

        public void UnPost(JournalEntry journalEntry)
        {
            organization.LedgersDal.RemoveTransaction(journalEntry.Id);
            journalEntry.PostStatus = LedgerPostStatus.Editable;
            erpNodeDBContext.SaveChanges();
        }

        public List<JournalEntry> GetReadyForPost()
        {
            return erpNodeDBContext.JournalEntries
.Where(s => s.PostStatus == LedgerPostStatus.Editable)
.Include(s => s.Items).ToList();
        }

        public void PostLedger()
        {
            var postingTransactions = this.GetReadyForPost();
            Console.WriteLine($"> Post {this.transactionType} [{postingTransactions.Count()}]");

            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;

            postingTransactions.ForEach(s =>
            {
                this.PostLedger(s, false);
            });

            //erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;
            erpNodeDBContext.ChangeTracker.DetectChanges();
            erpNodeDBContext.SaveChanges();
        }
        public bool PostLedger(JournalEntry tr, bool SaveImmediately = true)
        {
            if (tr.PostStatus == LedgerPostStatus.Locked)
                return false;

            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Id = tr.Id,
                TrnDate = tr.TrnDate,
                TransactionName = tr.Name,
                TransactionNo = tr.No,
                TransactionType = transactionType
            };

            foreach (var journalentryItem in tr.Items.ToList())
            {
                if (journalentryItem.Debit != null && journalentryItem.Debit > 0)
                    trLedger.Debit(journalentryItem.Account, journalentryItem.Debit ?? 0);
                else
                    trLedger.Credit(journalentryItem.Account, journalentryItem.Credit ?? 0);
            }


            if (trLedger.FinalValidate())
            {
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
                tr.PostStatus = LedgerPostStatus.Locked;
            }
            else
            {
                return false;
            }


            if (SaveImmediately)
                erpNodeDBContext.SaveChanges();

            return true;
        }
    }
}