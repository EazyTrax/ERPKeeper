using ERPKeeperCore.Enterprise.Models.Suppliers;
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

    [Route("/Suppliers/SupplierPayments/{Id:Guid}/{action=index}")]
    public class SupplierPaymentController : _Suppliers_Base_Controller
    {
        public Guid Id
            => Guid.Parse(RouteData.Values["Id"].ToString());

        public IActionResult Index()
        {
            var SupplierPayment = Organization.ErpCOREDBContext.SupplierPayments.Find(Id);
            return View(SupplierPayment);
        }


        public IActionResult Export()
        {
            var SupplierPayment = Organization.ErpCOREDBContext.SupplierPayments.Find(Id);
            return View(SupplierPayment);
        }

        public IActionResult Refresh()
        {
            var SupplierPayment = Organization.ErpCOREDBContext.SupplierPayments.Find(Id);

            SupplierPayment.UpdateBalance();
            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult Update(SupplierPayment model)
        {
            var supplierPayment = Organization.ErpCOREDBContext.SupplierPayments.Find(Id);

            supplierPayment.Date = model.Date;
            supplierPayment.Reference = model.Reference;

            supplierPayment.RetentionTypeId = model.RetentionTypeId;
            supplierPayment.PayFrom_AssetAccountId = model.PayFrom_AssetAccountId;

            supplierPayment.Amount = supplierPayment.Purchase.Total;
            supplierPayment.AmountBankFee = model.AmountBankFee;
            supplierPayment.AmountDiscount = model.AmountDiscount;
            supplierPayment.Memo = model.Memo;


            supplierPayment.UpdateBalance();
            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult Documents()
        {
            var transcation = Organization.SupplierPayments.Find(Id);
            return View(transcation);
        }
        public IActionResult DocumentUpload(IFormFile uploadFile)
        {
            var transcation = Organization.SupplierPayments.Find(Id);

            var file = new ERPKeeperCore.Enterprise.Models.Storage.Document()
            {
                Note = uploadFile.FileName,
                CreatedDate = DateTime.Now,
                Size = (int)uploadFile.Length,
                ContentType = uploadFile.ContentType,
                FileName = uploadFile.FileName,
                FileExtension = System.IO.Path.GetExtension(uploadFile.FileName),
                TransactionId = Id,
                ProfileId = transcation.Purchase.SupplierId,
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
    }
}
