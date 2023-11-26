
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Reports.CompanyandFinancial;
using ERPKeeper.Node.Models.Accounting.Enums;

namespace ERPKeeper.Node.DAL.Accounting
{
    public class Journals : ERPNodeDalRepository
    {

        public Journals(Organization organization) : base(organization)
        {

        }

        public IQueryable<Models.Accounting.TransactionLedger> GetAll()
        {
            return erpNodeDBContext.TransactionLedgers;
        }

        public object Find(Guid id) => erpNodeDBContext.TransactionLedgers.Find(id);
    }
}