using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{

    [Route("/{CompanyId}/{area}/Sales/{TransactionId:Guid}/{action=index}")]
    public class SaleController : Base_CustomersController
    {
        public Guid TransactionId => Guid.Parse(RouteData.Values["TransactionId"].ToString());

        public IActionResult Index()
        {
            var transcation = EnterpriseRepo.Sales.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Items()
        {
            var transcation = EnterpriseRepo.Sales.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Payments()
        {
            var transcation = EnterpriseRepo.Sales.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Shipments()
        {
            var transcation = EnterpriseRepo.Sales.Find(TransactionId);
            return View(transcation);
        }



        [Route("/{CompanyId}/{area}/Sales/{TransactionId:Guid}/Shipments/{ShipmentId:Guid}/Export")]
        public IActionResult ExportShipment(Guid shipmentId)
        {
            var transcation = EnterpriseRepo.CommercialShipments.Find(shipmentId);
            var ms = EnterpriseRepo.CommercialShipments.ExportPDF(shipmentId);
            ms.Position = 0;

            return File(fileStream: ms, contentType: "application/pdf", fileDownloadName: $"Shipment_{shipmentId}.pdf");
        }


        public IActionResult Documents()
        {
            var transcation = EnterpriseRepo.Sales.Find(TransactionId);
            return View(transcation);
        }

        public IActionResult Export()
        {
            var transcation = EnterpriseRepo.Sales.Find(TransactionId);
            return View(transcation);
        }
    }
}
