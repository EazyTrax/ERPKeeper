
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Customers.Controllers
{
    [RouteArea("Customers")]
    [RoutePrefix("Sales")]
    [Route("{action}/{id?}")]
    public class SalesController : WebFrontEnd.Controllers._DefaultNodeController
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

        public PartialViewResult PartialGridView(CommercialViewStatus id = CommercialViewStatus.Open)
        {
            ViewBag.id = id;
            var Transactions = Organization.Sales.GetByViewStatus(id);
            return PartialView("PartialGridView", Transactions);
        }
        public PartialViewResult _Chart()
        {
            DateTime StartDate = DateTime.Now.AddDays(-365);

            var SalesTables = Organization.Sales.GetQueryable()
                            .Where(t => t.TrnDate > StartDate)
                            .GroupBy(t => new { t.TrnDate })
                            .Select(go => new
                            {
                                IncomeAmount = go.Sum(i => i.Total),
                                TrnDate = go.Key.TrnDate
                            })
                            .ToList();

            return PartialView("_Chart", SalesTables);
        }

        public ActionResult Export(string DocumentType = "ใบแจ้งหนี้/ใบกำกับภาษี")
        {
            var lastYearTransaction = Organization.Sales.ListAll();

            lastYearTransaction.Where(s => s.ProfileAddressGuid == null).ToList().ForEach(s =>
            {
                s.ProfileAddress = s.Profile.Addresses.FirstOrDefault();
            });

            Organization.SaveChanges();

            var Report = new Node.Reports.Customers.Sale()
            {
                DataSource = lastYearTransaction.OrderBy(tr => tr.TrnDate).ToList(),
                Name = "LastMonthSale"
            };

            Report.Parameters["DocumentType"].Value = DocumentType;
            Report.Parameters["CompanyName"].Value = Organization.DataItems.OrganizationName;
            Report.Parameters["DocumentType"].Visible = false;
            Report.Parameters["CompanyName"].Visible = false;

            return View("Export", Report);
        }
        public ActionResult ExportLastYearOpening(string DocumentType = "ใบส่งของ/ใบกำกับภาษี")
        {
            var lastYearTransactions = Organization.Sales.GetLastYearOpening().ToList();

            lastYearTransactions
                .Where(s => s.ProfileAddressGuid == null)
                .ToList().ForEach(s => s.ProfileAddress = s.Profile.Addresses.FirstOrDefault());

            Organization.SaveChanges();

            var Report = new Node.Reports.Customers.Sale()
            {
                DataSource = lastYearTransactions
                .OrderBy(tr => tr.TrnDate)
                .ToList(),
                Name = "LastMonthSale"
            };

            return View("Export", Report);
        }

        public ActionResult ExportLastYear(string DocumentType = "ใบส่งของ/ใบกำกับภาษี")
        {
            var lastYearTransactions = Organization.Sales.GetLastYear().ToList();

            lastYearTransactions
                .Where(s => s.ProfileAddressGuid == null)
                .ToList().ForEach(s => s.ProfileAddress = s.Profile.Addresses.FirstOrDefault());

            Organization.SaveChanges();

            var Report = new Node.Reports.Customers.Sale()
            {
                DataSource = lastYearTransactions
                .OrderBy(tr => tr.TrnDate)
                .ToList(),
                Name = "LastMonthSale"
            };

            return View("Export", Report);
        }



        public ActionResult PostLedger()
        {
            Organization.Sales.Post();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult UnPost()
        {
            Organization.Sales.UnPost();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult UpdatePayments()
        {
            Organization.Sales.UpdatePayments();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult UpdateDailyBalance()
        {
            Organization.Sales.UpdateDailyBalance();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

    }
}