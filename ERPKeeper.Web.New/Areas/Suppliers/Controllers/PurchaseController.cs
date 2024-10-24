using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{

    [Route("/{CompanyId}/Suppliers/Purchases/{transactionId:Guid}/{action=index}")]
    public class PurchaseController : _Suppliers_Base_Controller
    {

        public IActionResult Index(Guid transactionId)
        {
            var transcation = Organization.Purchases.Find(transactionId);
            return View(transcation);
        }
        public IActionResult UnPost(Guid transactionId)
        {
            var model =   Organization.Purchases.Find(transactionId);
            Organization.Purchases.UnPost(model);
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
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
