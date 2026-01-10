using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{
    [Route("/Customers/Sales/{Id:Guid}/{action=index}")]
    public class SaleController : _Customers_Base_Controller
    {
        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());

        public IActionResult Index()
        {
            var sale = Organization.Sales.Find(Id);

            if (sale == null)
                return Redirect($"/Customers/Sales");

            sale.UpdateAddress();
            Organization.SaveChanges();

            return View(sale);
        }

        public IActionResult Issue(Sale model)
        {
            var transcation = Organization.Sales.Find(Id);

            if (transcation.Status == SaleStatus.Paid)
                transcation.SetStatus(SaleStatus.Open);

            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Refresh()
        {
            var transcation = Organization.Sales.Find(Id);
            transcation.Reorder();
            transcation.UpdateBalance();
            transcation.UpdateName();

            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Post()
        {
            var transcation = Organization.Sales.Find(Id);
            transcation.Post_Ledgers();
            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult UnPost()
        {
            var transcation = Organization.Sales.Find(Id);
            transcation.UnPostLedger();
            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }


        [HttpPost]
        public IActionResult Update(Sale model)
        {
            var transcation = Organization.Sales.Find(Id);

            if (!transcation.IsPosted)
            {
                transcation.Discount = model.Discount;
                transcation.TaxCodeId = model.TaxCodeId;
                transcation.UpdateBalance();
            }

            transcation.Memo = model.Memo;
            transcation.Date = model.Date.Date;
            transcation.ProjectId = model.ProjectId;
            transcation.Reference = model.Reference;
            transcation.ProfileAddesssId = model.ProfileAddesssId;
            transcation.ShipmentAddesssId = model.ShipmentAddesssId;
            transcation.PaymentTermId = model.PaymentTermId;
            

            transcation.Reorder();
            transcation.UpdateName();
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Items()
        {
            var transcation = Organization.Sales.Find(Id);
            transcation.UpdateItems();

            Organization.SaveChanges();

            return View(transcation);
        }

        public IActionResult Items_Add()
        {
            var transcation = Organization.Sales.Find(Id);
            return View(transcation);
        }


        [Route("/Customers/Sales/{Id:Guid}/Items/Avaliable")]
        public IActionResult Items_Avaliable()
        {
            var transcation = Organization.Sales.Find(Id);
            return View(transcation);
        }

        [Route("/Customers/Sales/{Id:Guid}/Items/Add")]
        public IActionResult Items_Add([FromQuery] Guid ItemId)
        {
            var transcation = Organization.Sales.Find(Id);
            var item = Organization.Items.Find(ItemId);
            transcation.AddItem(item);

            Organization.SaveChanges();

            return Redirect($"/Customers/Sales/{Id}/Items");
        }

        public IActionResult Payment()
        {
            var transcation = Organization.Sales.Find(Id);

            if (transcation.ReceivePayment != null)
                return Redirect($"/Customers/ReceivePayments/{transcation.ReceivePayment.Id}/");

            return View(transcation);
        }

        public IActionResult Pay(String Date)
        {
            var Sale = Organization.Sales.Find(Id);
            DateTime payDate = DateTime.Today;

            if (Date == "SaleDate")
                payDate = Sale.Date;

            if (Sale.ReceivePayment == null)
            {
                Sale.ReceivePayment = new Enterprise.Models.Customers.ReceivePayment()
                {
                    Date = payDate,
                    Amount = Sale.Total,
                    Receivable_Asset_Account = Organization.SystemAccounts.Cash
                };
                Organization.SaveChanges();
            }

            return Redirect($"/Customers/Sales/{Id}/Payment");
        }

        public IActionResult Delete()
        {
            var transcation = Organization.Sales.Find(Id);


            transcation.Items.Clear();
            Organization.ErpCOREDBContext.Sales.Remove(transcation);

            Organization.SaveChanges();
            return Redirect($"/Customers/Sales");
        }

        public IActionResult Void()
        {
            var transcation = Organization.Sales.Find(Id);
            transcation.SetStatus(SaleStatus.Open);

            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Shipments()
        {
            var transcation = Organization.Sales.Find(Id);
            Organization.ErpCOREDBContext.Shipments.Where(x => x.TransactionId == Id && x.ShipmentAddesssId == null)
                .ToList()
                .ForEach(x => x.ShipmentAddesssId = transcation.ShipmentAddesssId);
            Organization.ErpCOREDBContext.SaveChanges();

            return View(transcation);
        }

        public IActionResult Documents()
        {
            var transcation = Organization.Sales.Find(Id);
            return View(transcation);
        }

        [HttpPost]
        public IActionResult DocumentUpload(IFormFile uploadFile)
        {
            var transcation = Organization.Sales.Find(Id);

            var file = new ERPKeeperCore.Enterprise.Models.Storage.Document()
            {
                Note = uploadFile.FileName,
                CreatedDate = DateTime.Now,
                Size = (int)uploadFile.Length,
                ContentType = uploadFile.ContentType,
                FileName = uploadFile.FileName,
                FileExtension = System.IO.Path.GetExtension(uploadFile.FileName),
                TransactionId = Id,
                ProfileId = transcation.Customer.ProfileId,
                Type = Enterprise.Models.Accounting.Enums.TransactionType.Sale

            };

            Organization.ErpCOREDBContext.Documents.Add(file);
            Organization.SaveChanges();

            file.AddContent(CompanyId, GetByteArrayFromIFormFile(uploadFile));


            return Ok();
        }

        public static byte[] GetByteArrayFromIFormFile(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public IActionResult Export(string documentName = "ใบกำกับภาษี")
        {
            var transcation = Organization.Sales.Find(Id);
            transcation.Reorder();
            transcation.UpdateBalance();
            Organization.SaveChanges();

            ViewBag.DocumentName = documentName;

            return View(transcation);
        }
        public IActionResult Export_DeliveryNote(string documentName = "ใบส่งของ")
        {
            var transcation = Organization.Sales.Find(Id);
            transcation.Reorder();
            transcation.UpdateBalance();
            Organization.SaveChanges();

            ViewBag.DocumentName = documentName;

            return View(transcation);
        }

        
        public IActionResult ExportInvat(string documentName = "ใบกำกับภาษี")
        {
            var transcation = Organization.Sales.Find(Id);
            transcation.Reorder();
            transcation.UpdateBalance();
            Organization.SaveChanges();

            ViewBag.DocumentName = documentName;

            return View(transcation);
        }

        public IActionResult ExportShipmentLabel()
        {
            var sale = Organization.Sales.Find(Id);
            return View(sale);
        }


        [Route("/Customers/Sales/{Id:Guid}/Shipments/{ShipmentId}/ExportShipmentLabel")]

        public IActionResult ExportShipmentLabel(Guid ShipmentId)
        {
            ViewBag.ShipmentId = ShipmentId;
            var sale = Organization.Sales.Find(Id);
            return View(sale);
        }


        [Route("/Customers/Sales/{Id:Guid}/Shipments/{ShipmentId}/ExportPDFShipmentLabel")]
        public IActionResult ExportPDFShipmentLabel(Guid shipmentId)
        {
            var sale = Organization.Sales.Find(Id);
            var shipment = Organization.ErpCOREDBContext.Shipments.FirstOrDefault(s => s.Id == shipmentId);


            var company = Organization.Profiles.GetSelf();
            var company_address = company.Addresses.OrderByDescending(a => a.IsPrimary).FirstOrDefault();

            if (shipment == null)
                return NotFound("Shipment not found");

            byte[] pdfContents = shipment.GeneratePDF(company_address);
            return File(pdfContents, "application/pdf", $"{sale.Name}-Shipment.pdf");
        }
    }
}
