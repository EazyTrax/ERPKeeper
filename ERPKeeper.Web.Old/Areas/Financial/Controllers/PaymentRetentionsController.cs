using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    [RouteArea("Financial")]
    [RoutePrefix("Payments/Retentions")]
    [Route("{action}")]
    public class PaymentRetentionsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();
        public PartialViewResult PartialGridView()
        {
            var payments = Organization.ErpNodeDBContext
                .GeneralPayments
                .Where(gp => gp.RetentionTypeGuid != null)
                .ToList();
            return PartialView("PartialGridView", payments);
        }

        public ActionResult ReOrder()
        {
            Organization.LiabilityPayments.ReOrder();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
      
    }
}