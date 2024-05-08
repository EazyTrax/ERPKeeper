
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


        public void PostToTransactions()
        {
            CreateTransactions();

            var LiabilityPayments = erpNodeDBContext.LiabilityPayments
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted)
                .OrderBy(s => s.Date).ToList();

            LiabilityPayments.ForEach(LiabilityPayment =>
            {
                LiabilityPayment.PostToTransaction();
                erpNodeDBContext.SaveChanges();
            });

        }
    }
}