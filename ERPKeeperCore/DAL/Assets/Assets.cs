
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
    public class Assets : ERPNodeDalRepository
    {
        public Assets(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Asset> GetAll()
        {
            return erpNodeDBContext.Assets.ToList();
        }
        public int Count()
        {
            return erpNodeDBContext.Assets.Count();
        }



        public Asset? Find(Guid Id) => erpNodeDBContext.Assets.Find(Id);

        public void PostToTransactions()
        {
            CreateTransactions();

            var Assets = erpNodeDBContext.Assets
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted)
                .OrderBy(s => s.PurchaseDate).ToList();

            Assets.ForEach(Asset =>
            {
                Asset.PostToTransaction();
                erpNodeDBContext.SaveChanges();
            });
        }
        public void UnPostToTransactions()
        {
            var Assets = erpNodeDBContext.Assets
                .Where(s => s.TransactionId != null)
                .Where(s => s.IsPosted)
                .ToList();

            Assets.ForEach(Asset =>
            {
                Asset.UnPostLedger();
                erpNodeDBContext.SaveChanges();
            });
        }


        public void CreateTransactions()
        {
            var Assets = erpNodeDBContext
                .Assets
                .Where(s => s.TransactionId == null)
                .ToList();

            Assets.ForEach(purchase =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(purchase.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{purchase.Name}");
                    transaction = new Transaction()
                    {
                        Id = purchase.Id,
                        Date = purchase.PurchaseDate,
                        Reference = purchase.Name,
                        Type = Models.Accounting.Enums.TransactionType.Asset,
                        Asset = purchase,
                    };
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }

        public void Refresh()
        {


            foreach (var asset in erpNodeDBContext.Assets.ToList())
            {

                if (asset.AssetType.UseFulLifeYear == 0)
                    asset.SavageValue = asset.AssetValue;
                else if (asset.AssetType.UseFulLifeYear != 0 && asset.SavageValue == 0)
                    asset.SavageValue = 1;

                if (asset.Status == AssetStatus.Sold)
                    return;

                if (DateTime.Today > asset.EndDeprecationDate)
                    asset.Status = AssetStatus.Depreciated;

                if (DateTime.Today <= asset.EndDeprecationDate)
                    asset.Status = AssetStatus.PartialDeplicate;
            }

            erpNodeDBContext.SaveChanges();
        }
    }
}