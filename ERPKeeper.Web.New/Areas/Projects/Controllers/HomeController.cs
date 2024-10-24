using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Projects.Controllers
{
    [Area("Projects")]
    public class HomeController : DefaultController
    {
        public IActionResult Index()
        {
            return View();
        }
      

    }
}
