using ERPKeeper.Node.DAL;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Items.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPKeeperCore.CMD
{
    public partial class ERPMigrationTool
    {
        public void Copy_Assets()
        {
            Console.WriteLine("> Copy_Assets");

            Copy_Assets_AssetTypes();
            Copy_Assets_Assets();
            Copy_Assets_AssetDeprecreates();
        }

        public void Copy_Assets_AssetTypes()
        {
            Console.WriteLine("> Copy_Assets_AssetTypes");
            var oldModels = oldOrganization.ErpNodeDBContext.AssetTypes.ToList();

            oldModels.ForEach(a =>
            {
              
                var exist = newOrganization.ErpCOREDBContext.AssetTypes.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"PROF:{a.Name}-{a.Name}");

                    exist = new ERPKeeperCore.Enterprise.Models.Assets.AssetType()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Description = a.Description,
                        AccumulateDeprecateAccId = a.AccumulateDeprecateAccUid,
                        AmortizeExpenseAccId = a.AmortizeExpenseAccUid,
                        AwaitDeprecateAccId = a.AwaitDeprecateAccUid,
                        Purchase_Asset_AccountId = a.PurchaseAccUid,
                        CodePrefix = a.CodePrefix,
                        DeprecatedAble = a.DeprecatedAble,
                        DepreciationMethod = (Enterprise.Models.Assets.Enums.EnumDepreciationMethod)a.DepreciationMethod,
                        ScrapValue = a.ScrapValue,
                        UseFulLifeYear = a.UseFulLifeYear,
                    };

                    newOrganization.ErpCOREDBContext.AssetTypes.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        public void Copy_Assets_Assets()
        {
            Console.WriteLine("> Copy_Assets_Assets");
            var oldModels = oldOrganization.ErpNodeDBContext.Assets.ToList();

            oldModels.ForEach(a =>
            {
              
                var exist = newOrganization.ErpCOREDBContext.Assets.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"PROF:{a.Name}-{a.Name}");

                    exist = new ERPKeeperCore.Enterprise.Models.Assets.Asset()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        AssetTypeId = a.FixedAssetTypeUid,
                        AssetValue = a.AssetValue,
                        Code = a.Code,
                        DepreciationPerYear = a.DepreciationPerYear,
                        LastCreateSchedule = a.LastCreateSchedule,
                        Memo = a.Memo,
                        No = a.No,
                        PurchaseDate = a.StartDeprecationDate,
                        SavageValue = a.SavageValue,
                        Reference = a.Reference,
                        StartDeprecationDate = a.StartDeprecationDate,
                        Status = (Enterprise.Models.Assets.Enums.AssetStatus)a.Status,
                    };

                    newOrganization.ErpCOREDBContext.Assets.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        public void Copy_Assets_AssetDeprecreates()
        {
            Console.WriteLine("> Copy_Assets_AssetDeprecreates");
            var oldModels = oldOrganization.ErpNodeDBContext.DeprecateSchedules.ToList();

            oldModels.ForEach(a =>
            {
            
                var exist = newOrganization.ErpCOREDBContext.AssetDeprecateSchedules.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"PROF:{a.Name}-{a.Name}");

                    exist = new ERPKeeperCore.Enterprise.Models.Assets.AssetDeprecateSchedule()
                    {
                        Id = a.Uid,
                        AssetId = a.FixedAssetUid,
                        DepreciateAccumulation = a.DepreciateAccumulation,
                        DepreciateOffsetValue = a.DepreciateOffsetValue,
                        DepreciationValue = a.DepreciationValue,
                        EndDate = a.EndDate,
                        No = a.No,
                        OpeningValue = a.OpeningValue,
                        PostStatus = (Enterprise.Models.Accounting.Enums.LedgerPostStatus)a.PostStatus,
                        RemainValue = a.RemainValue,
                        StartDate = a.StartDate,
                    };

                    newOrganization.ErpCOREDBContext.AssetDeprecateSchedules.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
    }
}
