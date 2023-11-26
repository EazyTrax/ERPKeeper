using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Customers.Controllers
{

    public class HomeController : Base_CustomersController
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Refresh()
        {
            Organization.Customers.UpdateSalesBalance();
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
