using ERPKeeper.WebFrontEnd.Authorize;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.DashBoard.Controllers
{
    [ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel.Viewer)]

    public class CurrentUserController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public PartialViewResult _ViewPeriodTrackBar()
        {
            return PartialView();
        }





    }
}