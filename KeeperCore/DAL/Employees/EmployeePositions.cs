
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Financial;
using KeeperCore.ERPNode.Models.Financial.Transfer;
using KeeperCore.ERPNode.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.Employees;

namespace KeeperCore.ERPNode.DAL.Employees
{
    public class EmployeePositions : ERPNodeDalRepository
    {
        public EmployeePositions(Organization organization) : base(organization)
        {

        }

        public List<EmployeePosition> GetAll() => erpNodeDBContext.EmployeePositions.ToList();

        public EmployeePosition Find(Guid id) => erpNodeDBContext.EmployeePositions.Find(id);

        public EmployeePosition CreateNew(EmployeePosition position)
        {
            position.Id = Guid.NewGuid();
            erpNodeDBContext.EmployeePositions.Add(position);
            return position;
        }
    }
}