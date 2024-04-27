using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{

    public class SuppliersController : Base_SuppliersController
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Refresh()
        {
            EnterpriseRepo.Purchases.UpdatePurchasingBalance();
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
