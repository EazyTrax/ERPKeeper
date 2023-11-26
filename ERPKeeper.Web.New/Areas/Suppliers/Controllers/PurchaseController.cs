using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Suppliers.Controllers
{

    [Route("/{CompanyId}/{area}/Purchases/{transactionId:Guid}/{action=index}")]
    public class PurchaseController : Base_SuppliersController
    {

        public IActionResult Index(Guid transactionId)
        {
            var transcation = Organization.Purchases.Find(transactionId);
            return View(transcation);
        }


        public IActionResult Items(Guid transactionId)
        {
            var transcation = Organization.Purchases.Find(transactionId);
            return View(transcation);
        }

        public IActionResult Payments(Guid transactionId)
        {
            var transcation = Organization.Purchases.Find(transactionId);
            return View(transcation);
        }

        public IActionResult Shipments(Guid transactionId)
        {
            var transcation = Organization.Purchases.Find(transactionId);
            return View(transcation);
        }

        public IActionResult Documents(Guid transactionId)
        {
            var transcation = Organization.Purchases.Find(transactionId);
            return View(transcation);
        }

        public IActionResult Export(Guid transactionId)
        {
            var transcation = Organization.Purchases.Find(transactionId);
            return View(transcation);
        }
    }
}
