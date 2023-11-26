using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeper.Web.New.Controllers;

namespace ERPKeeper.Web.New.Areas.Employees.Controllers
{
    [Route("/{CompanyId}/{area}/Employees/{EmployeeUid:Guid}/{action=Index}")]
    public class EmployeeController : Base_EmployeesController
    {

        public IActionResult Index(Guid EmployeeUid)
        {
            var Employee = Organization.Employees.Find(EmployeeUid);
            return View(Employee);
        }

        public IActionResult Payments(Guid EmployeeUid)
        {
            var Employee = Organization.Employees.Find(EmployeeUid);
            return View(Employee);
        }

    }
}
