
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

namespace ERPKeeperCore.Enterprise.DAL.ObsoleteAssets
{
    public class ObsoleteAssets : ERPNodeDalRepository
    {
        public ObsoleteAssets(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<ObsoleteAsset> GetAll()
        {
            return erpNodeDBContext.ObsoleteAssets.ToList();
        }
        public int Count()
        {
            return erpNodeDBContext.ObsoleteAssets.Count();
        }



        public ObsoleteAsset? Find(Guid Id) => erpNodeDBContext.ObsoleteAssets.Find(Id);

        public void PostToTransactions()
        {
            Console.WriteLine(">Post OBS Assets");
            CreateTransactions();

            var ObsoleteAssets = erpNodeDBContext.ObsoleteAssets
              .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted)
                .OrderBy(s => s.ObsoleteDate)
                .ToList();

            ObsoleteAssets.ForEach(Asset =>
            {
                Console.WriteLine($">Post OBS Assets {Asset.Asset.Name}");
                Asset.PostToTransaction();
                erpNodeDBContext.SaveChanges();
            });
        }
        public void CreateTransactions()
        {
            var ObsoleteAssets = erpNodeDBContext
                .ObsoleteAssets
                .Where(s => s.TransactionId == null)
                .ToList();

            ObsoleteAssets.ForEach(obs =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(obs.Id);

                if (transaction == null)
                {
                    Console.WriteLine($">Create TR >OBS > {obs.Asset.Name}");

                    transaction = new Transaction()
                    {
                        Id = obs.Id,
                        Date = obs.ObsoleteDate,
                        Reference = "OBS:" + obs.Asset.Name,
                        Type = Models.Accounting.Enums.TransactionType.ObsoleteAsset,
                        ObsoleteAsset = obs,
                    };
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
                else
                {

                }
            });

        }


    }
}