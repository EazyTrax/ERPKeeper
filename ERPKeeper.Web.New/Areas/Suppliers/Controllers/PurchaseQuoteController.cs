using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Suppliers;
using ERPKeeperCore.Enterprise.Models.Suppliers.Enums;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{
    [Route("/Suppliers/PurchaseQuotes/{Id:Guid}/{action=index}")]
    public class PurchaseQuoteController : _Suppliers_Base_Controller
    {

        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());

        public IActionResult Index()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);
            return View(transcation);
        }
        public IActionResult Refresh()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);
            transcation.Reorder();
            transcation.UpdateBalance();
            transcation.UpdateName();

            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Void()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);
            transcation.Status = PurchaseQuoteStatus.Void;
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Close()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);
            transcation.Status = PurchaseQuoteStatus.Close;
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Items()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);

            transcation.UpdateItems();

            Organization.SaveChanges();
            return View(transcation);
        }

        [Route("/Suppliers/PurchaseQuotes/{Id:Guid}/Items/Avaliable")]
        public IActionResult Items_Avaliable()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);
            return View(transcation);
        }

        [Route("/Suppliers/PurchaseQuotes/{Id:Guid}/Items/Add")]
        public IActionResult Items_Add([FromQuery] Guid ItemId)
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);
            var item = Organization.Items.Find(ItemId);
            transcation.AddItem(item);
            Organization.SaveChanges();

            return Redirect($"/Suppliers/PurchaseQuotes/{Id}/Items");
        }

        [HttpPost]
        public IActionResult Update(PurchaseQuote model)
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);

            transcation.Date = model.Date;
            transcation.Memo = model.Memo;
            transcation.Discount = model.Discount;
            transcation.No = model.No;
            transcation.Project = model.Project;
            transcation.Reference = model.Reference;
            transcation.PaymentTermId = model.PaymentTermId;
            transcation.TaxCodeId = model.TaxCodeId;


            transcation.UpdateBalance();
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Export(String DocumentName = "ใบสั่งซื้อ(PO)")
        {
            ViewBag.DocumentName = DocumentName;

            var transcation = Organization.PurchaseQuotes.Find(Id);
            transcation.UpdateBalance();
            Organization.SaveChanges();

            return View(transcation);
        }



        public IActionResult Order()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);
            transcation.SetStatus(PurchaseQuoteStatus.Order);
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Delete()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);

            Organization.PurchaseQuotes.Delete(transcation);
            Organization.SaveChanges();

            return Redirect($"/Suppliers/Suppliers");
        }
        public IActionResult Documents()
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);
            return View(transcation);
        }
        public IActionResult DocumentUpload(IFormFile uploadFile)
        {
            var transcation = Organization.PurchaseQuotes.Find(Id);

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
                Type = Enterprise.Models.Accounting.Enums.TransactionType.PurchaseQuote

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
        public IActionResult ConvertToInvoice()
        {
            var PurchaseQuote = Organization.PurchaseQuotes.Find(Id);
            var Purchase = Organization.Purchases.CreateInvoice(PurchaseQuote);

            Purchase.TaxCodeId = PurchaseQuote.TaxCodeId;
            // Purchase.ExpenseAccountId = Organization.SystemAccounts.Expense.Id;

            Organization.SaveChanges();

            string returnUrl = $"/Customers/Purchases/{Purchase.Id}/";

            return Redirect(returnUrl);
        }
    }
}
