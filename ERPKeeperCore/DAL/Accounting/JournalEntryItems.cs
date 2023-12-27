
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