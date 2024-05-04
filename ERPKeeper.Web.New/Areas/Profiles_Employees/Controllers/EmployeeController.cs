using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Profiles_Employees.Controllers
{
    [Route("/{CompanyId}/Profiles/Employees/Employees/{EmployeeUid:Guid}/{action=Index}")]
    public class EmployeeController : _Profiles_Employees_Base_Controller
    {

        public IActionResult Index(Guid EmployeeUid)
        {
            var Employee = OrganizationCore.Employees.Find(EmployeeUid);
            return View(Employee);
        }

        public IActionResult Payments(Guid EmployeeUid)
        {
            var Employee = OrganizationCore.Employees.Find(EmployeeUid);
            return View(Employee);
        }

    }
}
