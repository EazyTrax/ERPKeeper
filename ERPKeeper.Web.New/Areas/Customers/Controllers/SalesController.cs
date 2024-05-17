using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{

    public class SalesController : _Customers_Base_Controller
    {
        public IActionResult Index()
        {
            OrganizationCore.Sales.CreateTransactions();
            return View();
        }
        public IActionResult Post()
        {
            OrganizationCore.Sales.PostToTransactions();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
