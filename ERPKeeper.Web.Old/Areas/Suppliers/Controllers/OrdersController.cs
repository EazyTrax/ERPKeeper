

using ERPKeeper.Node.Models.Estimations.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;


namespace ERPKeeper.WebFrontEnd.Areas.Suppliers.Controllers
{
    public class OrdersController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => RedirectToAction("Ordered");
        public ActionResult Ordered() => View("Home", QuoteStatus.Ordered);
        public ActionResult Close() => View("Home", QuoteStatus.Close);
        public ActionResult Void() => View("Home", QuoteStatus.Void);
        public ActionResult Items() => View("Items");
        public PartialViewResult PartialGridView(QuoteStatus? id)
        {
            ViewBag.id = id;
            var estimates = Organization.PurchaseEstimates.ListByStatus(id);
            return PartialView("PartialGridView", estimates);
        }

        public ActionResult ReOrder()
        {
            Organization.PurchaseEstimates.ReOrder();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}