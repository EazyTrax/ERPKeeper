
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
    public class EmployeePaymentTemplates : ERPNodeDalRepository
    {
        public EmployeePaymentTemplates(Organization organization) : base(organization)
        {

        }

        public List<EmployeePaymentTemplate> GetAll() => erpNodeDBContext.EmployeePaymentTemplates.ToList();
        public EmployeePaymentType Find(Guid id) => erpNodeDBContext.EmployeePaymentTypes.Find(id);

        public void CreateNew(EmployeePaymentTemplate template)
        {
            template.Id = Guid.NewGuid();
            erpNodeDBContext.EmployeePaymentTemplates.Add(template);
        }
    }
}