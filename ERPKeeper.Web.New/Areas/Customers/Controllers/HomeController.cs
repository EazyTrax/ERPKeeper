using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{

    public class HomeController : _Customers_Base_Controller
    {
        public IActionResult Index()
        {
            return Redirect("/tec/Customers/Customers");
        }

        public ActionResult Refresh()
        {
           
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
