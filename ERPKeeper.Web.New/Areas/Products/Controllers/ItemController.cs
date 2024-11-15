
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ERPKeeperCore.Web.Areas.Products.Controllers
{
    [Route("/{CompanyId}/Products/Items/{ItemUid:Guid}/{action=Index}")]
    public class ItemController : Base_ProductsController
    {
        public Guid ItemUid => Guid.Parse(RouteData.Values["ItemUid"].ToString());
        public Enterprise.Models.Items.Item Item => Organization.Items.Find(ItemUid);

        public IActionResult Index() => View(Item);
        public IActionResult Quotes() => View(Item);
        public IActionResult Purchases() => View(Item);
        public IActionResult Sales() => View(Item);
        public IActionResult Customers() => View(Item);
        public IActionResult Suppliers() => View(Item);

        [HttpPost]
        public IActionResult Update(ERPKeeperCore.Enterprise.Models.Items.Item model)
        {

            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

      


      

        public IActionResult UpdateStock()
        {
            
            return Redirect(Request.Headers["Referer"].ToString());
        }



        public IActionResult Documents()
        {
            var transcation = Organization.Items.Find(ItemUid);
            return View(transcation);
        }

        [HttpPost]
        public IActionResult DocumentUpload(IFormFile uploadFile)
        {
            var transcation = Organization.Items.Find(ItemUid);

            var file = new ERPKeeperCore.Enterprise.Models.Storage.Document()
            {
                Note = uploadFile.FileName,
                CreatedDate = DateTime.Now,
                Size = (int)uploadFile.Length,
                ContentType = uploadFile.ContentType,
                FileName = uploadFile.FileName,
                FileExtension = System.IO.Path.GetExtension(uploadFile.FileName),
                TransactionId = ItemUid,
                Type = Enterprise.Models.Accounting.Enums.TransactionType.ProductItem

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
