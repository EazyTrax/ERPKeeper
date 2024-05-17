
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

namespace ERPKeeperCore.Enterprise.DAL.Suppliers
{
    public class Suppliers : ERPNodeDalRepository
    {
        public Suppliers(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Supplier> GetAll()
        {
            return erpNodeDBContext.Suppliers.ToList();
        }


        public int Count() => erpNodeDBContext.Suppliers.Count();
        public Supplier? Find(Guid Id) => erpNodeDBContext.Suppliers.Find(Id);

        public void UpdateSuppliersPurchasesBalance()
        {

            erpNodeDBContext.Suppliers.ToList()
                    .ForEach(x =>
                    {
                        x.UpdateBalance();
                    });
            erpNodeDBContext.SaveChanges();
        }
    }
}