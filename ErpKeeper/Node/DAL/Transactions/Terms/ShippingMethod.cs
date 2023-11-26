
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Financial;
using ERPKeeper.Node.Models.Financial.Transfer;
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.Models.Employees;
using ERPKeeper.Node.Models.Transactions.Commercials;

namespace ERPKeeper.Node.DAL.Transactions.Terms
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
            term.Uid = Guid.NewGuid();
            erpNodeDBContext.ShippingMethods.Add(term);

            return term;
        }
    }
}