using ERPKeeperCore.Enterprise.DBContext;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{

    public class SuppliersController : _Suppliers_Base_Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Refresh()
        {

            Organization.Suppliers.UpdatePurchasesBalance();
            Organization.Suppliers.UpdatePurchasesAndPurchaseQuoteCount();


            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
