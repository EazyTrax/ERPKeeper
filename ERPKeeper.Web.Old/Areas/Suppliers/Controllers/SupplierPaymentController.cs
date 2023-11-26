using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Financial.Payments;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Suppliers.Controllers
{
    [RouteArea("Suppliers")]
    [RoutePrefix("SupplierPayment")]
    [Route("{id}/{action=Home}")]
    public class SupplierPaymentController : WebFrontEnd.Controllers._DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.SupplierPayments.Find(id));
        public ActionResult Export(Guid id, string DocumentType = "ใบเสร็จรับเงิน")
        {
            var payment = Organization.SupplierPayments.Find(id);
            ViewBag.Report = this.GetExportReport(id, DocumentType);

            return View(payment);
        }
        private Node.Reports.Suppliers.SupplierPayment GetExportReport(Guid id, string DocumentType)
        {
            var payments = Organization.SupplierPayments.FindList(id);

            var Report = new Node.Reports.Suppliers.SupplierPayment()
            {
                DataSource = payments,
                Name = id.ToString("D")
            };

            Report.Parameters["DocumentType"].Value = DocumentType;
            Report.Parameters["CompanyName"].Value = Organization.DataItems.OrganizationHeader;

            Report.Parameters["DocumentType"].Visible = false;
            Report.Parameters["CompanyName"].Visible = false;

            Report.RequestParameters = false;
            return Report;
        }

        public ActionResult Delete(Guid id)
        {
            var result = Organization.SupplierPayments.Delete(id);
            return RedirectToAction("Home", "SupplierPayments");
        }

        public ActionResult New(Guid? id)
        {
            var newSupplierPayment = Organization.SupplierPayments.CreateNew(id);
            return RedirectToAction("Home", new { id = newSupplierPayment.Uid });
        }

        public ActionResult Save(SupplierPayment updatedPayment)
        {
            var payment = Organization.SupplierPayments.Save(updatedPayment);

            if (payment != null)
                return RedirectToAction("Home", new { Id = updatedPayment.Uid });
            else
                return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult RemoveCommercial(Guid id, Guid commercialUid)
        {
            var payment = Organization.SupplierPayments.Find(id);
            if (payment.PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Transaction Lock");

            var commercial = Organization.Commercials.Find(commercialUid);

            payment.RemoveCommercail(commercial);
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult AddCommercial(Guid id, Guid commercialUid)
        {
            var payment = Organization.SupplierPayments.Find(id);
            if (payment.PostStatus == LedgerPostStatus.Locked)
                throw new Exception("Transaction Lock");

            var commercial = Organization.Commercials.Find(commercialUid);

            payment.AddCommercial(commercial);
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }



       
        public ActionResult UnPost(Guid id)
        {
            var payment = Organization.SupplierPayments.Find(id);
            Organization.SupplierPayments.UnPost(payment);
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Documents(Guid id)
        {
            ViewBag.id = id;

            var payment = Organization.SupplierPayments.Find(id);
            return View(payment);
        }

        public ActionResult Ledger(Guid id) =>
            RedirectToAction("Home", "Ledgers", new { Area = "Accounting", id = id });



        public ActionResult UpdateBalance(Guid id)
        {
            var exPayment = Organization.SupplierPayments.Find(id);
            exPayment.CalculateAmount();
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}