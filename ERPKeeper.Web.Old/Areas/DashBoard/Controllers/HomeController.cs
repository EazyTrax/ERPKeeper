using ERPKeeper.WebFrontEnd.Authorize;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.DashBoard.Controllers
{

    [ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel.Viewer)]

    public class HomeController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult RedirectToDefaultProfile() => RedirectToAction("Home", "Home", new
        {
            Profile = "evil",
            Area = "DashBoard"
        });

        public ActionResult Home() => View();
        public PartialViewResult _Tile() => PartialView();
        public ActionResult Date(DateTime id) => Content(id.ToLongDateString());

    }
}