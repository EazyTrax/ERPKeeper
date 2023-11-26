using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Items.Controllers
{
    public class ParameterTypesController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();
        public PartialViewResult PartialGridView() => PartialView(Organization.ItemParameterTypes.ListAll());

        public ActionResult New(string name)
        {
            var newParameter = Organization.ItemParameterTypes.CreateNew(name);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}