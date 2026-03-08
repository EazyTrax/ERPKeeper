using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Accounting.Controllers
{
    public class FiscalYearsController : AccountingArea_BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Refresh()
        {

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Prepares()
        {

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
