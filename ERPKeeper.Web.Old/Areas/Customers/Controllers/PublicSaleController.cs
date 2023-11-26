using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Customers.Controllers
{
    [Authorize.ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel.None)]
    [AllowAnonymous]
    public class PublicSaleController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        public ActionResult Home(Guid id) => View(Organization.Sales.Find(id));
        public ActionResult Payment(Guid id) => View(Organization.Sales.Find(id));
        public ActionResult Shipment(Guid id) => View(Organization.Sales.Find(id));

        private Node.Reports.Customers.Sale GetExportReport(Guid id, string DocumentType)
        {
            var transactions = Organization.Sales.FindList(id);

            var Report = new Node.Reports.Customers.Sale()
            {
                DataSource = transactions,
                Name = id.ToString("D")
            };


            Report.Parameters["DocumentType"].Value = DocumentType;
            Report.Parameters["CompanyName"].Value = Organization.DataItems.OrganizationName;

            Report.Parameters["DocumentType"].Visible = false;
            Report.Parameters["CompanyName"].Visible = false;

            return Report;
        }


        public ActionResult ExportSale(Guid id, string DocumentType)
        {
            var existTransaction = Organization.Sales.Find(id);
            existTransaction.UpdateAddress();
            existTransaction.ReCalculate();

            Organization.SaveChanges();


            Node.Reports.Customers.Sale Report = this.GetExportReport(id, DocumentType);
            ViewBag.Report = Report;
            return View(existTransaction);
        }


        public ActionResult ExportReceipt(Guid id, string DocumentType)
        {

            var transactions = Organization.Sales.GetQueryable()
                .Where(tr => tr.Uid == id)
                .Take(1)
                .ToList();


            var Report = new Node.Reports.Customers.Receipt()
            {
                DataSource = transactions,
                Name = id.ToString("D")
            };

            Report.Parameters["DocumentType"].Value = DocumentType;
            Report.Parameters["CompanyName"].Value = Organization.DataItems.OrganizationName;
            Report.Parameters["DocumentType"].Visible = false;
            Report.Parameters["CompanyName"].Visible = false;

            ViewBag.Report = Report;

            return View(transactions.FirstOrDefault());
        }
    }
}