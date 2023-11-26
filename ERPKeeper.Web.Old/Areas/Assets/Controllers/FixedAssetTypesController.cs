using System;
using System.Linq;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Assets.Controllers
{
    public class FixedAssetTypesController : WebFrontEnd.Controllers._DefaultNodeController
    {

        public ActionResult Home()
        {
            return View();
        }

        [ValidateInput(false)]
        public PartialViewResult PartialGridView(string Type = "Active")
        {
            return PartialView("PartialGridView", Organization.FixedAssetTypes.All());
        }
    }
}