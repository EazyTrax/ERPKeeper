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
        private void CopyBrands()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.Brands.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Uid}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.Brands.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Items.Brand()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Description = a.Description,
                        WebSite = a.WebSite,
                        ShotName = a.ShotName,
                    };
                    newOrganization.ErpCOREDBContext.Brands.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void CopyItemGroups()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.ItemGroups.ToList();

            oldModels.ForEach(a =>
            {

                var exist = newOrganization.ErpCOREDBContext.ItemGroups.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"ITM:{a.Uid}-{a.PartNumber}");
                    exist = new ERPKeeperCore.Enterprise.Models.Items.ItemGroup()
                    {
                        Id = a.Uid,
                        Name = a.PartNumber,
                    };
                    newOrganization.ErpCOREDBContext.ItemGroups.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {

                }


            });
        }
        private void Copy_Items_Items()
        {
            var existModelIds = newOrganization.ErpCOREDBContext.Items
                .Select(x => x.Id)
                .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.Items
                .Where(i => i.ItemType != ERPKeeper.Node.Models.Items.Enums.ItemTypes.Group)
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();



            oldModels.ForEach(a =>
            {
                var exist = newOrganization.ErpCOREDBContext.Items.FirstOrDefault(x => x.Id == a.Uid);
                var brand = newOrganization.ErpCOREDBContext.Brands.FirstOrDefault(x => x.Id == a.BrandGuid);

                if (exist == null)
                {
                    Console.WriteLine($"ITM:{a.Uid}-{a.PartNumber}");
                    exist = new ERPKeeperCore.Enterprise.Models.Items.Item()
                    {
                        Id = a.Uid,
                        PartNumber = a.PartNumber,
                        BrandId = brand?.Id,
                        Description = a.Description,
                        PurchasePrice = a.PurchasePrice,
                        SalePrice = a.UnitPrice,
                        UPC = a.UPC,
                        Status = (ItemStatus)a.Status,
                        ItemType = (ItemTypes)a.ItemType,
                        OnlineSale = a.OnlineSale,
                        Specification = a.Specification,
                        IncomeAccountId = a.IncomeAccountUid,
                        PurchaseAccountId = a.PurchaseAccountUid,
                        CreatedDate = (DateTime)(a.CreatedDate ?? DateTime.Today),
                    };
                    newOrganization.ErpCOREDBContext.Items.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {
                    try
                    {
                        exist.ItemGroupId = a.ParentUid;
                        newOrganization.ErpCOREDBContext.SaveChanges();
                    }
                    catch (Exception) { }
                }


            });
        }

    }
}
