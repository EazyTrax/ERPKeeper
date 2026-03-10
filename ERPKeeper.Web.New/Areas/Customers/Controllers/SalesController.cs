using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{

    public class SalesController : _CustomersArea_BaseController
    {
        public IActionResult Index(SaleStatus? status = null)
        {
            Organization.Sales.CreateTransactions();
            Organization.Sales.UpdateStatus();

            return View(status);
        }
        public IActionResult Post()
        {
            Organization.Sales.Post_Ledgers();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Refresh()
        {
            Organization.Sales.CreateTransactions();
            Organization.Sales.UpdateStatus();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
