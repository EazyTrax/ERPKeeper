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

    [Route("/{CompanyId}/{area}/FundTransfers/{TransactionId:Guid}/{action=index}")]
    public class FundTransferController : Financial_BaseController
    {
        public Guid TransactionId => Guid.Parse(RouteData.Values["TransactionId"].ToString());

        public IActionResult Index()
        {
            var transcation = EnterpriseRepo.FundTransfers.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Items()
        {
            var transcation = EnterpriseRepo.FundTransfers.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Payments()
        {
            var transcation = EnterpriseRepo.FundTransfers.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Shipments()
        {
            var transcation = EnterpriseRepo.FundTransfers.Find(TransactionId);
            return View(transcation);
        }



        public IActionResult Documents()
        {
            var transcation = EnterpriseRepo.FundTransfers.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Export()
        {
            var transcation = EnterpriseRepo.FundTransfers.Find(TransactionId);
            return View(transcation);
        }
    }
}
