
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.DAL.Company;
using ERPKeeper.Node.Models.Transactions;
using System.Data.Entity;
using ERPKeeper.Node.Models.Accounting.Enums;

namespace ERPKeeper.Node.DAL.Accounting
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
            template.Uid = Guid.NewGuid();
            erpNodeDBContext.JournalEntryTypes.Add(template);
            erpNodeDBContext.SaveChanges();

            return template;
        }
    }
}