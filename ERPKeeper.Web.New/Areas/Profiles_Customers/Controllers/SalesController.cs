using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Profiles_Customers.Controllers
{

    public class SalesController : _Profiles_Customers_Base_Controller
    {
        public IActionResult Index()
        {
            EnterpriseRepo.Sales.CreateTransactions();
            return View();
        }
        public IActionResult Post()
        {
            EnterpriseRepo.Sales.PostToTransactions();
            return View();
        }
    }
}
