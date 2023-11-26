
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
    public class PaymentTerms : ERPNodeDalRepository
    {
        public PaymentTerms(Organization organization) : base(organization)
        {

        }


        public List<PaymentTerm> ListAll() => erpNodeDBContext.PaymentTerms.ToList();
        public PaymentTerm Find(Guid id) => erpNodeDBContext.PaymentTerms.Find(id);
        public IQueryable<PaymentTerm> Query() => erpNodeDBContext.PaymentTerms;

        public PaymentTerm CreateNew(PaymentTerm paymentTerm)
        {
            paymentTerm.Id = Guid.NewGuid();
            erpNodeDBContext.PaymentTerms.Add(paymentTerm);
            erpNodeDBContext.SaveChanges();

            return paymentTerm;
        }
    }
}