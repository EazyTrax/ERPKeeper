
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
            term.Uid = Guid.NewGuid();
            erpNodeDBContext.ShippingTerms.Add(term);
            return term;
        }
    }
}