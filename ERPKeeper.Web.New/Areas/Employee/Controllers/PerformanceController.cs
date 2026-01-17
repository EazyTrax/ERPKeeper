using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

namespace ERPKeeperCore.Web.Areas.Employee.Controllers
{
    public class PerformanceController : _EmployeeBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
