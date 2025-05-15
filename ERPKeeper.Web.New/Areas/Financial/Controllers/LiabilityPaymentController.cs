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
            var transcation = Organization.ErpCOREDBContext.LiabilityPayments.Find(TransactionId);

            if (transcation == null)
                return NotFound();


            return View(transcation);
        }
        public IActionResult Update(LiabilityPayment model)
        {
            var existModel = Organization.ErpCOREDBContext.LiabilityPayments.Find(TransactionId);

            if (!existModel.IsPosted)
            {
                existModel.Reference = model.Reference;
                existModel.Amount = model.Amount;
                existModel.Date = model.Date;
                Organization.SaveChanges();

                existModel.UpdateBalance();
                Organization.SaveChanges();
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult UnPost()
        {
            var model = Organization.ErpCOREDBContext.LiabilityPayments.Find(TransactionId);
            model.UnPostLedger();
            Organization.ErpCOREDBContext.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Post()
        {
            var model = Organization.ErpCOREDBContext.LiabilityPayments.Find(TransactionId);
            model.PostLedgers();
            Organization.ErpCOREDBContext.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Delete()
        {
            var model = Organization.ErpCOREDBContext.LiabilityPayments.Find(TransactionId);

            if (!model.IsPosted)
            {
                Organization.ErpCOREDBContext.LiabilityPayments.Remove(model);
                Organization.ErpCOREDBContext.SaveChanges();
                return Redirect($"/{CompanyId}/Financial/LiabilityPayments");
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Issue()
        {
            var model = Organization.ErpCOREDBContext.LiabilityPayments.Find(TransactionId);


            model.Status = Enterprise.Models.Financial.Enums.LiabilityPaymentStatus.Paid;
            Organization.ErpCOREDBContext.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
