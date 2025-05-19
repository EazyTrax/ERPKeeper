
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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public List<TaxPeriod> GetByFiscal(FiscalYear fs)
        {
            return erpNodeDBContext.TaxPeriods
                .Where(x => x.StartDate.Year == fs.StartDate.Year)
                .ToList();
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

        public void UnPostToTransactions(TaxPeriod taxPeriod)
        {
            taxPeriod.UpdateBalance();
            taxPeriod.UnPostLedger();
            this.SaveChanges();
        }

        public void PostToTransactions(bool rePost = false)
        {
            this.CreateTransactions();

            var TaxPeriods = erpNodeDBContext.TaxPeriods
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

        public TaxPeriod Find(DateTime date, bool createNew = false)
        {
            var firstDateOfMonth = new DateTime(date.Year, date.Month, 1);
            var endDateOfMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddMinutes(-1);

            // Fetch data from the database without using the calculated property
            var taxPeriod = erpNodeDBContext.TaxPeriods
                .Where(s => s.StartDate <= date)
                .AsEnumerable() // Switch to client-side evaluation
                .Where(s => s.EndDate >= date)
                .Where(s => s.PurchasesCount > 0)
                .FirstOrDefault();

            if (taxPeriod == null && createNew)
            {
                taxPeriod = new TaxPeriod()
                {
                    StartDate = firstDateOfMonth,
                    Status = TaxPeriodStatus.Draft,
                    IsPosted = false,
                };
            }

            return taxPeriod;
        }

        public void UnPostToTransactions()
        {
            var TaxPeriods = erpNodeDBContext.TaxPeriods
                .Where(s => s.TransactionId != null)
                .Where(s => s.IsPosted)
                .ToList();
            TaxPeriods.ForEach(TaxPeriod =>
            {
                TaxPeriod.UnPostLedger();
                erpNodeDBContext.SaveChanges();
            });
        }
    }
}