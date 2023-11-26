
using DevExpress.Pdf;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Financial.Payments;
using ERPKeeper.WebFrontEnd.Controllers;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Customers.Controllers
{
    [RouteArea("Customers")]
    [RoutePrefix("ReceivePayment")]
    [Route("{id}/{action=Home}")]
    public class ReceivePaymentController : _DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.ReceivePayments.Find(id));

        public ActionResult Export(Guid id, string DocumentType = "ใบเสร็จรับเงิน")
        {
            var payment = Organization.ReceivePayments.Find(id);
            ViewBag.Report = this.GetExportReport(id, DocumentType);

            return View(payment);
        }
        private Node.Reports.Customers.ReceiptPayment GetExportReport(Guid id, string DocumentType)
        {
            var payments = Organization.ReceivePayments.FindList(id);
            payments.FirstOrDefault().CompanyProfile = Organization.SelfProfile;
            Organization.SaveChanges();

            var Report = new Node.Reports.Customers.ReceiptPayment()
            {
                DataSource = payments,
                Name = payments.FirstOrDefault().Name
            };

            Report.Parameters["DocumentType"].Value = DocumentType;
            Report.Parameters["CompanyName"].Value = Organization.DataItems.OrganizationHeader;

            Report.Parameters["DocumentType"].Visible = false;
            Report.Parameters["CompanyName"].Visible = false;

            Report.RequestParameters = false;
            return Report;
        }

        public ActionResult RemoveCommercial(Guid id, Guid commercialUid)
        {
            var payment = Organization.ReceivePayments.Find(id);
            if (payment.PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Transaction Lock");

            var commercial = Organization.Commercials.Find(commercialUid);
            payment.RemoveCommercail(commercial);
            Organization.SaveChanges();

            commercial.UpdatePaymentStatus();
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult AddCommercial(Guid id, Guid commercialUid)
        {
            var payment = Organization.ReceivePayments.Find(id);
            if (payment.PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Transaction Lock");

            var commercial = Organization.Commercials.Find(commercialUid);

            payment.AddCommercial(commercial);
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
 
        public ActionResult Delete(Guid id)
        {
            var result = Organization.ReceivePayments.Delete(id);
            return RedirectToAction("Home", "ReceivePayments");
        }
        public ActionResult UnPost(Guid id)
        {
            var payment = Organization.ReceivePayments.Find(id);
            Organization.ReceivePayments.UnPost(payment);
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Documents(Guid id) => View(Organization.ReceivePayments.Find(id));
        public ActionResult Ledger(Guid id) =>
        RedirectToAction("Home", "Ledgers", new { Area = "Accounting", id = id });
        public ActionResult Save(ReceivePayment updatedPayment)
        {
            Organization.ReceivePayments.Save(updatedPayment);
            return RedirectToAction("Home", new { Id = updatedPayment.Uid });
        }
        public ActionResult UpdateBalance(Guid id)
        {
            var exPayment = Organization.ReceivePayments.Find(id);
            exPayment.CalculateAmount();
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult DownloadWH(Guid id)
        {
            var payment = Organization.ReceivePayments.Find(id);
            var currentOrganization = Organization.SelfProfile;

            var formPath = String.Format("/Content/Form/WH_01.pdf");
            formPath = Server.MapPath(formPath);

            var destinationPath = "/Content/Form/" + payment.Uid.ToString("D") + ".pdf";
            destinationPath = Server.MapPath(destinationPath);

            using (PdfDocumentProcessor pdfDocumentProcessor = new PdfDocumentProcessor())
            {
                pdfDocumentProcessor.LoadDocument(formPath);

                PdfFormData data = pdfDocumentProcessor.GetFormData();
                data["id1"].Value = currentOrganization.TaxNumber;
                data["name1"].Value = currentOrganization.Name;
                data["add1"].Value = currentOrganization.PrimaryAddress.AddressLine;

                //data["id2"].Value = payment.Commercial.Profile.TaxNumber;
                //data["name2"].Value = payment.Commercial.Profile.Name;
                //data["add2"].Value = payment.Commercial.Profile.PrimaryAddress.AddressLine;

                data["date1"].Value = payment.TrnDate.ToString("dd-MMM-yy");
                //data["pay1"].Value = payment.Amount.ToString("N2");
                //data["tax1"].Value = payment.RetentionAmount.ToString("N2");


                //data["pay15"].Value = payment.Amount.ToString("N2");
                //data["tax15"].Value = payment.RetentionAmount.ToString("N2");


                pdfDocumentProcessor.ApplyFormData(data);
                pdfDocumentProcessor.SaveDocument(destinationPath);


                string contentType = MimeMapping.GetMimeMapping(destinationPath);
                return File(destinationPath, contentType, payment.No.ToString() + ".pdf");
            }
        }
    }
}