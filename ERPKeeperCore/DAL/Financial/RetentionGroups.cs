
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
    public class RetentionGroups : ERPNodeDalRepository
    {
        public RetentionGroups(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Models.Financial.RetentionGroup> GetAll()
        {
            return erpNodeDBContext.RetentionGroups.ToList();
        }



        public Models.Financial.RetentionGroup? Find(Guid Id) => erpNodeDBContext.RetentionGroups.Find(Id);

        public void UnPost(Models.Financial.RetentionGroup model)
        {
            model.Transaction.ClearLedger();
            model.IsPosted = false;

            erpNodeDBContext.SaveChanges();
        }


        public void PostToTransactions()
        {
            CreateTransactions();

            var models = erpNodeDBContext.RetentionGroups
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
                .RetentionGroups
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
                    };
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }

        public RetentionGroup? FindOrCreate(RetentionType? retentionType, DateTime date)
        {
            Console.WriteLine($"Find RetentionGroup:{retentionType?.Name} {date}");

            var rgs = erpNodeDBContext.RetentionGroups
                .Where(s => s.RetentionTypeId == retentionType.Id)
                .ToList();

            var rg = rgs.Where(rg => rg.StartDate.Date <= date.Date && rg.EndDate.Date >= date).FirstOrDefault();

            if (rg == null)
            {
                rg = new RetentionGroup()
                {
                    Id = Guid.NewGuid(),
                    Date = date,
                    RetentionTypeId = retentionType.Id,
                    RetentionType = retentionType,
                    Name = $"{retentionType.Name} {date:yyyy-MM}",
                    IsPosted = false,
                };

                erpNodeDBContext.RetentionGroups.Add(rg);
                erpNodeDBContext.SaveChanges();
            }

            Console.WriteLine($"Found RetentionGroup:{rg?.Id}");
            return rg;
        }

        public void UnPostAll()
        {
            erpNodeDBContext.RetentionGroups
                .Where(rg => rg.IsPosted)
                .ToList()
                .ForEach(rg => UnPost(rg));

            erpNodeDBContext.SaveChanges();
        }
    }
}