using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    [RouteArea("Financial")]
    [RoutePrefix("LiabilityPayments")]
    [Route("{action}/{id?}")]
    public class LiabilityPaymentsController : WebFrontEnd.Controllers._DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();
        public PartialViewResult PartialGridView() =>
            PartialView("PartialGridView", Organization.LiabilityPayments.GetAll());


        public ActionResult ReOrder()
        {
            Organization.LiabilityPayments.ReOrder();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult UnPost()
        {
            Organization.LiabilityPayments.UnPost();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult New(Guid id)
        {
            var liabilityPayment = Organization.LiabilityPayments.CreateNew(id, 0);
            return RedirectToAction("Home", "LiabilityPayment", new { Area = "Financial", Id = liabilityPayment.Uid });
        }

    }
}