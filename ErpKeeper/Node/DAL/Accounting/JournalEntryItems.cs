
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.DAL.Company;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Transactions;
using System.Data.Entity;
using ERPKeeper.Node.Models.Accounting.Enums;

namespace ERPKeeper.Node.DAL.Accounting
{

    public class JournalEntryItems : ERPNodeDalRepository
    {
        public JournalEntryItems(Organization organization) : base(organization)
        {

        }

        public List<JournalEntryItem> GetAll()
        {
            return erpNodeDBContext.JournalEntryItems.ToList();
        }

        public JournalEntryItem Find(Guid id) => erpNodeDBContext.JournalEntryItems.Find(id);
    }
}