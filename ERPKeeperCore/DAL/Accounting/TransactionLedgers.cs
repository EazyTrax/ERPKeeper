
using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Reports.CompanyandFinancial;
using KeeperCore.ERPNode.Models.Accounting.Enums;

namespace KeeperCore.ERPNode.DAL.Accounting
{
    public class TransactionLedgers : ERPNodeDalRepository
    {

        public TransactionLedgers(Organization organization) : base(organization)
        {

        }

        public IQueryable<Models.Accounting.TransactionLedger> GetAll()
        {
            return erpNodeDBContext.TransactionLedgers;
        }

        public object Find(Guid id) => erpNodeDBContext.TransactionLedgers.Find(id);
    }
}