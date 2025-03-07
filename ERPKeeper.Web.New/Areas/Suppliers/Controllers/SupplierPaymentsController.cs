using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{

    public class SupplierPayments : _Suppliers_Base_Controller
    {
        public IActionResult Index()
        {
            Organization.SupplierPayments.CreateTransactions();
            return View();
        }
        public IActionResult Refresh()
        {
            Organization.SupplierPayments.UpdateRetentionGroups();
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
