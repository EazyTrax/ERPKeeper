using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Products.Controllers
{

    public class ItemsController : Base_ProductsController
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult UpdateStock()
        {
            EnterpriseRepo.InventoryItemsDal.UpdateStockAmount();
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
