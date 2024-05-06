
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class Lends : ERPNodeDalRepository
    {
        public Lends(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Models.Financial.Lend> GetAll()
        {
            return erpNodeDBContext.Lends.ToList();
        }



        public Models.Financial.Lend? Find(Guid Id) => erpNodeDBContext.Lends.Find(Id);

        public void UnPost(Models.Financial.Lend model)
        {
            throw new NotImplementedException();
        }


        public void PostToTransactions()
        {
            this.CreateTransactions();

            var incomeTaxes = erpNodeDBContext.Lends
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted)
                .OrderBy(s => s.Date).ToList();

            incomeTaxes.ForEach(incomeTax =>
            {
                incomeTax.PostToTransaction();
                erpNodeDBContext.SaveChanges();
            });
        }

        public void CreateTransactions()
        {
            var lends = erpNodeDBContext
                .Lends
                .Where(s => s.TransactionId == null)
                .ToList();

            lends.ForEach(lend =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(lend.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{lend.Name}");
                    transaction = new Transaction()
                    {
                        Id = lend.Id,
                        Date = lend.Date,
                        Reference = lend.Name,
                        Type = Models.Accounting.Enums.TransactionType.Lend,
                        Lend = lend,
                    };
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }
    }
}