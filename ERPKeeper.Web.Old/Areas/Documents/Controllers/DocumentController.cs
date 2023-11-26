using DevExpress.Pdf;
using ERPKeeper.Node.Models.Documents;
using ERPKeeper.WebFrontEnd.Areas.Documents.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Documents.Controllers
{
    public class DocumentController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
        public ActionResult Home(Guid id)
        {
            ViewBag.id = id;
            var ExistFile = Organization.Documents.Find(id);

            if (ExistFile == null)
                return RedirectToAction("Home", "Documents", new { Area = "Documents" });

            return View(ExistFile);
        }

        public ActionResult Save(Document document)
        {
            ViewBag.id = document.Uid;

            var exDocument = Organization.Documents.Find(document.Uid);

            if (exDocument != null)
            {
                exDocument.Name = document.Name;
                exDocument.ObjectName = document.ObjectName;
                exDocument.TotalValue = document.TotalValue;
                Organization.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult PDFViewer(Guid id)
        {
            ViewBag.id = id;

            var ExistFile = Organization.Documents.Find(id);
            return View(ExistFile);
        }

        public PartialViewResult _PDFViewer(Guid id)
        {
            ViewBag.id = id;
            var ExistFile = Organization.Documents.Find(id);

            PdfDocumentProcessor documentProcessor = new PdfDocumentProcessor();
            MemoryStream stream = new MemoryStream(ExistFile.Content);
            documentProcessor.LoadDocument(stream);

            List<PdfPageModel> model = new List<PdfPageModel>();

            for (int pageNumber = 1; pageNumber <= documentProcessor.Document.Pages.Count; pageNumber++)
            {
                model.Add(new PdfPageModel(documentProcessor)
                {
                    PageNumber = pageNumber
                });
            }
            return PartialView("_PDFViewer", model);
        }

        public ActionResult Download(Guid id)
        {
            var ExistFile = Organization.Documents.Find(id);
            byte[] filedata = ExistFile.Content;
            return File(filedata, ExistFile.ContentType, ExistFile.FileName);
        }

        public ActionResult Delete(Guid id)
        {
            Organization.Documents.Delete(id);
            return RedirectToAction("Home", "Documents");
        }

        public ActionResult Extract(Guid id, int pageCount = 1)
        {
            ViewBag.id = id;
            var sourceDocument = Organization.Documents.Find(id);

            var remainDocument = Organization.Documents.ExtractPage(sourceDocument, pageCount);
            sourceDocument = Organization.Documents.RemovePages(sourceDocument, pageCount);



            return RedirectToAction("Home", "Document", new { Area = "Documents", id = remainDocument.Uid });
        }

        public ActionResult RemovePage(Guid id, int pageNo = 1)
        {
            ViewBag.id = id;
            var document = Organization.Documents.Find(id);
            this.Organization.Documents.RemovePage(document, pageNo);
            this.Organization.SaveChanges();

            return RedirectToAction("Home", "Document", new { Area = "Documents", id = document.Uid });
        }

    }
}
