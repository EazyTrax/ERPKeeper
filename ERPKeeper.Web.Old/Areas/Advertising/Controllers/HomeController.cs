using System;
using System.Linq;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Advertising.Controllers
{
    [AllowAnonymous]
    public class HomeController : WebFrontEnd.Controllers._DefaultNodeController
    {
        // GET: Advertising/Default
        public ActionResult Home()
        {
            return View();
        }

        public PartialViewResult _SideAdvertise()
        {
            return PartialView();
        }

    }
}