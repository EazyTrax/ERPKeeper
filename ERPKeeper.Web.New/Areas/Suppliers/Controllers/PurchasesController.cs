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
            OrganizationCore.Purchases.CreateTransactions();
            return View();
        }

    }
}
