
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Financial;
using ERPKeeper.Node.Models.Financial.Transfer;
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.Models.Employees;

namespace ERPKeeper.Node.DAL.Employees
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
            position.Uid = Guid.NewGuid();
            erpNodeDBContext.EmployeePositions.Add(position);
            return position;
        }
    }
}