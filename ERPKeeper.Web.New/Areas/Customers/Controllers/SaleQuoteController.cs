using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{
    [Route("/Customers/SaleQuotes/{Id:Guid}/{action=index}")]
    public class SaleQuoteController : _Customers_Base_Controller
    {

        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());

        public IActionResult Index()
        {
            var transcation = Organization.SaleQuotes.Find(Id);
            transcation.UpdateAddress();
            Organization.SaveChanges();

            if (transcation == null)
                return Redirect($"/Customers/SaleQuotes");

            return View(transcation);
        }


        public IActionResult Refresh()
        {
            var transcation = Organization.SaleQuotes.Find(Id);
            transcation.Reorder();
            transcation.UpdateBalance();
            transcation.UpdateName();

            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Items()
        {
            var transcation = Organization.SaleQuotes.Find(Id);
            transcation.UpdateItems();

            Organization.SaveChanges();

            return View(transcation);
        }

        [Route("/Customers/SaleQuotes/{Id:Guid}/Items/Avaliable")]
        public IActionResult Items_Avaliable()
        {
            var transcation = Organization.SaleQuotes.Find(Id);
            return View(transcation);
        }

        [Route("/Customers/SaleQuotes/{Id:Guid}/Items/Add")]
        public IActionResult Items_Add([FromQuery] Guid ItemId)
        {
            var transcation = Organization.SaleQuotes.Find(Id);
            var item = Organization.Items.Find(ItemId);
            transcation.AddItem(item);

            Organization.SaveChanges();

            return Redirect($"/Customers/SaleQuotes/{Id}/Items");
        }

        [HttpPost]
        public IActionResult Update(SaleQuote model)
        {
            var saleQuote = Organization.SaleQuotes.Find(Id);

            saleQuote.Memo = model.Memo;
            saleQuote.Date = model.Date.Date;
            saleQuote.Discount = model.Discount;
            saleQuote.No = model.No;
            saleQuote.ProfileAddesssId = model.ProfileAddesssId;
            saleQuote.ProjectId = model.ProjectId;
            saleQuote.Reference = model.Reference;
            saleQuote.PaymentTermId = model.PaymentTermId;
            saleQuote.IsPriceTaxInclude = model.IsPriceTaxInclude;

            saleQuote.UpdateBalance();
            saleQuote.UpdateName();

            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Export()
        {
            var transcation = Organization.SaleQuotes.Find(Id);
            transcation.UpdateBalance();
            Organization.SaveChanges();

            return View(transcation);
        }
        public IActionResult Documents()
        {
            var transcation = Organization.SaleQuotes.Find(Id);
            return View(transcation);
        }

        [HttpPost]
        public IActionResult DocumentUpload(IFormFile uploadFile)
        {
            var transcation = Organization.SaleQuotes.Find(Id);

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
                Type = Enterprise.Models.Accounting.Enums.TransactionType.SaleQuote

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
        public IActionResult Void()
        {
            var transcation = Organization.SaleQuotes.Find(Id);
            transcation.SetStatus(SaleQuoteStatus.Void);

            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Clone()
        {
            var quote = Organization.SaleQuotes.Find(Id);
            var newQuote = Organization.SaleQuotes.Clone(quote);


            var redirectUrl = $"/Customers/SaleQuotes/{newQuote.Id}/";

            return Redirect(redirectUrl);
        }
        public IActionResult Quote()
        {
            var transcation = Organization.SaleQuotes.Find(Id);
            transcation.SetStatus(SaleQuoteStatus.Open);

            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Order()
        {
            var transcation = Organization.SaleQuotes.Find(Id);
            transcation.SetStatus(SaleQuoteStatus.Order);

            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Delete()
        {
            var transcation = Organization.SaleQuotes.Find(Id);

            Organization.SaleQuotes.Delete(transcation);
            Organization.SaveChanges();

            return Redirect($"/Customers/SaleQuotes");
        }
        public IActionResult ConvertToInvoice()
        {
            var saleQuote = Organization.SaleQuotes.Find(Id);
            var sale = Organization.Sales.CreateInvoice(saleQuote);

            sale.TaxCodeId = Organization.TaxCodes.GetDefault(Enterprise.Models.Taxes.Enums.TaxDirection.Output).Id;
            sale.IncomeAccountId = Organization.SystemAccounts.Income.Id;


            Organization.SaveChanges();

            string returnUrl = $"/Customers/Sales/{sale.Id}/";

            return Redirect(returnUrl);
        }
        public IActionResult ToggleTaxInclude()
        {
            var transcation = Organization.SaleQuotes.Find(Id);


            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
