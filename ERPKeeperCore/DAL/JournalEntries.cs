
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class JournalEntries : ERPNodeDalRepository
    {
        public JournalEntries(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<JournalEntry> GetAll()
        {
            return erpNodeDBContext.JournalEntries.ToList();
        }



        public JournalEntry? Find(Guid Id) => erpNodeDBContext.JournalEntries.Find(Id);

        public void UnPost(JournalEntry model)
        {
            model.Transaction.ClearLedger();
            model.Debit = 0;
            model.Credit = 0;
            model.IsPosted = false;

            erpNodeDBContext.SaveChanges();
        }


        public void PostToTransactions()
        {
            this.CreateTransactions();

            var journalEntries = erpNodeDBContext.JournalEntries
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted)
                .OrderBy(s => s.Date).ToList();

            journalEntries.ForEach(journalEntry =>
            {
                journalEntry.PostToTransaction();
                erpNodeDBContext.SaveChanges();
            });
        }
        public void CreateTransactions()
        {
            var journalEntries = erpNodeDBContext
                .JournalEntries
                .Where(s => s.TransactionId == null)
                .ToList();

            journalEntries.ForEach(purchase =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(purchase.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{purchase.Name}");
                    transaction = new Transaction()
                    {
                        Id = purchase.Id,
                        Date = purchase.Date,
                        Reference = purchase.Name,
                        Type = Models.Accounting.Enums.TransactionType.JournalEntry,
                        JournalEntry = purchase,
                    };
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }
    }
}