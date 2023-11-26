using ERPKeeper.Node.DAL;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Profiles.Controllers
{
    public class PermissionsController : WebFrontEnd.Controllers._DefaultNodeController
    {



        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }



        public ActionResult Home() => View();

        [ValidateInput(false)]
        public PartialViewResult PartialGridView()
        {
            var profiles = Organization.Profiles.GetPermissionProfileList;
            return PartialView("PartialGridView", profiles);
        }
    }
}