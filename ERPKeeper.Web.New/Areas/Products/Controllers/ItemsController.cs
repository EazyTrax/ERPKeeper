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
            Organization.Items.RemoveGroupItems();

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
        public ActionResult UpdateAmount()
        {
            Organization.Items.Update_Sales_Amount();
            Organization.Items.Update_Purchases_Amount();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public ActionResult UpdateCustomerItems()
        {
            Organization.Items.UpdateCustomerItems();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public ActionResult UpdateSupplierItems()
        {
            Organization.Items.UpdateSupplierItems();
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
