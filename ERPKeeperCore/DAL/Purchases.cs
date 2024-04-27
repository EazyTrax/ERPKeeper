
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Suppliers;
using ERPKeeperCore.Enterprise.Models.Suppliers.Enums;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class Purchases : ERPNodeDalRepository
    {
        public Purchases(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Purchase> GetAll()
        {
            return erpNodeDBContext.Purchases.ToList();
        }



        public Purchase? Find(Guid Id) => erpNodeDBContext.Purchases.Find(Id);


    }
}