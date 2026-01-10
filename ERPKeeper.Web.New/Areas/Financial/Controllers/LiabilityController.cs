using ERPKeeperCore.Web.Areas.Financial.Controllers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Financials.Controllers
{

    [Route("/Financial/Liabilities/{AccountId:Guid}/{action=index}")]
    public class LiabilityController : Financial_BaseController
    {
        public Guid AccountId => Guid.Parse(RouteData.Values["AccountId"].ToString());

        public IActionResult Index()
        {
            var transcation = Organization.ChartOfAccount.Find(AccountId);

            if (transcation == null)
                return NotFound();


            return View(transcation);
        }

        public IActionResult Balances()
        {
            var transcation = Organization.ChartOfAccount.Find(AccountId);
            return View(transcation);
        }

        public IActionResult Payments()
        {
            var transcation = Organization.ChartOfAccount.Find(AccountId);
            return View(transcation);
        }


    }
}
