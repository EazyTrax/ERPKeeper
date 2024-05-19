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
        public IActionResult Accounts()
        {
            return View();
        }
        public ActionResult UpdateStock()
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public ActionResult UpdateCustomerItems()
        {
            OrganizationCore.Items.UpdateCustomerItems();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public ActionResult UpdateSupplierItems()
        {
            OrganizationCore.Items.UpdateSupplierItems();
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
