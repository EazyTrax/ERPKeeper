using System;
using System.Linq;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Documents.Controllers
{
    public class HomeController : WebFrontEnd.Controllers._DefaultNodeController
    {
        // GET: Documents/Home
        public ActionResult Home()
        {
            return RedirectToAction(string.Empty, "Documents", null);
        }
    }
}