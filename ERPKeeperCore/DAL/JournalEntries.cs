
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
            throw new NotImplementedException();
        }
    }
}