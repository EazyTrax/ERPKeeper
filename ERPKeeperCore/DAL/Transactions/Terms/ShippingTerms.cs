
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Financial;
using KeeperCore.ERPNode.Models.Financial.Transfer;
using KeeperCore.ERPNode.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.Employees;
using KeeperCore.ERPNode.Models.Transactions.Commercials;

namespace KeeperCore.ERPNode.DAL.Transactions.Terms
{
    public class ShippingTerms : ERPNodeDalRepository
    {
        public ShippingTerms(Organization organization) : base(organization)
        {

        }


        public List<ShippingTerm> ListAll() => erpNodeDBContext.ShippingTerms.ToList();
        public ShippingTerm Find(Guid id) => erpNodeDBContext.ShippingTerms.Find(id);
        public IQueryable<ShippingTerm> Query() => erpNodeDBContext.ShippingTerms;

        public ShippingTerm CreateNew(ShippingTerm term)
        {
            term.Id = Guid.NewGuid();
            erpNodeDBContext.ShippingTerms.Add(term);
            return term;
        }
    }
}