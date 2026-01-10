using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Employees.Controllers
{
    [Route("/Employees/Employees/{EmployeeId:Guid}/{action=Index}")]
    public class EmployeeController : _Employees_Base_Controller
    {

        public IActionResult Index(Guid EmployeeId)
        {
            var Employee = Organization.Employees.Find(EmployeeId);
            return View(Employee);
        }

        public IActionResult Payments(Guid EmployeeId)
        {
            var Employee = Organization.Employees.Find(EmployeeId);
            return View(Employee);
        }

    }
}
