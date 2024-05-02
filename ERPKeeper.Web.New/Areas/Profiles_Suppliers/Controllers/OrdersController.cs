using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Profiles_Suppliers.Controllers
{
    public class OrdersController : _Profiles_Suppliers_Base_Controller
    {
        public IActionResult Index()
        {
            return View();
        }     
    }
}
