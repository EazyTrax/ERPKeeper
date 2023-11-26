using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Reports.CompanyandFinancial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Accounting.Controllers
{
    public class ReportsController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home()
        {
            return View();
        }
        public ActionResult OpeningBalance() => View();
        public ActionResult IncomeStatement()
        {
            var Incomes = Organization.ChartOfAccount.IncomeAccounts;
            var Expenses = Organization.ChartOfAccount.ExpenseAccounts;

            decimal profit = (Incomes.Sum(a => a.CurrentBalance) - (Expenses.Sum(a => a.CurrentBalance)));

            var IncomeStatementData = new ERPKeeper.Node.Models.Accounting.Reports.IncomeStatement()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1).AddDays(-1),
                Profit = profit,
                Incomes = Incomes,
                Expenses = Expenses,
            };

            var IncomeStatements = new HashSet<ERPKeeper.Node.Models.Accounting.Reports.IncomeStatement>();

            IncomeStatements.Add(IncomeStatementData);


            var Report = new ERPKeeper.Node.Reports.Accounting.IncomeStatement()
            {
                DataSource = IncomeStatements.ToList(),
                Name = "IncomeStatement",
            };

            Report.Parameters["ProfileFullName"].Value = Organization.DataItems.OrganizationName;

            ViewBag.Report = Report;
            return View();
        }
        public ActionResult BalanceSheet()
        {
            var assetAccounts = Organization.ChartOfAccount.AssetAccounts.Where(a => a.CurrentBalance != 0).ToList();
            var liabilityAccount = Organization.ChartOfAccount.LiabilityAccounts.Where(a => a.CurrentBalance != 0).ToList();
            var equityAccounts = Organization.ChartOfAccount.EquityAccounts.Where(a => a.CurrentBalance != 0).ToList();


            var balanceSheet = new ERPKeeper.Node.Models.Accounting.Reports.BalanceSheet()
            {
                TrnDate = DateTime.Now,
                EquityAccounts = equityAccounts,
                LiabilityAccounts = liabilityAccount,
                AssetAccounts = assetAccounts,
            };


            var balanceSheets = new HashSet<ERPKeeper.Node.Models.Accounting.Reports.BalanceSheet>();

            balanceSheets.Add(balanceSheet);


            var Report = new ERPKeeper.Node.Reports.Accounting.BalanceSheet()
            {
                DataSource = balanceSheets.ToList(),
                Name = "IncomeStatement"
            };
            Report.Parameters["ProfileFullName"].Value = Organization.DataItems.OrganizationName;

            ViewBag.Report = Report;
            return View();
        }
        public ActionResult TrialBalance(enumTrialBalaceViewType type = enumTrialBalaceViewType.Default, Guid? fiscalYearUid = null)
        {
            ViewBag.Type = type;
            ViewBag.FiscalYearUid = fiscalYearUid;

            return View();
        }



        public PartialViewResult PartialGridViewTrialBalance(enumTrialBalaceViewType type, Guid? fiscalYearUid)
        {
            ViewBag.Type = type;
            var Ledgers = Organization.LedgersDal.QueryBy(type, fiscalYearUid);
            var result = Ledgers
              .GroupBy(o => o.AccountUid)
              .Select(go => new TrialBalanceAccountItem
              {
                  Uid = go.Key,
                  accountItem = go.FirstOrDefault().accountItem,
                  Credit = go.Sum(i => i.Credit) ?? 0,
                  Debit = go.Sum(i => i.Debit) ?? 0,
                  Type = go.FirstOrDefault().accountItem.Type,
                  SubType = go.FirstOrDefault().accountItem.SubEnumType,
              })
              .ToList();

            return PartialView(result);
        }
        public PartialViewResult PartialGridViewOpeningBalance()
        {
            var result = Organization.ChartOfAccount.OpeningItemList();
            return PartialView(result);
        }
        public PartialViewResult _Chart_ProfitAndLostSummary() => PartialView();
        public PartialViewResult _Chart_SummaryProfitAndLost() => PartialView();
        public PartialViewResult _Chart_SummaryCurrentAssetandLiability() => PartialView();
        public PartialViewResult _Chart_SummaryCurrentFinancial() => PartialView();
        public PartialViewResult _Chart_CurrentAssetAndLiability()
        {
            return PartialView();
        }
    }
}