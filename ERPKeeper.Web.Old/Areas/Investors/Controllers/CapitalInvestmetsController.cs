using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Investors.Controllers
{
    public class CapitalActivitiesController : WebFrontEnd.Controllers._DefaultNodeController
    {
        public ERPKeeper.Node.DAL.Investors.CapitalActivities capitalInvestments;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();

        public PartialViewResult PartialGridView() => PartialView(Organization.CapitalActivities.GetAll());

        public ActionResult ReOrder()
        {
            capitalInvestments.ReOrder();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult PostLedger()
        {
            capitalInvestments.PostLedger();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult UnPost()
        {
            capitalInvestments.UnPost();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

    }
}