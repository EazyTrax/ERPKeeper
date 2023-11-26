
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
    public class EmployeePaymentTypes : ERPNodeDalRepository
    {
        public EmployeePaymentTypes(Organization organization) : base(organization)
        {

        }

        public List<EmployeePaymentType> GetAll() => erpNodeDBContext.EmployeePaymentTypes.ToList();


        public EmployeePaymentType Find(Guid id) => erpNodeDBContext.EmployeePaymentTypes.Find(id);

        public EmployeePaymentType CreateNew(EmployeePaymentType paymentType)
        {
            paymentType.Uid = Guid.NewGuid();
            erpNodeDBContext.EmployeePaymentTypes.Add(paymentType);

            return paymentType;
        }
        public EmployeePaymentType CreateNew()
        {
            EmployeePaymentType paymentType = new EmployeePaymentType()
            {
                Name = "New Payment Type",
                Uid = Guid.NewGuid()
            };
            erpNodeDBContext.EmployeePaymentTypes.Add(paymentType);
            erpNodeDBContext.SaveChanges();

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