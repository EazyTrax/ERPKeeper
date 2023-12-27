
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
    public class EmployeePaymentTypes : ERPNodeDalRepository
    {
        public EmployeePaymentTypes(Organization organization) : base(organization)
        {

        }

        public List<EmployeePaymentType> GetAll() => erpNodeDBContext.EmployeePaymentTypes.ToList();


        public EmployeePaymentType Find(Guid id) => erpNodeDBContext.EmployeePaymentTypes.Find(id);

        public EmployeePaymentType CreateNew(EmployeePaymentType paymentType)
        {
            paymentType.Id = Guid.NewGuid();
            erpNodeDBContext.EmployeePaymentTypes.Add(paymentType);

            return paymentType;
        }

        public void ClearEmpty()
        {
            var emptyLines = erpNodeDBContext.EmployeePaymentLines.Where(l => l.EmployeePaymentId == null).ToList();
            erpNodeDBContext.EmployeePaymentLines.RemoveRange(emptyLines);



            erpNodeDBContext.SaveChanges();

        }
    }
}