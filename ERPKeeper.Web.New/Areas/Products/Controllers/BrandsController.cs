using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Products.Controllers
{
    public class BrandsController : Base_ProductsController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpdateAmount()
        {
            EnterpriseRepo.ItemBrands.UpdateAmount();
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
