using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Accounting.Controllers
{

    public class HomeController : AccountingBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Post()
        {
            Organization.LedgersDal.PostLedgers();
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
