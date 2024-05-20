
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

namespace ERPKeeperCore.Enterprise.DAL.Taxes
{
    public class TaxPeriods : ERPNodeDalRepository
    {
        public TaxPeriods(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<TaxPeriod> ListAll()
        {
            return erpNodeDBContext.TaxPeriods.ToList();
        }

        public TaxPeriod? Find(Guid Id) => erpNodeDBContext.TaxPeriods.Find(Id);

        public void Refresh()
        {
            var taxPeriods = erpNodeDBContext.TaxPeriods.ToList();

            foreach (var taxPeriod in taxPeriods)
            {
                taxPeriod.UpdateBalance();
                erpNodeDBContext.SaveChanges();
            }
        }

        public int Count()
        {
            return erpNodeDBContext.TaxPeriods.Count();
        }

        public void PostToTransactions(bool rePost = false)
        {
            this.CreateTransactions();

            var TaxPeriods = erpNodeDBContext.TaxPeriods
                .Where(s => s.TransactionId != null)
              //  .Where(s => s.Status != TaxPeriodStatus.Draft)
                .Where(s => !s.IsPosted || rePost)
                .ToList()
                .OrderBy(s => s.EndDate)
                .ToList();

            TaxPeriods.ForEach(TaxPeriod =>
            {

                TaxPeriod.PostToTransaction();
                erpNodeDBContext.SaveChanges();
            });
        }

        public void CreateTransactions()
        {
            var TaxPeriods = erpNodeDBContext
                .TaxPeriods
                .Where(s => s.TransactionId == null)
                .ToList();

            TaxPeriods.ForEach(TaxPeriod =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(TaxPeriod.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{TaxPeriod.Name}");
                    transaction = new Transaction()
                    {
                        Id = TaxPeriod.Id,
                        Date = TaxPeriod.EndDate,
                        Reference = TaxPeriod.Name,
                        Type = Models.Accounting.Enums.TransactionType.TaxPeriod,
                        TaxPeriod = TaxPeriod,
                    };
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }
    }
}