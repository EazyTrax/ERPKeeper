using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    public class LendsController : WebFrontEnd.Controllers._DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();
        public PartialViewResult PartialGridView() => PartialView("PartialGridView", Organization.Lends.GetAll());


        public ActionResult ReOrder()
        {
            Organization.Lends.ReOrder();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult PostLedger()
        {
            Organization.Lends.PostLedger();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult UnPost()
        {
            Organization.Lends.UnPost();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}