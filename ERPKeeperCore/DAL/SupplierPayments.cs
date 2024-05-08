
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

namespace ERPKeeperCore.Enterprise.DAL
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


        public void PostToTransactions()
        {
            this.CreateTransactions();

            var SupplierPayments = erpNodeDBContext.SupplierPayments
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted)
               // .Where(s => s.AmountBankFee > 0)
                .OrderBy(s => s.Date).ToList();

            var DiscountAccount = organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.DiscountGiven);
            var ReceivableAccount = organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.AccountReceivable);
            var BankFeeAccount = organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.BankFee);


            SupplierPayments.ForEach(receivePayment =>
            {
                //receivePayment.DiscountAccount = DiscountAccount;
                //receivePayment.DiscountAccountId = DiscountAccount.Id;

                //receivePayment.ReceivableAccount = ReceivableAccount;
                //receivePayment.ReceivableAccountId = ReceivableAccount.Id;

                //receivePayment.BankFeeAccount = BankFeeAccount;
                //receivePayment.BankFeeAccountId = BankFeeAccount.Id;

                //erpNodeDBContext.SaveChanges();

                //receivePayment.PostToTransaction();
                //erpNodeDBContext.SaveChanges();
            });
        }
    }
}