using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Suppliers.Controllers
{

    public class SuppliersController : Base_SuppliersController
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Refresh()
        {
            Organization.Purchases.UpdatePurchasingBalance();
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
