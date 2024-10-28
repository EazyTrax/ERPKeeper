using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Investors.Controllers
{
    public class HomeController : _Investors_Base_Controller
    {
        public IActionResult Index()
        {
            return Redirect($"/{CompanyId}/Investors/Investors");
        }

        public ActionResult Refresh()
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}