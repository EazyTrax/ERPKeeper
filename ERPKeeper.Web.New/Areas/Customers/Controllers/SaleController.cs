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

    [Route("/{CompanyId}/Customers/Sales/{Id:Guid}/{action=index}")]
    public class SaleController : _Customers_Base_Controller
    {
        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());

        public IActionResult Index()
        {
            var sale = Organization.Sales.Find(Id);
            
            if (sale == null)
                return NotFound();

            sale.UpdateAddress();
            Organization.SaveChanges();

            return View(sale);
        }

        [HttpPost]
        public IActionResult Update(Sale model)
        {
            var transcation = Organization.Sales.Find(Id);

            if (transcation.IsPosted)
                return Redirect(Request.Headers["Referer"].ToString());

            transcation.Memo = model.Memo;
            transcation.Discount = model.Discount;
            transcation.ProjectId = model.ProjectId;
            transcation.Reference = model.Reference;
            transcation.ProfileAddesssId = model.ProfileAddesssId;
            transcation.PaymentTermId = transcation.PaymentTermId;

            transcation.Reorder();
            transcation.UpdateBalance();
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


        [Route("/{CompanyId}/Customers/Sales/{Id:Guid}/Items/Avaliable")]
        public IActionResult Items_Avaliable()
        {
            var transcation = Organization.Sales.Find(Id);
            return View(transcation);
        }

        [Route("/{CompanyId}/Customers/Sales/{Id:Guid}/Items/Add")]
        public IActionResult Items_Add([FromQuery] Guid ItemId)
        {
            var transcation = Organization.Sales.Find(Id);
            var item = Organization.Items.Find(ItemId);
            transcation.AddItem(item);

            Organization.SaveChanges();

            return Redirect($"/{CompanyId}/Customers/Sales/{Id}/Items");
        }


        public IActionResult Payment()
        {
            var transcation = Organization.Sales.Find(Id);
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

            return Redirect($"/{CompanyId}/Customers/Sales/{Id}/Payment");
        }


        public IActionResult Void()
        {
            var transcation = Organization.Sales.Find(Id);
            transcation.SetStatus(SaleStatus.Void);

            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Shipments()
        {
            var transcation = Organization.Sales.Find(Id);
            return View(transcation);
        }



        public IActionResult Documents()
        {
            var transcation = Organization.Sales.Find(Id);
            return View(transcation);
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



    }
}
