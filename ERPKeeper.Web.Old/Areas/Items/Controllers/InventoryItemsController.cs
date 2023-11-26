
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Items.Controllers
{
    public class InventoryItemsController : WebFrontEnd.Controllers._DefaultNodeController
    {


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();

        public ActionResult UpdateAvgCost()
        {
            return RedirectToAction("Home");
        }


    }
}