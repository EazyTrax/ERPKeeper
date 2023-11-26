using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Suppliers.Controllers
{
    [RouteArea("Suppliers")]
    [RoutePrefix("Purchases")]
    [Route("{action}/{id?}")]
    public class PurchasesController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(CommercialViewStatus id = CommercialViewStatus.Open)
        {
            ViewBag.id = id;
            return View("Home");
        }
        public ActionResult Open(CommercialViewStatus id = CommercialViewStatus.Open)
        {
            ViewBag.id = id;
            return View("Home");
        }

        public ActionResult Paid(CommercialViewStatus id = CommercialViewStatus.Paid)
        {
            ViewBag.id = id;
            return View("Home");
        }
        public ActionResult OverDue(CommercialViewStatus id = CommercialViewStatus.OverDue)
        {
            ViewBag.id = id;
            return View("Home");
        }

        public ActionResult All(CommercialViewStatus id = CommercialViewStatus.All)
        {
            ViewBag.id = id;
            return View("Home");
        }

        public ActionResult LastFiscal(CommercialViewStatus id = CommercialViewStatus.LastFiscal)
        {
            ViewBag.id = id;
            return View("Home");
        }
        public ActionResult Chart() => View();

        public ActionResult Export(string id, string DocumentType = "Purchase")
        {
            id = id?.ToLower();

            List<Commercial> purchases;
            if (id == "LastYearOpening".ToLower())
                purchases = Organization.Purchases.GetLastYearOpening();
            else if (id == "LastMonth".ToLower())
                purchases = Organization.Purchases.LastMonth;
            if (id == "LastYear".ToLower())
                purchases = Organization.Purchases.GetLastYear();
            else
                purchases = Organization.Purchases.ThisYear;

            var Report = new Node.Reports.Suppliers.Purchase()
            {
                DataSource = purchases,
                Name = "LastMonth"
            };

            Report.Parameters["DocumentType"].Value = DocumentType.ToString().ToUpper();
            Report.Parameters["CompanyName"].Value = Organization.DataItems.OrganizationName;
            Report.Parameters["DocumentType"].Visible = false;
            Report.Parameters["CompanyName"].Visible = false;

            return View("Export", Report);
        }
        public ActionResult ExportSummary(string DocumentType = "PurchaseSummary")
        {


            var MonthlyPurchase = Organization.ErpNodeDBContext
                .Commercials
                .Where(c => c.TransactionType == Node.Models.Accounting.Enums.ERPObjectType.Purchase || c.TransactionType == Node.Models.Accounting.Enums.ERPObjectType.SalesReturn)
                .GroupBy(g => new
                {
                    year = g.TrnDate.Year,
                    month = g.TrnDate.Month,
                })
                .Select(m => new { year = m.Key.year, month = m.Key.month, commercial = m.ToList() })
                .OrderByDescending(t => t.year)
                .ThenByDescending(t => t.month)
                .ToList();

            Node.Reports.Suppliers.MonthlyPurchase report = null;

            foreach (var monthly in MonthlyPurchase)
            {
                var tempReport = new Node.Reports.Suppliers.MonthlyPurchase()
                {
                    DataSource = monthly.commercial,
                    Name = $"{monthly.year}-{monthly.month}"
                };

                tempReport.Parameters["DocumentName"].Value = $"PURCHASE {monthly.year}-{monthly.month}";
                tempReport.Parameters["CompanyName"].Value = Organization.DataItems.OrganizationName;
                tempReport.Parameters["DocumentName"].Visible = false;
                tempReport.Parameters["CompanyName"].Visible = false;

                tempReport.CreateDocument();

                if (report == null)
                    report = tempReport;
                else
                    report.Pages.AddRange(tempReport.Pages);
            }

            return View("Export", report);
        }







        public PartialViewResult PartialGridView(CommercialViewStatus id = CommercialViewStatus.Open)
        {
            ViewBag.id = id;
            var Transactions = Organization.Purchases.GetByViewStatus(id);
            return PartialView("PartialGridView", Transactions);
        }
        public PartialViewResult _Chart()
        {
            DateTime StartDate = DateTime.Today.AddDays(-365);

            var PurchasesTables = Organization.Purchases.GetQueryable()
                .Where(t => t.TrnDate > StartDate)
                          .GroupBy(t => t.TrnDate)
                          .Select(go => new { ExpenseAmount = go.Sum(i => i.Total), Date = go.FirstOrDefault().TrnDate })
                          .ToList();

            return PartialView("_Chart", PurchasesTables);
        }
        public ActionResult ReOrder()
        {
            Organization.Purchases.ReOrder();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult UpdatePayments()
        {
            Organization.Purchases.UpdatePayments();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult UpdateProfilesCache()
        {
            Organization.Purchases.UpdateProfilesCache();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult PostLedger()
        {
            Organization.Purchases.PostLedger();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult UnPost()
        {
            Organization.Purchases.UnPost();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult UpdateDailyBalance()
        {
            Organization.Sales.UpdateDailyBalance();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}