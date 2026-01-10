using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Investors.Controllers
{
    [Route("/Investors/Investors/{InvestorUid:Guid}/{action=Index}")]
    public class InvestorController : _Investors_Base_Controller
    {

        public IActionResult Index(Guid InvestorUid)
        {
            var Investor = Organization.Investors.Find(InvestorUid);
            return View(Investor);
        }

        public IActionResult Payments(Guid InvestorUid)
        {
            var Investor = Organization.Investors.Find(InvestorUid);
            return View(Investor);
        }

    }
}
