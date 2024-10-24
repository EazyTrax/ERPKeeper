using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{

    [Route("/{CompanyId}/Customers/Sales/{TransactionId:Guid}/{action=index}")]
    public class SaleController : _Customers_Base_Controller
    {
        public Guid TransactionId => Guid.Parse(RouteData.Values["TransactionId"].ToString());

        public IActionResult Index()
        {
            var sale = Organization.Sales.Find(TransactionId);

            if (sale == null)
                return NotFound();

            return View(sale);
        }

        [HttpPost]
        public IActionResult Update(Sale model)
        {
            var transcation = Organization.Sales.Find(TransactionId);

            if (transcation.IsPosted)
                return Redirect(Request.Headers["Referer"].ToString());

            transcation.Memo = model.Memo;
            transcation.Discount = model.Discount;
            transcation.ProjectId = model.ProjectId;

            transcation.UpdateBalance();
            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Items()
        {
            var transcation = Organization.Sales.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Payments()
        {
            var transcation = Organization.Sales.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Shipments()
        {
            var transcation = Organization.Sales.Find(TransactionId);
            return View(transcation);
        }



        public IActionResult Documents()
        {
            var transcation = Organization.Sales.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Export()
        {
            var transcation = Organization.Sales.Find(TransactionId);
            transcation.UpdateBalance();

            return View(transcation);
        }
    }
}
