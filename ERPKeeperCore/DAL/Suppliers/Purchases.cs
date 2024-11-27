
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
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;

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

        public void Post_Ledgers(bool rePost = false)
        {
            CreateTransactions();

            var purchases = erpNodeDBContext.Purchases
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted || rePost)
                .Where(s => s.ExpenseAccountId != null)
                .OrderBy(s => s.Date).ToList();



            var PayableAccount = organization.SystemAccounts.AccountPayable;
            var IncomeAccount_DiscountTaken = organization.SystemAccounts.GetAccount(DefaultAccountType.Income_DiscountTaken);
            var ExpenseAccount = organization.SystemAccounts.Expense;

            int maxNo = purchases.Count();

            purchases.ForEach(purchase =>
            {
                purchase.AssignDefaultAccount(ExpenseAccount, IncomeAccount_DiscountTaken, PayableAccount);
                purchase.Post_Ledgers(maxNo--);

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
        public void UnPostAll()
        {
            var purchase = erpNodeDBContext.Purchases
                .Where(s => s.IsPosted)
                .Include(s => s.Transaction)
                .ThenInclude(s => s.Ledgers)
                .ToList();

            purchase.ForEach(purchase =>
            {
                purchase.UnPostLedger();
            });

            erpNodeDBContext.SaveChanges();
        }
        public void UpdateStatus()
        {
            var paidTransactions = erpNodeDBContext.Purchases
                .Where(s => s.Status != PurchaseStatus.Paid && s.SupplierPayment != null)
                .ToList();
            paidTransactions.ForEach(purchase => purchase.Status = PurchaseStatus.Paid);
            erpNodeDBContext.SaveChanges();

        }
        public Purchase Creat(Purchase model)
        {


            var maxNo = erpNodeDBContext.Purchases
                .Select(a => (int?)a.No)
                .Max() ?? 0;

            model.Date = DateTime.Today;
            model.Status = PurchaseStatus.Open;
            model.No = maxNo + 1;
            model.UpdateBalance();
            model.UpdateName();

            erpNodeDBContext.Purchases.Add(model);
            erpNodeDBContext.SaveChanges();

            return model;
        }

    }
}