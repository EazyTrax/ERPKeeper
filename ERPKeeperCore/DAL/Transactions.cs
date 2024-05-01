
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Items;
using ERPKeeperCore.Enterprise.Models.Items.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class Transactions : ERPNodeDalRepository
    {
        public Transactions(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Transaction> GetAll()
        {
            return erpNodeDBContext.Transactions.ToList();
        }


        public Transaction CreateTransaction(Guid Id, TransactionType Type)
        {
            var existTransaction = this.Find(Id);

            if (existTransaction == null)
            {
                existTransaction = new Transaction
                {
                    Id = Id,
                    Type = Type,
                    Date = DateTime.Now
                };

                this.organization.ErpCOREDBContext.Transactions.Add(existTransaction);
                this.SaveChanges();
            }
            return existTransaction;

        }
        public Transaction? Find(Guid Id) => erpNodeDBContext.Transactions.Find(Id);


    }
}