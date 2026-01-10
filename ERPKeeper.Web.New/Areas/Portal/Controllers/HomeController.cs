using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("/{area}/{controller=Home}/{action=Index}/{id?}")]
    public class HomeController : _BaseController
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
