using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    [RouteArea("Financial")]
    [RoutePrefix("Payments/RetentionGroups")]
    [Route("{action}")]
    public class RetentionGroupsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();
        public PartialViewResult PartialGridView()
        {
            var payments = Organization.ErpNodeDBContext
                .RetentionGroups
                .ToList();
            return PartialView("PartialGridView", payments);
        }

        public ActionResult ReOrder()
        {
            Organization.RetentionGroups.Reorder();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


    }
}