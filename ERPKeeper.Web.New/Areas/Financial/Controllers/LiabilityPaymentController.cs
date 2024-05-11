using ERPKeeperCore.Enterprise.Models.Financial;
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

    [Route("/{CompanyId}/Financial/LiabilityPayments/{TransactionId:Guid}/{action=index}")]
    public class LiabilityPaymentController : Financial_BaseController
    {
        public Guid TransactionId => Guid.Parse(RouteData.Values["TransactionId"].ToString());

        public IActionResult Index()
        {
            var transcation = OrganizationCore.ErpCOREDBContext.LiabilityPayments.Find(TransactionId);
            return View(transcation);
        }
        public IActionResult Update(LiabilityPayment model)
        {
            var existModel = OrganizationCore.ErpCOREDBContext.LiabilityPayments.Find(TransactionId);

            if (!existModel.IsPosted)
            {
                existModel.Reference = model.Reference;
                existModel.Amount = model.Amount;
                existModel.Date = model.Date;
                OrganizationCore.SaveChanges();
            }


            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult UnPost()
        {
            var model = OrganizationCore.ErpCOREDBContext.LiabilityPayments.Find(TransactionId);
            model.IsPosted = false;
            model.Transaction.ClearLedger();
            OrganizationCore.ErpCOREDBContext.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
