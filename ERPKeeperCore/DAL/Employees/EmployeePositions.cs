
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Employees;
using ERPKeeperCore.Enterprise.Models.Employees.Enums;

namespace ERPKeeperCore.Enterprise.DAL.Employees
{
    public class EmployeePositions : ERPNodeDalRepository
    {
        public EmployeePositions(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<JobPosition> GetAll()
        {
            return erpNodeDBContext.EmployeePositions.ToList();
        }
        public int Count() => erpNodeDBContext.EmployeePositions.Count();


        public JobPosition? Find(Guid Id) => erpNodeDBContext.EmployeePositions.Find(Id);


    }
}