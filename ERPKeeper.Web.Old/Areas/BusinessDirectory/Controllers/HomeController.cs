using System;
using System.Linq;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.BusinessDirectory.Controllers
{
    public class HomeController : WebFrontEnd.Controllers._DefaultNodeController
    {
        public ActionResult Home()
        {
            return View();
        }
    }
}