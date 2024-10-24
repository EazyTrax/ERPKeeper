using ERPKeeperCore.Web.Areas.Financial.Controllers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Financial.Controllers
{
    public class LiabilitiesController : Financial_BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UpdateCurrentBalance()
        {
            Organization.ChartOfAccount.Refresh_CurrentBalance();
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
