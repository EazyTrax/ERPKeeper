using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Items.Controllers
{

    public class BrandsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();
        public PartialViewResult PartialGridView() =>
            PartialView("PartialGridView", Organization.ItemBrands.ListAll());

        public ActionResult New(string Name)
        {
            var newBrand = Organization.ItemBrands.CreateNew(Name);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}