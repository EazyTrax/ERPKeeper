using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{
    [Route("/{CompanyId}/{area}/Customers/{customerUid:Guid}/{action=Index}")]
    public class CustomerController : Base_CustomersController
    {
        public IActionResult Index(Guid customerUid)
        {
            var customer = EnterpriseRepo.Customers.Find(customerUid);
            return View(customer);
        }

        public IActionResult Sales(Guid customerUid)
        {
            var customer = EnterpriseRepo.Customers.Find(customerUid);
            return View(customer);
        }
        public IActionResult Quotes(Guid customerUid)
        {
            var customer = EnterpriseRepo.Customers.Find(customerUid);
            return View(customer);
        }

        public IActionResult SaleItems(Guid customerUid)
        {
            var customer = EnterpriseRepo.Customers.Find(customerUid);
            return View(customer);
        }
        public IActionResult EstimateItems(Guid customerUid)
        {
            var customer = EnterpriseRepo.Customers.Find(customerUid);
            return View(customer);
        }



    }
}
