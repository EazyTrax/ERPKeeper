using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Employees.Controllers
{
    [Route("/{CompanyId}/{area}/Employees/{EmployeeUid:Guid}/{action=Index}")]
    public class EmployeeController : Base_EmployeesController
    {

        public IActionResult Index(Guid EmployeeUid)
        {
            var Employee = EnterpriseRepo.Employees.Find(EmployeeUid);
            return View(Employee);
        }

        public IActionResult Payments(Guid EmployeeUid)
        {
            var Employee = EnterpriseRepo.Employees.Find(EmployeeUid);
            return View(Employee);
        }

    }
}
