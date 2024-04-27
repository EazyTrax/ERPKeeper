
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
    public class EmployeePositions : ERPNodeDalRepository
    {
        public EmployeePositions(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<EmployeePosition> GetAll()
        {
            return erpNodeDBContext.EmployeePositions.ToList();
        }
        public int Count() => erpNodeDBContext.EmployeePositions.Count();


        public EmployeePosition? Find(Guid Id) => erpNodeDBContext.EmployeePositions.Find(Id);


    }
}