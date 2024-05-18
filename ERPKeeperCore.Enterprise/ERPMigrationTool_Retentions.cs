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
        private void CopyRetentions_Types()
        {
            var existModelIds = newOrganization.ErpCOREDBContext.RetentionTypes
            .Select(x => x.Id)
            .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.RetentionTypes
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();


            oldModels.ForEach(oldModel =>
            {
                Console.WriteLine($"RetentionTypes:{oldModel.Name}-{oldModel.Uid}");
                var exist = newOrganization.ErpCOREDBContext
                .RetentionTypes
                .FirstOrDefault(x => x.Id == oldModel.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"> Add");

                    exist = new ERPKeeperCore.Enterprise.Models.Financial.RetentionType()
                    {
                        Id = oldModel.Uid,
                        IsActive = oldModel.IsActive,
                        Description = oldModel.Description ?? "NA",
                        Name = oldModel.Name,
                        Direction = (Enterprise.Models.Financial.Enums.RetentionDirection)oldModel.RetentionDirection,
                        Rate = oldModel.Rate,
                        RetentionToAccountId = oldModel.RetentionToAccountGuid,
                    };

                    newOrganization.ErpCOREDBContext.RetentionTypes.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {

                }
            });

            //newOrganization.ErpCOREDBContext.SaveChanges();
        }
        private void CopyRetentions_Groups()
        {
            
            var existModelIds = newOrganization.ErpCOREDBContext.RetentionGroups
            .Select(x => x.Id)
            .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.RetentionGroups
                .Where(i => !existModelIds.Contains(i.Id) )
                .ToList();

            oldModels.ForEach(oldModel =>
            {
                Console.WriteLine($"RetentionGroups:{oldModel.Id}");
                var exist = newOrganization.ErpCOREDBContext
                .RetentionGroups
                .FirstOrDefault(x => x.Id == oldModel.Id);

                if (exist == null)
                {
                    Console.WriteLine($"> Add");

                    exist = new ERPKeeperCore.Enterprise.Models.Financial.RetentionGroup()
                    {
                        Id = oldModel.Id,
                        AmountCommercial = oldModel.AmountCommercial,
                        Date = oldModel.TrnDate,
                        AmountRetention = oldModel.AmountRetention,
                        Count = oldModel.Count,
                        RetentionTypeId = oldModel.RetentionTypeId,
                    };

                    newOrganization.ErpCOREDBContext.RetentionGroups.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {

                }
            });

            newOrganization.ErpCOREDBContext.SaveChanges();
        }
    }
}
