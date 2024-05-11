
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
    public class IncomeTaxes : ERPNodeDalRepository
    {
        public IncomeTaxes(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Models.Taxes.IncomeTax> GetAll()
        {
            return erpNodeDBContext.IncomeTaxes.ToList();
        }



        public Models.Taxes.IncomeTax? Find(Guid Id) => erpNodeDBContext.IncomeTaxes.Find(Id);

        public void UnPost(Models.Taxes.IncomeTax model)
        {
            throw new NotImplementedException();
        }
        public int Count()
        {
            return erpNodeDBContext.IncomeTaxes.Count();
        }

        public void PostToTransactions()
        {
            this.CreateTransactions();

            var incomeTaxes = erpNodeDBContext.IncomeTaxes
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted)
                .OrderBy(s => s.Date).ToList();

            incomeTaxes.ForEach(incomeTax =>
            {
                if (incomeTax.WriteOff_TaxReceiveable_ExpenseAccount == null)
                {
                    incomeTax.WriteOff_TaxReceiveable_ExpenseAccount
                    = organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.WriteOff_TaxReceiveable_Expense);
                    incomeTax.WriteOff_TaxReceiveable_ExpenseAccountId = incomeTax.WriteOff_TaxReceiveable_ExpenseAccount.Id;
                }

                incomeTax.PostToTransaction();
                erpNodeDBContext.SaveChanges();
            });
        }

        public void CreateTransactions()
        {
            var incomeTaxes = erpNodeDBContext
                .IncomeTaxes
                .Where(s => s.TransactionId == null)
                .ToList();

            incomeTaxes.ForEach(incomeTax =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(incomeTax.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{incomeTax.Name}");
                    transaction = new Transaction()
                    {
                        Id = incomeTax.Id,
                        Date = incomeTax.Date,
                        Reference = incomeTax.Name,
                        Type = Models.Accounting.Enums.TransactionType.IncomeTax,
                        IncomeTax = incomeTax,
                    };
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }
    }
}