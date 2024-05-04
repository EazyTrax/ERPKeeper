using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Profiles_Customers.Controllers
{
    [Route("/{CompanyId}/Profiles/Customers/Customers/{customerUid:Guid}/{action=Index}")]
    public class CustomerController : _Profiles_Customers_Base_Controller
    {
        public IActionResult Index(Guid customerUid)
        {
            var customer = OrganizationCore.Customers.Find(customerUid);
            return View(customer);
        }

        public IActionResult Sales(Guid customerUid)
        {
            var customer = OrganizationCore.Customers.Find(customerUid);
            return View(customer);
        }
        public IActionResult Quotes(Guid customerUid)
        {
            var customer = OrganizationCore.Customers.Find(customerUid);
            return View(customer);
        }

        public IActionResult SaleItems(Guid customerUid)
        {
            var customer = OrganizationCore.Customers.Find(customerUid);
            return View(customer);
        }
        public IActionResult EstimateItems(Guid customerUid)
        {
            var customer = OrganizationCore.Customers.Find(customerUid);
            return View(customer);
        }



    }
}
