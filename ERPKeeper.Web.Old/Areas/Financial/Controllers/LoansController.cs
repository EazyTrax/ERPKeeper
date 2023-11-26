using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    public class LoansController : WebFrontEnd.Controllers._DefaultNodeController
    {


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
        public ActionResult Home() => View();
        public PartialViewResult PartialGridView() => PartialView("PartialGridView", Organization.Loans.GetAll());


        public ActionResult ReOrder()
        {
            Organization.Loans.ReOrder();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult PostLedger()
        {
            Organization.Loans.PostLedger();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

    }
}