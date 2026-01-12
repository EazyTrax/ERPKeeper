using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.HR_Employee.Controllers
{

    public class HomeController : _Employee_Base_Controller
    {

        public IActionResult Index(Guid EmployeeId)
        {

            return View();
        }

        public IActionResult Payments(Guid EmployeeId)
        {

            return View();
        }

    }
}
