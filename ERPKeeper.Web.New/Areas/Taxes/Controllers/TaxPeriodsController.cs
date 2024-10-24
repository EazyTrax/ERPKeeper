using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Taxes.Controllers
{

    public class TaxPeriodsController : Base_TaxesController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Refresh()
        {
            Organization.TaxPeriods.Refresh();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
