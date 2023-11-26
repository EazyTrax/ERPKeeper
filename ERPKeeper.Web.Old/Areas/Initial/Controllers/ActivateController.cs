using System;
using System.Linq;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Setup.Controllers
{
    [Authorize]
    public class ActivateController : Controller
    {
        public ActionResult Home(string Profile)
        {
            return View();
        }
    }
}