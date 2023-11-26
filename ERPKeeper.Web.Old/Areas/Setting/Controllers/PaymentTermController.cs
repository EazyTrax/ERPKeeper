using ERPKeeper.Node.Models.Transactions.Commercials;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Setting.Controllers
{
    public class PaymentTermController : WebFrontEnd.Controllers._DefaultNodeController
    {


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);


        }

        public ActionResult Home(Guid id) => View(Organization.PaymentTerms.Find(id));

        public ActionResult New()
        {
            var PaymentTerm = new PaymentTerm()
            {
                Uid = Guid.NewGuid(),
                DueDayCount = 0,
                Name = string.Empty
            };
            return View("Home", PaymentTerm);
        }
        public ActionResult Save(PaymentTerm paymentTerm)
        {
            var existPaymentTerm = Organization.PaymentTerms.Find(paymentTerm.Uid);

            if (existPaymentTerm == null)
                existPaymentTerm = Organization.PaymentTerms.CreateNew(paymentTerm);
            else
                existPaymentTerm.Update(paymentTerm);
            Organization.SaveChanges();


            return RedirectToAction("Home", new { id = paymentTerm.Uid });
        }
    }
}