
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Suppliers;

namespace ERPKeeperCore.Enterprise.DAL.Suppliers
{
    public class SupplierPayments : ERPNodeDalRepository
    {
        public SupplierPayments(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<SupplierPayment> GetList()
        {
            return erpNodeDBContext.SupplierPayments.ToList();
        }



        public SupplierPayment? Find(Guid Id) => erpNodeDBContext.SupplierPayments.Find(Id);

        public int Count()
        {
            return erpNodeDBContext.SupplierPayments.Count();
        }


        public void CreateTransactions()
        {
            var SupplierPayments = erpNodeDBContext
                .SupplierPayments
                .Where(s => s.TransactionId == null)
                .ToList();

            SupplierPayments.ForEach(SupplierPayment =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(SupplierPayment.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{SupplierPayment.Name}");
                    transaction = new Transaction()
                    {
                        Id = SupplierPayment.Id,
                        Date = SupplierPayment.Date,
                        Reference = SupplierPayment.Name,
                        Type = Models.Accounting.Enums.TransactionType.SupplierPayment,
                        SupplierPayment = SupplierPayment,
                    };

                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }


        public void PostToTransactions(bool rePost = false)
        {
            CreateTransactions();

            var SupplierPayments = erpNodeDBContext.SupplierPayments
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted || rePost)
                .Include(s => s.Transaction)
                .ThenInclude(s => s.Ledgers)
                .OrderBy(s => s.Date)
                .ToList();

            var DiscountTaken_IncomeAccount = organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.Income_DiscountTaken);
            var SupplierPayable_LiabilityAccount = organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.Liability_AccountPayable);
            var BankFee_ExpenseAccount = organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.Expense_BankFee);


            SupplierPayments.ForEach(payment =>
            {
                payment.IncomeAccount_DiscountTaken = DiscountTaken_IncomeAccount;
                payment.LiablityAccount_SupplierPayable = SupplierPayable_LiabilityAccount;
                payment.ExpenseAccount_BankFee = BankFee_ExpenseAccount;
                payment.Post_Ledgers();
            });
            erpNodeDBContext.SaveChanges();
        }


        public void UnPostAll()
        {
            var payents = erpNodeDBContext.SupplierPayments
                .Where(s => s.IsPosted)
                .Include(s => s.Transaction)
                .ThenInclude(s => s.Ledgers)
                .ToList();

            payents.ForEach(payment =>
            {
                payment.UnPostLedger();
            });

            erpNodeDBContext.SaveChanges();
        }

        public void UpdateRetentionGroups()
        {
            erpNodeDBContext.SupplierPayments
                   .Where(s => s.RetentionType != null && s.RetentionGroupId == null)
                   .ToList()
                   .ForEach(s =>
                   {
                       s.UpdateBalance();
                       s.RetentionGroup = organization.RetentionGroups.FindOrCreate(s.RetentionType, s.Date);
                   });
        }
    }
}