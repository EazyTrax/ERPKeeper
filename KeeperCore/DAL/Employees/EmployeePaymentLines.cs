using KeeperCore.ERPNode.DAL.Transactions;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Employees;
using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.DAL.Employees
{
    public class EmployeePaymentLines : ERPNodeDalRepository
    {
        public EmployeePaymentLines(Organization organization) : base(organization)
        {

        }

        public Models.Employees.EmployeePaymentLine Find(Guid id) => erpNodeDBContext.EmployeePaymentLines.Find(id);
        public IQueryable<EmployeePaymentLine> Query() => erpNodeDBContext.EmployeePaymentLines;

        public void Remove(EmployeePaymentLine exEmployeePaymentItem)
        {
            erpNodeDBContext.EmployeePaymentLines.Remove(exEmployeePaymentItem);
            erpNodeDBContext.SaveChanges();
        }
    }
}