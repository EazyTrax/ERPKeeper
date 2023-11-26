using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Commercials.Controllers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeRush", "Can implement base type constructors")]
    public class CommercialsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home()
        {
            return View();
        }

        [ValidateInput(false)]
        public PartialViewResult PartialGridView()
        {
            var model = Organization.Commercials.Query();
            return PartialView("PartialGridView", model.ToList());
        }






    }
}