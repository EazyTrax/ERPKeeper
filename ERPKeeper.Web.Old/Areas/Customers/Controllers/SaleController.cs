using ERPKeeper.Node.Models.Transactions;
using ERPKeeper.Node.Models.Transactions.Commercials;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Customers.Controllers
{
    [RouteArea("Customers")]
    [RoutePrefix("Sale")]
    [Route("{id}/{action=Home}")]
    public class SaleController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.Sales.Find(id));
        public ActionResult Payments(Guid id) => View(Organization.Sales.Find(id));
        public ActionResult Shipments(Guid id) => View(Organization.Sales.Find(id));
        public ActionResult Items(Guid id)
        {
            var sale = Organization.Sales.Find(id);
            Organization.AssistItems.Create(sale.ProfileGuid);
            return View(sale);
        }
        public ActionResult Documents(Guid id) => View(Organization.Sales.Find(id));
        public ActionResult Log(Guid id) => View(Organization.Sales.Find(id));
        public ActionResult JournalItems(Guid id) => View(Organization.Sales.Find(id));
        public PartialViewResult _PaymentSummary(Guid id) => PartialView(Organization.Sales.Find(id));
        public ActionResult Ledger(Guid id) => RedirectToAction("Home", "Ledgers", new { Area = "Accounting", id = id });
        public ActionResult CreatePayment(Guid id, DateTime? payDate = null)
        {
            var transaction = this.Organization.Sales.Find(id);
            var payment = Organization.ReceivePayments.Create(transaction.Profile, transaction, payDate);
            return RedirectToAction("Home", "ReceivePayment", new { id = payment.Uid });
        }

        [Route("{id}/Shipments/Create")]
        public ActionResult CreateShipment(Guid id, DateTime? payDate = null)
        {
            var transaction = this.Organization.Sales.Find(id);
            var shipment = transaction.CreatShipment("Method", "Tracking");
            Organization.SaveChanges();
            return Redirect($"/Customers/Sale/{id}/Shipments");
        }




        public ActionResult ExportSale(Guid id, string DocumentType)
        {
            var sale = Organization.Sales.Find(id);
            sale.UpdateAddress();
            sale.ReCalculate();
            sale.UpdateSerialNumber();
            Organization.SaveChanges();

            var report = Organization.Sales.GetExportReport(id, DocumentType);
            ViewBag.Report = report;
            return View(sale);
        }
        public ActionResult ExportShippingLabel(Guid id)
        {
            var sales = Organization.Sales.GetQueryable().Where(tr => tr.Uid == id).Take(1).ToList();

            var Report = new Node.Reports.Customers.ShippingLabel()
            {
                DataSource = sales,
                Name = id.ToString("D")
            };

            Report.Parameters["CompanyName"].Value = Organization.DataItems.OrganizationName;
            Report.Parameters["CompanyName"].Visible = false;

            ViewBag.Report = Report;
            return View(sales.FirstOrDefault());
        }
        public ActionResult Save(Sale sale)
        {
            Organization.Sales.UpdateChanges(sale);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult UnPost(Guid id)
        {
            var sale = Organization.Sales.Find(id);
            Organization.Sales.UnPost(sale);
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Void(Guid id)
        {
            var sale = Organization.Sales.Find(id);
            if (sale.CommercialItems.Count == 0)
                sale.Status = CommercialStatus.Void;
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Delete(Guid id)
        {
            var sale = Organization.Sales.Find(id);

            if (Organization.Sales.Delete(sale))
                return RedirectToAction("Home", "Sales", new { id = sale.Status });
            else
                return Redirect(Request.UrlReferrer.PathAndQuery);
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
        public RedirectResult Claim(Guid id)
        {
            var sale = Organization.Sales.Find(id);
         
            Organization.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}