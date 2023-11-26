
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
            paymentTerm.Uid = Guid.NewGuid();
            erpNodeDBContext.PaymentTerms.Add(paymentTerm);
            erpNodeDBContext.SaveChanges();

            return paymentTerm;
        }
    }
}