
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

namespace ERPKeeperCore.Enterprise.DAL
{
    public class AssetTypes : ERPNodeDalRepository
    {
        public AssetTypes(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<AssetType> GetAll()
        {
            return erpNodeDBContext.AssetTypes.ToList();
        }



        public AssetType? Find(Guid Id) => erpNodeDBContext.AssetTypes.Find(Id);


    }
}