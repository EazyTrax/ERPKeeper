using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Routing;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{
    [Route("/{CompanyId}/Customers/Customers/{customerId:Guid}/{action=Index}")]
    public class CustomerController : _Customers_Base_Controller
    {
        Guid customerId => Guid.Parse(HttpContext.GetRouteData().Values["customerId"].ToString());

        public IActionResult Index()
        {
            var customer = Organization.Customers.Find(customerId);
            return View(customer);
        }

        public IActionResult Sales()
        {
            var customer = Organization.Customers.Find(customerId);
            return View(customer);
        }

        public IActionResult SaleQuotes()
        {
            var customer = Organization.Customers.Find(customerId);
            return View(customer);
        }
        public IActionResult ReceivePayments()
        {
            var customer = Organization.Customers.Find(customerId);
            return View(customer);
        }
        public IActionResult Items()
        {
            var customer = Organization.Customers.Find(customerId);
            return View(customer);
        }

        public IActionResult UpdateItems(Guid customerId)
        {
            // Retrieve Customer once
            var customer = Organization.Customers.Find(customerId);
            if (customer == null)
                return NotFound();

            // Fetch CustomerItems in bulk to avoid querying in each loop
            var customerItemsDict = Organization.ErpCOREDBContext.CustomerItems
                .Where(ct => ct.CustomerId == customerId)
                .ToDictionary(ct => ct.ItemId, ct => ct);

            // Calculate and update AmountSale from SaleItems
            var saleItems = Organization.ErpCOREDBContext.SaleItems
                .Where(si => si.Sale.CustomerId == customerId)
                .GroupBy(si => si.ItemId)
                .Select(si => new { ItemId = si.Key, Amount = si.Sum(x => x.Quantity) })
                .ToList();

            foreach (var item in saleItems)
            {
                if (!customerItemsDict.TryGetValue(item.ItemId, out var customerItem))
                {
                    // Create and add new CustomerItem if it doesn't exist
                    customerItem = new Enterprise.Models.Customers.CustomerItem(item.ItemId, customerId);
                    customer.CustomerItems.Add(customerItem);
                    customerItemsDict[item.ItemId] = customerItem;
                }
                customerItem.AmountSale = item.Amount;
            }

            // Calculate and update AmountSaleQuote from SaleQuoteItems
            var quoteItems = Organization.ErpCOREDBContext.SaleQuoteItems
                .Where(si => si.Quote.CustomerId == customerId)
                .GroupBy(si => si.ItemId)
                .Select(si => new { ItemId = si.Key, Amount = si.Sum(x => x.Quantity) })
                .ToList();

            foreach (var item in quoteItems)
            {
                if (!customerItemsDict.TryGetValue(item.ItemId, out var customerItem))
                {
                    // Create and add new CustomerItem if it doesn't exist
                    customerItem = new Enterprise.Models.Customers.CustomerItem(item.ItemId, customerId);
                    customer.CustomerItems.Add(customerItem);
                    customerItemsDict[item.ItemId] = customerItem;
                }
                customerItem.AmountSaleQuote = item.Amount;
            }

            // Save all changes in one go
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult EstimateItems()
        {
            var customer = Organization.Customers.Find(customerId);
            return View(customer);
        }

        public IActionResult Delete()
        {
            var customer = Organization.Customers.Find(customerId);

            var result = Organization.Customers.Remove(customer);

            if(result)
                return Redirect($"/{CompanyId}/Customers/Customers");
            else
                return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
