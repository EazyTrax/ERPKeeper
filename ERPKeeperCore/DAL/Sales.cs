
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class Sales : ERPNodeDalRepository
    {
        public Sales(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Sale> GetAll()
        {
            return erpNodeDBContext.Sales.ToList();
        }



        public Sale? Find(Guid Id) => erpNodeDBContext.Sales.Find(Id);

        public void CreateTransactions()
        {
            var sales = erpNodeDBContext
                .Sales
                .Where(s => s.TransactionId == null)
                .ToList();

            sales.ForEach(sale =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(sale.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{sale.Name}");
                    transaction = new Transaction()
                    {
                        Id = sale.Id,
                        Date = sale.Date,
                        Reference = sale.Name,
                        Type = Models.Accounting.Enums.TransactionType.Sale,
                        Sale = sale,
                    };
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }


        public void PostToTransactions()
        {
            var sales = erpNodeDBContext.Sales
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted)
                .OrderBy(s => s.Date).ToList();

            sales.ForEach(sale =>
            {
                if (sale.ReceivableAccount == null)
                {
                    sale.ReceivableAccount = organization.SystemAccounts.AccountReceivable;
                    sale.ReceivableAccountId = sale.ReceivableAccount.Id;
                }

                sale.PostToTransaction();
                erpNodeDBContext.SaveChanges();
            });

        }
    }
}