
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Financial;

namespace ERPKeeperCore.Enterprise.DAL.Financial
{
    public class RetentionPeriods : ERPNodeDalRepository
    {
        public RetentionPeriods(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Models.Financial.RetentionPeriod> GetAll()
        {
            return erpNodeDBContext.RetentionPeriods.ToList();
        }



        public Models.Financial.RetentionPeriod? Find(Guid Id)
            => erpNodeDBContext.RetentionPeriods.Find(Id);

        public void UnPost(Models.Financial.RetentionPeriod model)
        {
            model.Transaction.ClearLedger();
            model.IsPosted = false;

            erpNodeDBContext.SaveChanges();
        }


        public void PostToTransactions()
        {
            CreateTransactions();

            var models = erpNodeDBContext.RetentionPeriods
                .Where(s => s.TransactionId != null)
                .Where(s => s.RetentionType.RetentionToAccount.Type == Models.Accounting.Enums.AccountTypes.Liability)
                .Where(s => !s.IsPosted)
                .OrderBy(s => s.Date).ToList();

            models.ForEach(model =>
            {
                if (model.PayDate == null)
                    model.PayDate = model.EndDate;

                if (model.PayFromAccount == null)
                    model.PayFromAccount = erpNodeDBContext.DefaultAccounts.Find(Models.Accounting.Enums.DefaultAccountType.Cash)?.Account;
                erpNodeDBContext.SaveChanges();


                model.PostToTransaction();
                erpNodeDBContext.SaveChanges();

            });
        }

        public void CreateTransactions()
        {
            var RetentionGroups = erpNodeDBContext
                .RetentionPeriods
                .Where(s => s.TransactionId == null)
                .ToList();

            Console.WriteLine($"Create TR:{RetentionGroups.Count}");

            RetentionGroups.ForEach(RetentionGroup =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(RetentionGroup.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{RetentionGroup.Name}");
                    transaction = new Transaction()
                    {
                        Id = RetentionGroup.Id,
                        Date = RetentionGroup.Date,
                        Reference = RetentionGroup.Name,
                        Type = Models.Accounting.Enums.TransactionType.RetentionGroup,
                        RetentionGroup = RetentionGroup,
                    }; ;
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }

        public RetentionPeriod? FindOrCreate(RetentionType retentionType, DateTime date)
        {
            if (retentionType == null)
                return null;

            Console.WriteLine($"Find RetentionGroup:{retentionType?.Name} {date}");

            var rgs = erpNodeDBContext.RetentionPeriods
                .Where(s => s.RetentionTypeId == retentionType.Id)
                .ToList();

            var rg = rgs
                .Where(rg => rg.StartDate.Date <= date.Date && rg.EndDate.Date >= date)
                .FirstOrDefault();

            if (rg == null && retentionType != null)
            {
                rg = new RetentionPeriod()
                {
                    Id = Guid.NewGuid(),
                    Date = date,
                    RetentionTypeId = retentionType.Id,
                    RetentionType = retentionType,
                    Name = $"{retentionType.Name} {date:yyyy-MM}",
                    IsPosted = false,
                    No = erpNodeDBContext.RetentionPeriods.Max(x => x.No) + 1
                };

                erpNodeDBContext.RetentionPeriods.Add(rg);
                erpNodeDBContext.SaveChanges();
            }

            Console.WriteLine($"Found RetentionGroup:{rg?.Id}");
            return rg;
        }

        public void UnPostAll()
        {
            erpNodeDBContext.RetentionPeriods
                .Where(rg => rg.IsPosted)
                .ToList()
                .ForEach(rg => UnPost(rg));

            erpNodeDBContext.SaveChanges();
        }

        public void UpdateCount()
        {
            erpNodeDBContext.RetentionPeriods.ToList()
                .ForEach(rg =>
                    {
                        rg.Count = rg.ReceivePayments.Count + rg.SupplierPayments.Count();
                    });
            erpNodeDBContext.SaveChanges();
        }
    }
}