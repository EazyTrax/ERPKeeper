
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
    public class Brands : ERPNodeDalRepository
    {
        public Brands(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Brand> GetAll()
        {
            return erpNodeDBContext.Brands.ToList();
        }



        public Brand? Find(Guid Id) => erpNodeDBContext.Brands.Find(Id);

        public void UpdateAmount()
        {
            erpNodeDBContext
                .Brands
                .ToList()
                .ForEach(b => b.Refresh());

            erpNodeDBContext.SaveChanges();
        }
    }
}