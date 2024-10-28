using ERPKeeperCore.Web.Areas.Investors.Controllers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Projects.Controllers
{
    public class HomeController : _Projects_Base_Controller
    {
        public IActionResult Index()
        {
            return View();
        }
      

    }
}
