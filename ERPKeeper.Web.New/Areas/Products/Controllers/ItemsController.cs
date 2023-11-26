using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Products.Controllers
{

    public class ItemsController : Base_ProductsController
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult UpdateStock()
        {
            Organization.InventoryItemsDal.UpdateStockAmount();
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
