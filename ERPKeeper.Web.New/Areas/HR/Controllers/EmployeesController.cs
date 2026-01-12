using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.HR.Controllers
{

    public class EmployeesController : _Employees_Base_Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Refresh()
        {
            var employees = Organization.Employees.GetAll();
            employees.ForEach(e =>
            {
                e.UpdateBalance();
            });
            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
