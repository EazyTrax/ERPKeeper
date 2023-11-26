
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using ERPKeeper.Node.Models.Transactions.Commercials;
using ERPKeeper.Node.Models.Financial;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Financial.Payments;
using ERPKeeper.Node.Models.Financial.Payments.Enums;
using ERPKeeper.Node.Models.Profiles;

namespace ERPKeeper.Node.DAL.Financial
{
    public class GeneralPayments : ERPNodeDalRepository
    {
        public GeneralPayments(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.GeneralPayment;
        }

        public GeneralPayment Find(Guid id) => erpNodeDBContext.GeneralPayments.Find(id);
        public IQueryable<GeneralPayment> Query() => erpNodeDBContext.GeneralPayments;

        public void UpdateBalance()
        {
            erpNodeDBContext.GeneralPayments.ToList().ForEach(q => q.CalculateAmount());
            organization.SaveChanges();
        }

        internal void UpdateDoumentsName()
        {
            erpNodeDBContext.GeneralPayments.ToList().ForEach(c =>
            {
                Console.Write("!");
                c.UpdateName();
            });
            erpNodeDBContext.SaveChanges();
        }
    }
}
