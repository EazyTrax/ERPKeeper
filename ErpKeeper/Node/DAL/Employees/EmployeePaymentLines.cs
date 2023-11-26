using ERPKeeper.Node.DAL.Transactions;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Employees;
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.DAL.Employees
{
    public class EmployeePaymentLines : ERPNodeDalRepository
    {
        public EmployeePaymentLines(Organization organization) : base(organization)
        {

        }

        public Models.Employees.EmployeePaymentItem Find(Guid id) => erpNodeDBContext.EmployeePaymentLines.Find(id);
        public IQueryable<EmployeePaymentItem> Query() => erpNodeDBContext.EmployeePaymentLines;

        public void Remove(EmployeePaymentItem exEmployeePaymentItem)
        {
            erpNodeDBContext.EmployeePaymentLines.Remove(exEmployeePaymentItem);
            erpNodeDBContext.SaveChanges();
        }
    }
}