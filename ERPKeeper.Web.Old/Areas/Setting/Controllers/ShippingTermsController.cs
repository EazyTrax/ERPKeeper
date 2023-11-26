using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Setting.Controllers
{
    public class ShippingTermsController : WebFrontEnd.Controllers._DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();

        public PartialViewResult PartialGridView() => PartialView(Organization.ShippingTerms.ListAll());

    }
}