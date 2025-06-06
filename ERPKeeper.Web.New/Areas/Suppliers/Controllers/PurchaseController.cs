﻿using ERPKeeperCore.Enterprise.Models.Suppliers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{
    [Route("/{CompanyId}/Suppliers/Purchases/{Id:Guid}/{action=index}")]
    public class PurchaseController : _Suppliers_Base_Controller
    {
        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());

        public IActionResult Index()
        {
            var transcation = Organization.Purchases.Find(Id);
            return View(transcation);
        }
       
        public IActionResult Refresh()
        {
            var transcation = Organization.Purchases.Find(Id);
            transcation.Reorder();
            transcation.UpdateBalance();
            transcation.UpdateName();

            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        

      







        [HttpPost]
        public IActionResult Update(Purchase model)
        {
            var transcation = Organization.Purchases.Find(Id);

            transcation.Date = model.Date;
            transcation.Memo = model.Memo;
            transcation.Discount = model.Discount;
            transcation.No = model.No;
            transcation.Project = model.Project;
            transcation.Reference = model.Reference;
            transcation.PaymentTermId = model.PaymentTermId;
            transcation.TaxCodeId = model.TaxCodeId;
            transcation.ExpenseAccountId = model.ExpenseAccountId;

            transcation.UpdateBalance();
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
      

        public IActionResult UnPost()
        {
            var model = Organization.Purchases.Find(Id);
            Organization.Purchases.UnPost(model);
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Items()
        {
            var transcation = Organization.Purchases.Find(Id);
            transcation.UpdateItems();

            Organization.SaveChanges();
            return View(transcation);
        }

        [Route("/{CompanyId}/Suppliers/Purchases/{Id:Guid}/Items/Avaliable")]
        public IActionResult Items_Avaliable()
        {
            var transcation = Organization.Purchases.Find(Id);
            return View(transcation);
        }
        [Route("/{CompanyId}/Suppliers/Purchases/{Id:Guid}/Items/Add")]
        public IActionResult Items_Add([FromQuery] Guid ItemId)
        {
            var transcation = Organization.Purchases.Find(Id);
            var item = Organization.Items.Find(ItemId);
            transcation.AddItem(item);
            Organization.SaveChanges();

            return Redirect($"/{CompanyId}/Suppliers/Purchases/{Id}/Items");
        }
        public IActionResult Payment()
        {
            var transcation = Organization.Purchases.Find(Id);
            return View(transcation);
        }
        public IActionResult Pay(String Date)
        {
            var purchase = Organization.Purchases.Find(Id);
            DateTime payDate = DateTime.Today;

            if (Date == "PurchaseDate")
                payDate = purchase.Date;

            if (purchase.SupplierPayment == null)
            {
                purchase.SupplierPayment = new Enterprise.Models.Suppliers.SupplierPayment()
                {
                    Date = payDate,
                    Amount = purchase.Total,
                    AssetAccount_PayFrom = Organization.SystemAccounts.Cash,
                };

                purchase.Status = Enterprise.Models.Suppliers.Enums.PurchaseStatus.Paid;
                Organization.SaveChanges();
            }

            return Redirect($"/{CompanyId}/Suppliers/SupplierPayments/{purchase.SupplierPayment.Id}");
        }
        public IActionResult Shipments()
        {
            var transcation = Organization.Purchases.Find(Id);
            return View(transcation);
        }

        public IActionResult Documents()
        {
            var transcation = Organization.Purchases.Find(Id);
            return View(transcation);
        }


        [HttpPost]
        public IActionResult DocumentUpload(IFormFile uploadFile)
        {
            var transcation = Organization.Purchases.Find(Id);

            var file = new ERPKeeperCore.Enterprise.Models.Storage.Document()
            {
                Note = uploadFile.FileName,
                CreatedDate = DateTime.Now,
                Size = (int)uploadFile.Length,
                ContentType = uploadFile.ContentType,
                FileName = uploadFile.FileName,
                FileExtension = System.IO.Path.GetExtension(uploadFile.FileName),
                TransactionId = Id,
                ProfileId = transcation.Supplier.Id,
                Type = Enterprise.Models.Accounting.Enums.TransactionType.Purchase

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

        public IActionResult Export()
        {
            var transcation = Organization.Purchases.Find(Id);
            return View(transcation);
        }

        public IActionResult Delete()
        {
            var transcation = Organization.Purchases.Find(Id);

            if (Organization.ErpCOREDBContext.Purchases.Max(s => s.No) == transcation.No)
            {
                transcation.Items.Clear();
            }

            Organization.ErpCOREDBContext.Purchases.Remove(transcation);
            Organization.SaveChanges();

            return Redirect($"/{CompanyId}/Suppliers/Purchases");
        }

    }
}
