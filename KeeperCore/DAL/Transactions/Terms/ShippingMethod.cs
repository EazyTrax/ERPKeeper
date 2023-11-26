
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
    public class ShippingMethods : ERPNodeDalRepository
    {
        public ShippingMethods(Organization organization) : base(organization)
        {

        }


        public List<ShippingMethod> ListAll() => erpNodeDBContext.ShippingMethods.ToList();
        public ShippingMethod Find(Guid id) => erpNodeDBContext.ShippingMethods.Find(id);
        public IQueryable<ShippingMethod> Query() => erpNodeDBContext.ShippingMethods;

        public ShippingMethod CreateNew(ShippingMethod term)
        {
            term.Id = Guid.NewGuid();
            erpNodeDBContext.ShippingMethods.Add(term);

            return term;
        }
    }
}