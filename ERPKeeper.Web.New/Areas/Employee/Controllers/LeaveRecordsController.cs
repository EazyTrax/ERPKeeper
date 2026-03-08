using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;
using ERPKeeperCore.Enterprise.Models.Employees;
using ERPKeeperCore.Enterprise.Models.Employees.Enums;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Route("/Employee/{controller=Home}/{action=Index}/{id?}")]
    public class LeaveRecordsController : _BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

     
    }
}
