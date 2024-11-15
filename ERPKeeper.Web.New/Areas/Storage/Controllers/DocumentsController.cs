using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Contents.Controllers
{
    [Area("Storage")]
    public class DocumentsController : DefaultController
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Upload(IFormFile uploadFile)
        {
            var file = new ERPKeeperCore.Enterprise.Models.Storage.Document()
            {
                Note = uploadFile.FileName,
                CreatedDate = DateTime.Now,
                Size = (int)uploadFile.Length,
                ContentType = uploadFile.ContentType,
                FileName = uploadFile.FileName,
                FileExtension = System.IO.Path.GetExtension(uploadFile.FileName),

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
