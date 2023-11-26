
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
    public class EmployeePaymentTemplates : ERPNodeDalRepository
    {
        public EmployeePaymentTemplates(Organization organization) : base(organization)
        {

        }

        public List<EmployeePaymentTemplate> GetAll() => erpNodeDBContext.EmployeePaymentTemplates.ToList();
        public EmployeePaymentType Find(Guid id) => erpNodeDBContext.EmployeePaymentTypes.Find(id);

        public void CreateNew(EmployeePaymentTemplate template)
        {
            template.Uid = Guid.NewGuid();
            erpNodeDBContext.EmployeePaymentTemplates.Add(template);
        }
    }
}