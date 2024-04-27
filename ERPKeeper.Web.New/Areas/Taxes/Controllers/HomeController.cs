using ERPKeeperCore.Web.Areas.Assets.Controllers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Taxes.Controllers
{
    
    public class HomeController : Base_TaxesController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
