using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{

    [Route("/{CompanyId}/Customers/Sales/{SaleId:Guid}/{action=index}")]
    public class SaleController : _Customers_Base_Controller
    {
        public Guid SaleId => Guid.Parse(RouteData.Values["SaleId"].ToString());

        public IActionResult Index()
        {
            var sale = Organization.Sales.Find(SaleId);

            if (sale == null)
                return Redirect($"/{CompanyId}/Customers/Sales");

            sale.UpdateAddress();
            Organization.SaveChanges();

            return View(sale);
        }


        public IActionResult Issue(Sale model)
        {
            var transcation = Organization.Sales.Find(SaleId);

            if (transcation.Status == SaleStatus.Paid)
                transcation.SetStatus(SaleStatus.Invoice);

            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        

        [HttpPost]
        public IActionResult Update(Sale model)
        {
            var transcation = Organization.Sales.Find(SaleId);

            if (transcation.IsPosted)
                return Redirect(Request.Headers["Referer"].ToString());

            transcation.Memo = model.Memo;
            transcation.Date = model.Date.Date;
            transcation.Discount = model.Discount;
            transcation.ProjectId = model.ProjectId;
            transcation.Reference = model.Reference;
            transcation.ProfileAddesssId = model.ProfileAddesssId;
            transcation.PaymentTermId = model.PaymentTermId;

            transcation.TaxCodeId = model.TaxCodeId;
            transcation.Reorder();
            transcation.UpdateBalance();
            transcation.UpdateName();

            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Items()
        {
            var transcation = Organization.Sales.Find(SaleId);
            transcation.UpdateItems();

            Organization.SaveChanges();

            return View(transcation);
        }
        public IActionResult Items_Add()
        {
            var transcation = Organization.Sales.Find(SaleId);
            return View(transcation);
        }


        [Route("/{CompanyId}/Customers/Sales/{SaleId:Guid}/Items/Avaliable")]
        public IActionResult Items_Avaliable()
        {
            var transcation = Organization.Sales.Find(SaleId);
            return View(transcation);
        }

        [Route("/{CompanyId}/Customers/Sales/{SaleId:Guid}/Items/Add")]
        public IActionResult Items_Add([FromQuery] Guid ItemId)
        {
            var transcation = Organization.Sales.Find(SaleId);
            var item = Organization.Items.Find(ItemId);
            transcation.AddItem(item);

            Organization.SaveChanges();

            return Redirect($"/{CompanyId}/Customers/Sales/{SaleId}/Items");
        }


        public IActionResult Payment()
        {
            var transcation = Organization.Sales.Find(SaleId);

            if (transcation.ReceivePayment != null)
                return Redirect($"/{CompanyId}/Customers/ReceivePayments/{transcation.ReceivePayment.Id}/");

            return View(transcation);
        }

        public IActionResult Pay(String Date)
        {
            var Sale = Organization.Sales.Find(SaleId);
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

            return Redirect($"/{CompanyId}/Customers/Sales/{SaleId}/Payment");
        }

        public IActionResult Delete()
        {
            var transcation = Organization.Sales.Find(SaleId);

            if (Organization.ErpCOREDBContext.Sales.Max(s => s.No) == transcation.No)
            {
                transcation.Items.Clear();
            }

            Organization.ErpCOREDBContext.Sales.Remove(transcation);
            Organization.SaveChanges();

            return Redirect($"/{CompanyId}/Customers/Sales");
        }



        public IActionResult Void()
        {
            var transcation = Organization.Sales.Find(SaleId);
            transcation.SetStatus(SaleStatus.Invoice);

            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Shipments()
        {
            var transcation = Organization.Sales.Find(SaleId);
            return View(transcation);
        }



        public IActionResult Documents()
        {
            var transcation = Organization.Sales.Find(SaleId);
            return View(transcation);
        }

        public IActionResult Export(string documentName = "ใบกำกับภาษี")
        {
            var transcation = Organization.Sales.Find(SaleId);
            transcation.Reorder();
            transcation.UpdateBalance();
            Organization.SaveChanges();

            ViewBag.DocumentName = documentName;

            return View(transcation);
        }



        public IActionResult ExportShipmentLabel()
        {
            var sale = Organization.Sales.Find(SaleId);


            return View(sale);
        }


        [Route("/{CompanyId}/Customers/Sales/{SaleId:Guid}/Shipments/{ShipmentId}/ExportShipmentLabel")]
        public IActionResult ExportShipmentLabel(Guid ShipmentId)
        {
            ViewBag.ShipmentId = ShipmentId;

            var sale = Organization.Sales.Find(SaleId);
            return View(sale);
        }

    }
}
