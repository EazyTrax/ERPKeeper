using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Profiles_Customers.Controllers
{

    [Route("/{CompanyId}/Profiles/Customers/Sales/{TransactionId:Guid}/{action=index}")]
    public class SaleController : _Profiles_Customers_Base_Controller
    {
        public Guid TransactionId => Guid.Parse(RouteData.Values["TransactionId"].ToString());

        public IActionResult Index()
        {
            var sale = OrganizationCore.Sales.Find(TransactionId);

            sale.PostToTransaction();
            OrganizationCore.SaveChanges();

            return View(sale);
        }

        public IActionResult Update(Sale model)
        {
            var transcation = OrganizationCore.SaleQuotes.Find(TransactionId);

            if (transcation.IsPosted)
                return Redirect(Request.Headers["Referer"].ToString());

            transcation.Memo = model.Memo;
            transcation.Discount = model.Discount;

            transcation.UpdateBalance();
            OrganizationCore.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Items()
        {
            var transcation = OrganizationCore.Sales.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Payments()
        {
            var transcation = OrganizationCore.Sales.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Shipments()
        {
            var transcation = OrganizationCore.Sales.Find(TransactionId);
            return View(transcation);
        }



        public IActionResult Documents()
        {
            var transcation = OrganizationCore.Sales.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Export()
        {
            var transcation = OrganizationCore.Sales.Find(TransactionId);
            transcation.UpdateBalance();

            return View(transcation);
        }
    }
}
