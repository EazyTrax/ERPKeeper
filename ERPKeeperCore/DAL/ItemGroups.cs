
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Items;
using ERPKeeperCore.Enterprise.Models.Items.Enums;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class ItemGroups : ERPNodeDalRepository
    {
        public ItemGroups(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<ItemGroup> GetAll()
        {
            return erpNodeDBContext.ItemGroups.ToList();
        }



        public ItemGroup? Find(Guid Id) => erpNodeDBContext.ItemGroups.Find(Id);


    }
}