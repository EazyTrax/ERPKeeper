using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeper.Web.New.Controllers;

namespace ERPKeeper.Web.New.Areas.Customers.Controllers
{
    [Route("/{CompanyId}/{area}/Customers/{customerUid:Guid}/{action=Index}")]
    public class CustomerController : Base_CustomersController
    {
        public IActionResult Index(Guid customerUid)
        {
            var customer = Organization.Customers.Find(customerUid);
            return View(customer);
        }

        public IActionResult Sales(Guid customerUid)
        {
            var customer = Organization.Customers.Find(customerUid);
            return View(customer);
        }
        public IActionResult Estimates(Guid customerUid)
        {
            var customer = Organization.Customers.Find(customerUid);
            return View(customer);
        }

        public IActionResult SaleItems(Guid customerUid)
        {
            var customer = Organization.Customers.Find(customerUid);
            return View(customer);
        }
        public IActionResult EstimateItems(Guid customerUid)
        {
            var customer = Organization.Customers.Find(customerUid);
            return View(customer);
        }



    }
}
