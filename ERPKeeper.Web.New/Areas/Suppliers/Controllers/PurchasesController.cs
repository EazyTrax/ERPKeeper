using Azure.Core;
using ERPKeeperCore.Web.Areas.Customers.Controllers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{
    public class PurchasesController : _Suppliers_Base_Controller
    {
        public IActionResult Index()
        {
            Organization.Purchases.CreateTransactions();
            Organization.Purchases.UpdateStatus();

            return View();
        }
        public IActionResult Post()
        {
            Organization.Purchases.Post_Ledgers();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Refresh()
        {
            Organization.Purchases.CreateTransactions();
            Organization.Purchases.UpdateStatus();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
