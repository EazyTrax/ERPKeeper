using ERPKeeper.Web.New.Areas.Assets.Controllers;
using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Taxes.Controllers
{
    
    public class HomeController : Base_TaxesController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
