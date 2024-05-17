
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;

namespace ERPKeeperCore.Enterprise.DAL.Financial
{
    public class LendReturns : ERPNodeDalRepository
    {
        public LendReturns(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Models.Financial.LendReturn> GetAll()
        {
            return erpNodeDBContext.LendReturns.ToList();
        }



        public Models.Financial.LendReturn? Find(Guid Id) => erpNodeDBContext.LendReturns.Find(Id);

        public void UnPost(Models.Financial.LendReturn model)
        {
            throw new NotImplementedException();
        }


        public void PostToTransactions()
        {
            CreateTransactions();

            var models = erpNodeDBContext.LendReturns
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted)
                .OrderBy(s => s.Date).ToList();

            models.ForEach(model =>
            {
                model.PostToTransaction();
                erpNodeDBContext.SaveChanges();
            });
        }

        public void CreateTransactions()
        {
            var LendReturns = erpNodeDBContext
                .LendReturns
                .Where(s => s.TransactionId == null)
                .ToList();

            LendReturns.ForEach(LendReturn =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(LendReturn.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR LendReturn:{LendReturn.Name}");
                    transaction = new Transaction()
                    {
                        Id = LendReturn.Id,
                        Date = LendReturn.Date,
                        Reference = LendReturn.Name,
                        Type = Models.Accounting.Enums.TransactionType.LendReturn,
                        LendReturn = LendReturn,
                    };
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }
    }
}