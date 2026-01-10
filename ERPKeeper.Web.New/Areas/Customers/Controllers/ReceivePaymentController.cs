using ERPKeeperCore.Enterprise.Models.Customers;
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

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{

    [Route("/Customers/ReceivePayments/{Id:Guid}/{action=index}")]
    public class ReceivePaymentController : _Customers_Base_Controller
    {
        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());

        public IActionResult Index()
        {

            var receivePayment = Organization.ErpCOREDBContext.ReceivePayments.Find(Id);
            return View(receivePayment);
        }

        public IActionResult Export()
        {
            var receivePayment = Organization.ErpCOREDBContext.ReceivePayments.Find(Id);
            return View(receivePayment);
        }

        public IActionResult Update(ReceivePayment model)
        {


            var receivePayment = Organization.ErpCOREDBContext.ReceivePayments.Find(Id);

            if (!receivePayment.IsPosted)
            {
                receivePayment.Date = model.Date;
                receivePayment.RetentionTypeId = model.RetentionTypeId;
                receivePayment.Receivable_Asset_AccountId = model.Receivable_Asset_AccountId;
                receivePayment.AmountBankFee = model.AmountBankFee;
                receivePayment.AmountDiscount = model.AmountDiscount;
                receivePayment.PayToAccountId = model.PayToAccountId;
                receivePayment.UpdateBalance();
            }
            receivePayment.Reference = model.Reference;
            receivePayment.Memo = model.Memo;




            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Documents()
        {
            var transcation = Organization.ReceivePayments.Find(Id);
            return View(transcation);
        }
        [HttpPost]

        public IActionResult DocumentUpload(IFormFile uploadFile)
        {
            var transcation = Organization.ReceivePayments.Find(Id);

            var file = new ERPKeeperCore.Enterprise.Models.Storage.Document()
            {
                Note = uploadFile.FileName,
                CreatedDate = DateTime.Now,
                Size = (int)uploadFile.Length,
                ContentType = uploadFile.ContentType,
                FileName = uploadFile.FileName,
                FileExtension = System.IO.Path.GetExtension(uploadFile.FileName),
                TransactionId = Id,
                ProfileId = transcation.Sale.CustomerId,
                Type = Enterprise.Models.Accounting.Enums.TransactionType.ReceivePayment

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


        public IActionResult Delete()
        {
            var transcation = Organization.ReceivePayments.Find(Id);
            var sale = transcation.Sale;
            Organization.ErpCOREDBContext.ReceivePayments.Remove(transcation);
            Organization.SaveChanges();

            return Redirect($"/Customers/Sales/{sale.Id}");
        }
    }
}
