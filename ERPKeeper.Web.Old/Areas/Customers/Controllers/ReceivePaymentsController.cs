

using ERPKeeper.Node.Models.Financial.Payments;
using ERPKeeper.Node.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Customers.Controllers
{
    [RouteArea("Customers")]
    [RoutePrefix("ReceivePayments")]
    [Route("{action}")]
    public class ReceivePaymentsController : WebFrontEnd.Controllers._DefaultNodeController
    {


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

        }



        public ActionResult Home() => View();
        public ActionResult ReOrder()
        {
            Organization.ReceivePayments.Reorder();
            return Redirect(Request.UrlReferrer.PathAndQuery);

        }




        public PartialViewResult PartialGridView()
        {
            var payments = Organization.ReceivePayments.GetAll();
            return PartialView("PartialGridView", payments);
        }
        public ActionResult Export(string DocumentType = "ใบเสร็จรับเงิน", ViewPeriod period = ViewPeriod.All)
        {
            var payments = Organization.ReceivePayments.GetAll().OrderBy(t => t.TrnDate).ToList();
            ViewBag.Report = this.GetExportReport(payments, DocumentType, period);
            return View();
        }

        private dynamic GetExportReport(List<ReceivePayment> payments, string documentType, ViewPeriod period)
        {
            var Report = new Node.Reports.Customers.ReceiptPayment()
            {
                DataSource = payments,
                Name = Guid.NewGuid().ToString("D")
            };

            Report.Parameters["DocumentType"].Value = documentType;
            Report.Parameters["CompanyName"].Value = Organization.DataItems.OrganizationHeader;
            Report.Parameters["DocumentType"].Visible = false;
            Report.Parameters["CompanyName"].Visible = false;
            return Report;
        }
    }
}