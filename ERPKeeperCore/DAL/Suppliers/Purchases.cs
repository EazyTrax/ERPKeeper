
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Suppliers;
using ERPKeeperCore.Enterprise.Models.Suppliers.Enums;

namespace ERPKeeperCore.Enterprise.DAL.Suppliers
{
    public class Purchases : ERPNodeDalRepository
    {
        public Purchases(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Purchase> GetAll()
        {
            return erpNodeDBContext.Purchases.ToList();
        }



        public Purchase? Find(Guid Id) => erpNodeDBContext.Purchases.Find(Id);

        public void PostToTransactions(bool rePost = false)
        {
            CreateTransactions();

            var purchases = erpNodeDBContext.Purchases
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted || rePost)
                .OrderBy(s => s.Date).ToList();

            purchases.ForEach(purchase =>
            {
                if (purchase.PayableAccount == null)
                {
                    purchase.PayableAccount = organization.SystemAccounts.AccountPayable;
                    purchase.PayableAccountId = purchase.PayableAccount.Id;
                }
                if (purchase.IncomeAccount_DiscountTaken == null && purchase.Discount > 0)
                {
                    purchase.IncomeAccount_DiscountTaken = organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.Income_DiscountTaken);
                    purchase.IncomeAccount_DiscountTakenId = purchase.IncomeAccount_DiscountTaken.Id;
                }

                purchase.PostToTransaction();
                erpNodeDBContext.SaveChanges();
            });
        }
        public void CreateTransactions()
        {
            var purchases = erpNodeDBContext
                .Purchases
                .Where(s => s.TransactionId == null)
                .ToList();

            purchases.ForEach(purchase =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(purchase.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{purchase.Name}");
                    transaction = new Transaction()
                    {
                        Id = purchase.Id,
                        Date = purchase.Date,
                        Reference = purchase.Name,
                        Type = Models.Accounting.Enums.TransactionType.Purchase,
                        Purchase = purchase,
                    };
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }

        public void UnPost(Purchase model)
        {
            model.Transaction.ClearLedger();
            model.IsPosted = false;

            erpNodeDBContext.SaveChanges();
        }

        public void New(Purchase model)
        {
            var currentYear = model.Date.Year;
            var currentMonth = model.Date.Month;

            var maxNo = erpNodeDBContext.Purchases
                .Where(a => a.Date.Year == currentYear && a.Date.Month == currentMonth)
                .Select(a => (int?)a.No)
                .Max() ?? 0;

            model.No = maxNo + 1;
            model.UpdateBalance();

            erpNodeDBContext.Purchases.Add(model);
            erpNodeDBContext.SaveChanges();
        }

       
    }
}