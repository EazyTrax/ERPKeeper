
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Assets;
using ERPKeeperCore.Enterprise.Models.Assets.Enums;

namespace ERPKeeperCore.Enterprise.DAL.Assets
{
    public class AssetDeprecateSchedules : ERPNodeDalRepository
    {
        public AssetDeprecateSchedules(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<AssetDeprecateSchedule> GetAll()
        {
            return erpNodeDBContext.AssetDeprecateSchedules.ToList();
        }
        public int Count()
        {
            return erpNodeDBContext.AssetDeprecateSchedules.Count();
        }



        public AssetDeprecateSchedule? Find(Guid Id) => erpNodeDBContext.AssetDeprecateSchedules.Find(Id);

        public void PostToTransactions()
        {
            CreateTransactions();

            var assetDeprecateSchedules = erpNodeDBContext.AssetDeprecateSchedules
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted)
                .OrderBy(s => s.StartDate).ToList();

            assetDeprecateSchedules.ForEach(assetDeprecateSchedule =>
            {
                assetDeprecateSchedule.PostToTransaction();
                erpNodeDBContext.SaveChanges();
            });
        }
        public void CreateTransactions()
        {
            var assetDeprecateSchedules = erpNodeDBContext
                .AssetDeprecateSchedules
                .Where(s => s.TransactionId == null)
                .ToList();

            assetDeprecateSchedules.ForEach(assetDeprecateSchedule =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(assetDeprecateSchedule.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{assetDeprecateSchedule.Name}");
                    transaction = new Transaction()
                    {
                        Id = assetDeprecateSchedule.Id,
                        Date = assetDeprecateSchedule.StartDate,
                        Reference = assetDeprecateSchedule.Name,
                        Type = Models.Accounting.Enums.TransactionType.AssetDeprecateSchedule,
                        AssetDeprecateSchedule = assetDeprecateSchedule,
                    };
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }
    }
}