using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

using Microsoft.AspNetCore.Http;


namespace ERPKeeperCore.Web.Areas.Employee.Controllers
{
    public class HomeController : _EmployeeBaseController
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}