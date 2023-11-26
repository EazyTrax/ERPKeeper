using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    [RouteArea("Financial")]
    [RoutePrefix("RetentionTypes")]
    [Route("{action}/{id?}")]
    public class RetentionTypesController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home() => View();
        public PartialViewResult PartialGridView() => PartialView(Organization.RetentionTypes.GetAll());
    }

}