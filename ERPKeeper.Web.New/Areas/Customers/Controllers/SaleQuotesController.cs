using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{
    public class SaleQuotesController : _CustomersArea_BaseController
    {
        public IActionResult Index(SaleQuoteStatus? status = null)
        {
            return View(status);
        }

        public IActionResult Orders()
        {
            return View();
        }
    }
}
