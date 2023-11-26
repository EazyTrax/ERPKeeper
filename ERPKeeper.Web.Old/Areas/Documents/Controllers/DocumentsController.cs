using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Documents.Controllers
{
    public class DocumentsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

        }


        public ActionResult Home() => View();

        public PartialViewResult _List(Guid id)
        {

            var files = Organization.Documents.GetQuery()
                .Where(file => file.ReferenceGuid == id)
                .OrderBy(fy => fy.RecordTime)
                .ToList();

            return PartialView(files);
        }

        public ActionResult GetFile(Guid id)
        {
            var ExistFile = Organization.Documents.Find(id);
            var ImagesPath = String.Format("/ProfilesData/Documents/");
            var physicalPath = Server.MapPath(ImagesPath + ExistFile.Uid.ToString("D"));

            byte[] filedata = System.IO.File.ReadAllBytes(physicalPath);
            string contentType = MimeMapping.GetMimeMapping(physicalPath);
            return File(physicalPath, contentType, ExistFile.FileName);
        }


        [ValidateInput(false)]
        public PartialViewResult PartialGridView(Guid? id)
        {
            if (id == null)
            {
                var model = Organization.Documents.GetQuery()
                    .Where(d => d.ReferenceGuid == null)
                    .OrderBy(fy => fy.RecordTime)
                    .ToList();
                return PartialView("PartialGridView", model);
            }
            else
            {
                var model = Organization.Documents.GetQuery()
                    .Where(file => file.ReferenceGuid == id)
                    .OrderBy(fy => fy.RecordTime)
                    .ToList();
                return PartialView("PartialGridView", model);
            }
        }


        public ActionResult SummaryList(Guid id)
        {
            var model = Organization.Documents.GetQuery()
                .Where(file => file.ReferenceGuid == id)
                .OrderBy(fy => fy.RecordTime)
                .ToList();

            return PartialView(model);
        }
        public ActionResult Reorder()
        {
            Organization.Documents.ReOrder();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Matching()
        {
            Organization.Documents.Matching();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Upload()
        {
            return View();
        }

        public PartialViewResult _FileUploadForm(Guid? id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        public ActionResult FileUpload(Guid? id, IEnumerable<DevExpress.Web.UploadedFile> UploadControl)
        {
            var memo = DevExpress.Web.Mvc.EditorExtension.GetValue<string>("Memo");
            var typeString = Request.Params["TypeString"];

            foreach (var uploadFile in UploadControl)
            {
                var newDocument = new ERPKeeper.Node.Models.Documents.Document()
                {
                    Uid = Guid.NewGuid(),
                    FileName = uploadFile.FileName,
                    Name = uploadFile.FileName.Replace(".pdf", string.Empty),
                    Size = uploadFile.ContentLength,
                    RecordTime = DateTime.Now,
                    Content = uploadFile.FileBytes,
                    ContentType = uploadFile.ContentType,
                    Uploader = CurrentUser.Name,
                    ReferenceGuid = id,
                    TypeString = typeString,
                    Memo = memo
                };

                Organization.Documents.Create(newDocument);
            }

            Organization.SaveChanges();


            return View();
        }






    }
}
