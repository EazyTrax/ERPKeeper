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
            var customer = OrganizationCore.Customers.Find(customerId);
            return View(customer);
        }

        public IActionResult Sales()
        {
            var customer = OrganizationCore.Customers.Find(customerId);
            return View(customer);
        }

        public IActionResult SaleQuotes()
        {
            var customer = OrganizationCore.Customers.Find(customerId);
            return View(customer);
        }
        public IActionResult ReceivePayments()
        {
            var customer = OrganizationCore.Customers.Find(customerId);
            return View(customer);
        }
        public IActionResult Items()
        {
            var customer = OrganizationCore.Customers.Find(customerId);
            return View(customer);
        }

        public IActionResult UpdateItems()
        {
            var customer = OrganizationCore.Customers.Find(customerId);

            var saleItems = OrganizationCore.ErpCOREDBContext.SaleItems
                .Where(si => si.Sale.CustomerId == customerId)
                .GroupBy(si => si.ItemId)
                .Select(si => new
                {
                    si.Key,
                    Amount = si.Sum(x => x.Quantity),
                }).ToList();


            saleItems.ForEach(si =>
            {
                var customerItem = OrganizationCore.ErpCOREDBContext.CustomerItems
                .Where(ct => ct.CustomerId == customerId && ct.ItemId == si.Key)
                .FirstOrDefault();

                if (customerItem == null)
                    customerItem = new Enterprise.Models.Customers.CustomerItem(si.Key);

                customerItem.AmountSale = si.Amount;

                customer.Items.Add(customerItem);
            });

            OrganizationCore.SaveChanges();


            var quoteItems = OrganizationCore.ErpCOREDBContext.SaleQuoteItems
               .Where(si => si.Quote.CustomerId == customerId)
               .GroupBy(si => si.ItemId)
               .Select(si => new
               {
                   si.Key,
                   Amount = si.Sum(x => x.Quantity),
               }).ToList();

            quoteItems.ForEach(si =>
            {
                var customerItem = OrganizationCore.ErpCOREDBContext.CustomerItems
                .Where(ct => ct.CustomerId == customerId && ct.ItemId == si.Key)
                .FirstOrDefault();

                if (customerItem == null)
                    customerItem = new Enterprise.Models.Customers.CustomerItem(si.Key);

                customerItem.AmountQuote = si.Amount;

                customer.Items.Add(customerItem);
            });

            OrganizationCore.SaveChanges();


            OrganizationCore.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult EstimateItems()
        {
            var customer = OrganizationCore.Customers.Find(customerId);
            return View(customer);
        }

    }
}
