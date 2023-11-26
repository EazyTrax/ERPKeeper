using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Employees.Controllers
{
    public class PaymentPeriodsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();
        public PartialViewResult PartialGridView() => PartialView(Organization.EmployeePaymentPeriods.GetAll());


        public ActionResult Refresh()
        {
            Organization.EmployeePaymentPeriods.Refresh();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult New()
        {
            var paymentPeriod = Organization.EmployeePaymentPeriods.CreateNew(DateTime.Today);
            return Redirect(Url.Action("Home", "PaymentPeriod", new { id = paymentPeriod.Uid }));
        }

    }
}