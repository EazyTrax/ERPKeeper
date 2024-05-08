
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
    public class Loans : ERPNodeDalRepository
    {
        public Loans(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Models.Financial.Loan> GetAll()
        {
            return erpNodeDBContext.Loans.ToList();
        }



        public Models.Financial.Loan? Find(Guid Id) => erpNodeDBContext.Loans.Find(Id);

        public void UnPost(Models.Financial.Loan model)
        {
            throw new NotImplementedException();
        }


        public void PostToTransactions()
        {
            CreateTransactions();

            var models = erpNodeDBContext.Loans
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
            var loans = erpNodeDBContext
                .Loans
                .Where(s => s.TransactionId == null)
                .ToList();

            loans.ForEach(loan =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(loan.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{loan.Name}");
                    transaction = new Transaction()
                    {
                        Id = loan.Id,
                        Date = loan.Date,
                        Reference = loan.Name,
                        Type = Models.Accounting.Enums.TransactionType.Loan,
                        Loan = loan,
                    };
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }
    }
}