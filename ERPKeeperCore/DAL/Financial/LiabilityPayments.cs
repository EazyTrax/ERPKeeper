
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
    public class LiabilityPayments : ERPNodeDalRepository
    {
        public LiabilityPayments(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<LiabilityPayment> GetList()
        {
            return erpNodeDBContext.LiabilityPayments.ToList();
        }



        public LiabilityPayment? Find(Guid Id) => erpNodeDBContext.LiabilityPayments.Find(Id);

        public int Count()
        {
            return erpNodeDBContext.LiabilityPayments.Count();
        }


        public void CreateTransactions()
        {
            var LiabilityPayments = erpNodeDBContext
                .LiabilityPayments
                .Where(s => s.TransactionId == null)
                .ToList();

            LiabilityPayments.ForEach(LiabilityPayment =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(LiabilityPayment.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{LiabilityPayment.Name}");

                    transaction = new Transaction()
                    {
                        Id = LiabilityPayment.Id,
                        Date = LiabilityPayment.Date,
                        Reference = LiabilityPayment.Name,
                        Type = Models.Accounting.Enums.TransactionType.LiabilityPayment,
                        LiabilityPayment = LiabilityPayment,
                    };

                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }


        public void PostToLedgers()
        {


            RemoveUnreferenceTransactions();
            CreateTransactions();

            var LiabilityPayments = erpNodeDBContext.LiabilityPayments
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted)
                .OrderBy(s => s.Date).ToList();

            LiabilityPayments.ForEach(LiabilityPayment =>
            {
                LiabilityPayment.PostLedgers();
                erpNodeDBContext.SaveChanges();
            });

        }

        private void RemoveUnreferenceTransactions()
        {
            erpNodeDBContext.Transactions
                .Where(t => t.Type == Models.Accounting.Enums.TransactionType.LiabilityPayment && t.LiabilityPayment == null)
                .ToList()
                 .ForEach(a =>
                 {
                     Console.WriteLine($"Delete {a.Id} {a.Debit} {a.Credit}");
                     erpNodeDBContext.Transactions.Remove(a);
                     SaveChanges();
                 });
        }

        public void ClearEmpties()
        {
            erpNodeDBContext.LiabilityPaymentPayFromAccounts
                  .Where(a => a.Account == null || a.Amount == 0)
                  .ToList()
                  .ForEach(a =>
                  {
                      Console.WriteLine($"Delete {a.LiabilityPayment.Name} {a.Amount}");
                      erpNodeDBContext.LiabilityPaymentPayFromAccounts.Remove(a);
                      SaveChanges();
                  });

            erpNodeDBContext.LiabilityPayments
                 .Where(a => a.Amount == 0)
                 .ToList()
                 .ForEach(a =>
                 {
                     Console.WriteLine($"Delete {a.Name} {a.Amount}");
                     Console.WriteLine($"Delete {a.LiabilityPaymentPayFromAccounts.Sum(m => m.Amount)}");

                     erpNodeDBContext.LiabilityPayments.Remove(a);
                     SaveChanges();
                 });

            RemoveUnreferenceTransactions();
        }

        public void UnPostAll()
        {
            erpNodeDBContext.LiabilityPayments
                .Where(rg => rg.IsPosted)
                .ToList()
                .ForEach(rg => rg.UnPostLedger());

            erpNodeDBContext.SaveChanges();
        }

        public void Delete(LiabilityPayment lp)
        {
            lp.UnPostLedger();
            if (lp.Transaction != null)
                erpNodeDBContext.Transactions.Remove(lp.Transaction);
            erpNodeDBContext.LiabilityPayments.Remove(lp);
            erpNodeDBContext.SaveChanges();
        }

    }
}