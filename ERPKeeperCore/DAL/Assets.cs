
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
    public class Assets : ERPNodeDalRepository
    {
        public Assets(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Asset> GetAll()
        {
            return erpNodeDBContext.Assets.ToList();
        }



        public Asset? Find(Guid Id) => erpNodeDBContext.Assets.Find(Id);


    }
}