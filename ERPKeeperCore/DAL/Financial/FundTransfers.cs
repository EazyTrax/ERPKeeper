
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Taxes.Enums;
using ERPKeeperCore.Enterprise.Models.Financial;

namespace ERPKeeperCore.Enterprise.DAL.Financial
{
    public class FundTransfers : ERPNodeDalRepository
    {
        public FundTransfers(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<FundTransfer> GetList()
        {
            return erpNodeDBContext.FundTransfers.ToList();
        }



        public FundTransfer? Find(Guid Id) => erpNodeDBContext.FundTransfers.Find(Id);

        public int Count()
        {
            return erpNodeDBContext.FundTransfers.Count();
        }


        public void CreateTransactions()
        {
            var fundTransfers = erpNodeDBContext
                .FundTransfers
                .Where(s => s.TransactionId == null)
                .ToList();

            fundTransfers.ForEach(fundTransfer =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(fundTransfer.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{fundTransfer.Name}");
                    transaction = new Transaction()
                    {
                        Id = fundTransfer.Id,
                        Date = fundTransfer.Date,
                        Reference = fundTransfer.Name,
                        Type = Models.Accounting.Enums.TransactionType.FundTransfer,
                        FundTransfer = fundTransfer,
                    };

                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }


        public void PostToTransactions()
        {
            CreateTransactions();

            var fundTransfers = erpNodeDBContext.FundTransfers
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted)
                .OrderBy(s => s.Date).ToList();

            fundTransfers.ForEach(fundTransfer =>
            {
                if (fundTransfer.BankFeeAmount > 0)
                    fundTransfer.BankFee_Expense_Account = organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.Expense_BankFee);

                fundTransfer.PostToTransaction();
                erpNodeDBContext.SaveChanges();
            });

        }

        public void UnPost(FundTransfer model)
        {
            model.Transaction.ClearLedger();
            model.IsPosted = false;

            erpNodeDBContext.SaveChanges();
        }
    }
}