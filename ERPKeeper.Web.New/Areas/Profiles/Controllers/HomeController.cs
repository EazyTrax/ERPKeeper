using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Profiles.Controllers
{

    public class HomeController : Base_ProfilesController
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Refresh()
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
