
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

    public class JournalEntryTypes : ERPNodeDalRepository
    {
        public JournalEntryTypes(Organization organization) : base(organization)
        {

        }

        public List<JournalEntryType> GetAll()
        {
            return erpNodeDBContext.JournalEntryTypes.ToList();
        }

        public JournalEntryType Find(Guid id) => erpNodeDBContext.JournalEntryTypes.Find(id);

        public JournalEntryType CreateNew(JournalEntryType template)
        {
            template.Id = Guid.NewGuid();
            erpNodeDBContext.JournalEntryTypes.Add(template);
            erpNodeDBContext.SaveChanges();

            return template;
        }
    }
}