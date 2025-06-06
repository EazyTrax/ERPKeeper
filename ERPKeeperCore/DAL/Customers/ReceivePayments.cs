﻿
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Customers;

namespace ERPKeeperCore.Enterprise.DAL.Customers
{
    public class ReceivePayments : ERPNodeDalRepository
    {
        public ReceivePayments(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<ReceivePayment> GetList()
        {
            return erpNodeDBContext.ReceivePayments.ToList();
        }



        public ReceivePayment? Find(Guid Id) => erpNodeDBContext.ReceivePayments.Find(Id);

        public int Count()
        {
            return erpNodeDBContext.ReceivePayments.Count();
        }


        public void CreateTransactions()
        {
            var ReceivePayments = erpNodeDBContext
                .ReceivePayments
                .Where(s => s.TransactionId == null)
                .ToList();

            ReceivePayments.ForEach(ReceivePayment =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(ReceivePayment.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{ReceivePayment.Name}");
                    transaction = new Transaction()
                    {
                        Id = ReceivePayment.Id,
                        Date = ReceivePayment.Date,
                        Reference = ReceivePayment.Name,
                        Type = Models.Accounting.Enums.TransactionType.ReceivePayment,
                        ReceivePayment = ReceivePayment,
                    };

                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }


        public void PostToTransactions()
        {
            CreateTransactions();

            var ReceivePayments = erpNodeDBContext.ReceivePayments
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted)
                // .Where(s => s.AmountBankFee > 0)
                .OrderBy(s => s.Date).ToList();


            var DefaultCashAccount = organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.Cash);
            var DiscountAccount = organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.DiscountGiven);
            var ReceivableAccount = organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.Asset_AccountReceivable);
            var BankFeeAccount = organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.Expense_BankFee);


            ReceivePayments.ForEach(receivePayment =>
            {
                if (receivePayment.PayToAccount == null)
                {
                    Console.WriteLine($"Info >> Assign Default Account {receivePayment.Name}");
                    receivePayment.PayToAccount = DefaultCashAccount;
                    receivePayment.PayToAccountId = DefaultCashAccount.Id;
                    erpNodeDBContext.SaveChanges();
                }

                receivePayment.Discount_Given_Expense_Account = DiscountAccount;
                receivePayment.Discount_Given_Expense_AccountId = DiscountAccount.Id;
                receivePayment.Receivable_Asset_Account = ReceivableAccount;
                receivePayment.Receivable_Asset_AccountId = ReceivableAccount.Id;
                receivePayment.BankFee_Expense_Account = BankFeeAccount;
                receivePayment.BankFee_Expense_AccountId = BankFeeAccount.Id;

                erpNodeDBContext.SaveChanges();

                receivePayment.PostToTransaction();
                erpNodeDBContext.SaveChanges();
            });
        }

        public void UpdateRetentionGroups()
        {
            erpNodeDBContext.ReceivePayments
                   .Where(s => s.RetentionType != null && s.RetentionGroupId == null)
                   .ToList()
                   .ForEach(s =>
                   {
                       s.UpdateBalance();
                       s.RetentionGroup = organization.RetentionGroups.FindOrCreate(s.RetentionType, s.Date);
                   });

            erpNodeDBContext.ReceivePayments
                  .Where(s => s.RetentionType != null && s.RetentionGroupId != null)
                  .Where(s => s.RetentionTypeId != s.RetentionGroup.RetentionTypeId)
                  .ToList()
                  .ForEach(s =>
                  {
                      s.UpdateBalance();
                      s.RetentionGroup = organization.RetentionGroups.FindOrCreate(s.RetentionType, s.Date);
                  });



        }
    }
}