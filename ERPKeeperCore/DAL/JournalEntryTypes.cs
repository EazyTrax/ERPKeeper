
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
    public class JournalEntryTypes : ERPNodeDalRepository
    {
        public JournalEntryTypes(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<JournalEntryType> GetAll()
        {
            return erpNodeDBContext.JournalEntryTypes.ToList();
        }



        public JournalEntryType? Find(Guid Id) => erpNodeDBContext.JournalEntryTypes.Find(Id);

        public void UnPost(JournalEntryType model)
        {
            throw new NotImplementedException();
        }
    }
}