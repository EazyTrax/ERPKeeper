
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

namespace ERPKeeperCore.Enterprise.DAL
{
    public class EmployeePaymentTypes : ERPNodeDalRepository
    {
        public EmployeePaymentTypes(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<EmployeePaymentType> GetAll()
        {
            return erpNodeDBContext.EmployeePaymentTypes.ToList();
        }
        public int Count() => erpNodeDBContext.EmployeePaymentTypes.Count();


        public EmployeePaymentType? Find(Guid Id) => erpNodeDBContext.EmployeePaymentTypes.Find(Id);

        public void UpdateBalance()
        {
            foreach (var employeePaymentType in erpNodeDBContext.EmployeePaymentTypes.ToList())
            {
                employeePaymentType.UpdateBalance();
            }
            erpNodeDBContext.SaveChanges();

        }
    }
}