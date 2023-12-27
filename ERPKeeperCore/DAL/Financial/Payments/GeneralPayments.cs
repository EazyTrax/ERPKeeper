
using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KeeperCore.ERPNode.Models.Transactions.Commercials;
using KeeperCore.ERPNode.Models.Financial;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Financial.Payments.Enums;
using KeeperCore.ERPNode.Models.Profiles;

namespace KeeperCore.ERPNode.DAL.Financial
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
