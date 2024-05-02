using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Profiles_Customers.Controllers
{
    public class QuotesController : _Profiles_Customers_Base_Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
