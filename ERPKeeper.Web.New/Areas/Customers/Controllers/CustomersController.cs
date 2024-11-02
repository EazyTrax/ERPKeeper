using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{

    public class CustomersController : _Customers_Base_Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Refresh()
        {
            Organization.Customers.UpdateSalesBalance();
            Organization.Customers.UpdateSalesAndSaleQuoteCount();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public ActionResult RemoveNoSale()
        {
            Organization.SaleQuotes.RemoveExpired(360);
            Organization.Customers.RemoveNoSaleOrQuote();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult SalesChart()
        {
            // Load and parse JSON data
            var jsonData = System.IO.File.ReadAllText("path_to_your_customers.json");
            var customers = JObject.Parse(jsonData)["data"]
                            .Select(c => new
                            {
                                Name = c["Profile"]["Name"].ToString(),
                                TotalSales = (decimal)c["TotalSales"]
                            })
                            .ToList();

            // Pass data to the view
            return View(customers);
        }

    }
}
