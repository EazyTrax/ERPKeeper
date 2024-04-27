
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Employment;
using ERPKeeperCore.Enterprise.Models.Employment.Enums;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class Employees : ERPNodeDalRepository
    {
        public Employees(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Employee> GetAll()
        {
            return erpNodeDBContext.Employees.ToList();
        }
        public int Count() => erpNodeDBContext.Employees.Count();


        public Employee? Find(Guid Id) => erpNodeDBContext.Employees.Find(Id);


    }
}