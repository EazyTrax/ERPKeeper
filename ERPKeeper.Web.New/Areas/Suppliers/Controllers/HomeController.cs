using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{

    public class HomeController : _Suppliers_Base_Controller
    {
        public IActionResult Index()
        {
            return Redirect($"/{CompanyId}/Suppliers/Suppliers");
        }

        public ActionResult Refresh()
        {
    
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
