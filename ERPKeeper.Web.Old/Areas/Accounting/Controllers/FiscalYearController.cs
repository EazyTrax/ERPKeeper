using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting.FiscalYears;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Accounting.Controllers
{
    [RouteArea("Accounting")]
    [RoutePrefix("FiscalYear")]
    [Route("{id}/{action=Home}")]
    public class FiscalYearController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.FiscalYears.Find(id));
        public ActionResult Balance_Incomes(Guid id) => View(Organization.FiscalYears.Find(id));
        public ActionResult Balance_Opening(Guid id) => View(Organization.FiscalYears.Find(id));
        public ActionResult Balance_Expenses(Guid id) => View(Organization.FiscalYears.Find(id));
        public ActionResult Balance_Closing(Guid id, bool OpeningBalance = false)
        {
            var fiscalYear = Organization.FiscalYears.Find(id);
            ViewBag.OpeningBalance = OpeningBalance;
            return View(fiscalYear);
        }
        public ActionResult Report_Balance_Period(Guid id) => View(Organization.FiscalYears.Find(id));
        public ActionResult Report_BalanceSheet(Guid id) => View(Organization.FiscalYears.Find(id));
        public ActionResult Report_IncomeStatement(Guid id) => View(Organization.FiscalYears.Find(id));
        public ActionResult Report_TransactionsLedger(Guid id, ERPObjectType trType)
        {
            var fiscalYear = Organization.FiscalYears.Find(id);
            var ledgers = Organization.FiscalYears.GetTransactionLedgers(fiscalYear, trType);

            var Report = new Node.Reports.Accounting.TransactionLedgers()
            {
                DataSource = ledgers,
                Name = "AccountGroupLedgers",
            };

            Report.Parameters["TransactionType"].Value = trType.ToString() + " Period: " + fiscalYear.StartDate.ToString("dd-MMM-yy") + " - " + fiscalYear.EndDate.ToString("dd-MMM-yy");
            ViewBag.Report = Report;
            return View(fiscalYear);
        }


        public ActionResult Export_TransactionsLedger(Guid id)
        {
            var fiscalYear = Organization.FiscalYears.Find(id);

            var objTypes = Organization.ErpNodeDBContext.TransactionLedgers
                .GroupBy(tr => tr.TransactionType)
                .Select(tr => tr.FirstOrDefault().TransactionType)
                .ToList();

            objTypes.ForEach(trType =>
            {
                var ledgers = Organization.FiscalYears.GetTransactionLedgers(fiscalYear, trType);
                var report = new Node.Reports.Accounting.TransactionLedgers()
                {
                    DataSource = ledgers,
                    Name = "AccountGroupLedgers",
                };
                report.Parameters["TransactionType"].Value = $"{trType} Period: {fiscalYear.StartDate:dd-MMM-yy} - {fiscalYear.EndDate:dd-MMM-yy}";
                report.ExportToPdf(@"c:\temp\Journal\" + $"{trType}.pdf");
            });

            return Content("Complete");
        }








        public ActionResult Report_AssetDeprecations(Guid id)
        {


            var fisCalDep = Organization.ErpNodeDBContext
                .DeprecateSchedules
                .Where(ds => ds.FiscalYearUid == id)
                .OrderBy(ds => ds.FixedAsset.Code)
                .ToList();

            var report = new Node.Reports.Assets.DeprecateSchedule()
            {
                DataSource = fisCalDep,
                Name = "AccountGroupLedgers",
            };
            ViewBag.Report = report;

            return View(Organization.FiscalYears.Find(id));
        }



        public ActionResult Report_COGS(Guid id) => View(Organization.FiscalYears.Find(id));
        public ActionResult Ledger(Guid id) => RedirectToAction("Home", "Ledgers", new { Area = "Accounting", id });
        public ActionResult Depreciation(Guid id) => View(Organization.FiscalYears.Find(id));
        public ActionResult Report_AccountsLedger(Guid id, Guid? accountUid)
        {
            var fiscalYear = Organization.FiscalYears.Find(id);
            List<PeriodAccountBalance> accounts = new List<PeriodAccountBalance>();

            if (accountUid == null)
                accounts = fiscalYear.PeriodAccountBalances
                 .OrderBy(a => a.Account.Type)
                 .ThenBy(a => a.Account.SubEnumType)
                 .ThenBy(a => a.Account.No)
                 .ToList();
            else
                accounts.Add(fiscalYear.PeriodAccountBalances.Where(a => a.AccountGuid == accountUid).FirstOrDefault());

            Node.Reports.Accounting.AccountLedgers report = null;

            accounts.ForEach(a =>
            {
                var childReport = Organization.LedgersDal.GetLedgerReport(fiscalYear, a.Account);

                if (childReport != null)
                {
                    if (report == null)
                    {
                        report = childReport;
                        report.CreateDocument();
                    }
                    else
                    {
                        childReport.CreateDocument();
                        report.Pages.AddRange(childReport.Pages);
                    }
                }

            });

            ViewBag.Report = report;
            return View("Report_AccountsLedger", fiscalYear);
        }
        public ActionResult Export_AccountsLedger(Guid id)
        {
            var fiscalYear = Organization.FiscalYears.Find(id);
            fiscalYear.PeriodAccountBalances.ToList().ForEach(a =>
            {
                var report = Organization.LedgersDal.GetLedgerReport(fiscalYear, a.Account);
                report.ExportToPdf(@"c:\temp\Ledger-Account\" + $"{a.Account.Code}-{a.Account.Name}.pdf");
            });

            return Content("Complete");
        }

        public ActionResult StockValue(Guid id) => View(Organization.FiscalYears.Find(id));

        public ActionResult Save(FiscalYear fiscalYear)
        {
            Organization.FiscalYears.Save(fiscalYear);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult Close(Guid id)
        {
            var fiscalYear = Organization.FiscalYears.Find(id);
            Organization.FiscalYears.Close(fiscalYear);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult ReOpen(Guid id)
        {
            var fiscalYear = Organization.FiscalYears.Find(id);
            Organization.FiscalYears.UnPost(fiscalYear);
            Organization.FiscalYears.UpdateBalance(fiscalYear);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult UpdateBalance(Guid id)
        {
            var fy = Organization.FiscalYears.Find(id);
            Organization.FiscalYears.UpdateBalance(fy);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult AssignPrevious(Guid id)
        {
            var fiscalYear = Organization.FiscalYears.Find(id);
            Organization.FiscalYears.UpdatePreviousFiscalYear(fiscalYear);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public PartialViewResult PartialGridView_Fiscal_Current_Balance(Guid id)
        {
            ViewBag.id = id;
            var fiscalYear = Organization.FiscalYears.Find(id);
            return PartialView(fiscalYear);
        }
        public PartialViewResult PartialGridView_Fiscal_Opening_Balance(Guid id)
        {
            ViewBag.id = id;
            var fiscalYear = Organization.FiscalYears.Find(id);
            return PartialView(fiscalYear);
        }
        public PartialViewResult PartialGridView_Fiscal_Closing_Balance(Guid id)
        {
            ViewBag.id = id;
            var fiscalYear = Organization.FiscalYears.Find(id);
            return PartialView(fiscalYear);
        }

        public PartialViewResult PartialGridView_Expenses(Guid id)
        {
            ViewBag.id = id;
            var fiscalYear = Organization.FiscalYears.Find(id);
            return PartialView(fiscalYear);
        }


        public PartialViewResult PartialGridView_Incomes(Guid id)
        {
            ViewBag.id = id;
            var fiscalYear = Organization.FiscalYears.Find(id);
            return PartialView(fiscalYear);
        }



        public PartialViewResult PartialGridView_Balance(Guid id, bool? IsLiquidity, AccountTypes accountType, bool summary = true)
        {
            if (summary)
                ViewBag.Summary = true;

            var fiscalYear = Organization.FiscalYears.Find(id);

            var accountBalances = fiscalYear.PeriodAccountBalances
              .Where(pab => pab.Account.Type == accountType)
              .ToList();

            if (IsLiquidity != null)
                accountBalances = accountBalances.Where(a => a.Account.IsLiquidity == IsLiquidity).ToList();

            return PartialView(accountBalances);
        }

        public PartialViewResult _Current_Balance(Guid id, bool? IsLiquidity, AccountTypes accountType, bool summary = true)
        {
            if (summary)
                ViewBag.Summary = true;

            var fiscalYear = Organization.FiscalYears.Find(id);

            var accountBalances = fiscalYear.PeriodAccountBalances
              .Where(pab => pab.Account.Type == accountType)
              .ToList();

            if (IsLiquidity != null)
                accountBalances = accountBalances.Where(a => a.Account.IsLiquidity == IsLiquidity).ToList();

            return PartialView(accountBalances);
        }

        public PartialViewResult PartialGridView_COGS(Guid id)
        {
            ViewBag.Id = id;
            var fiscal = Organization.FiscalYears.Find(id);
            var periodItemCOGS = Organization.ItemsCOGS.GetList(fiscal);
            return PartialView(periodItemCOGS);

        }
    }
}