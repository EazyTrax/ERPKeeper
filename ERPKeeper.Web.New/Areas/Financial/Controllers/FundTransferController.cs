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

    [Route("/{CompanyId}/Financial/FundTransfers/{TransactionId:Guid}/{action=index}")]
    public class FundTransferController : Financial_BaseController
    {
        public Guid TransactionId => Guid.Parse(RouteData.Values["TransactionId"].ToString());

        public IActionResult Index()
        {
            var transcation = Organization.FundTransfers.Find(TransactionId);

            if (transcation == null)
                return NotFound();


            return View(transcation);
        }

        public IActionResult UnPost()
        {
            var model = Organization.FundTransfers.Find(TransactionId);
            Organization.FundTransfers.UnPost(model);
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Items()
        {
            var transcation = Organization.FundTransfers.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Payments()
        {
            var transcation = Organization.FundTransfers.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Shipments()
        {
            var transcation = Organization.FundTransfers.Find(TransactionId);
            return View(transcation);
        }



        public IActionResult Documents()
        {
            var transcation = Organization.FundTransfers.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Export()
        {
            var transcation = Organization.FundTransfers.Find(TransactionId);
            return View(transcation);
        }
    }
}
