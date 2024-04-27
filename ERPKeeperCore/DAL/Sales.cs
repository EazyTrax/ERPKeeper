
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class Sales : ERPNodeDalRepository
    {
        public Sales(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Sale> GetAll()
        {
            return erpNodeDBContext.Sales.ToList();
        }



        public Sale? Find(Guid Id) => erpNodeDBContext.Sales.Find(Id);


    }
}