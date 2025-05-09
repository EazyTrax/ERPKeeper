using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Accounting.Controllers
{
    [Route("/{CompanyId}/Accounting/FiscalYears/{FiscalYearId:Guid}/{action=Index}")]
    public class FiscalYearController : AccountingBaseController
    {
        public Guid FiscalYearId => Guid.Parse(HttpContext.GetRouteData().Values["FiscalYearId"].ToString());


        public IActionResult Index()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

        public IActionResult TrialBalance()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

        public IActionResult Incomes()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);
            return View(model);
        }
        public IActionResult Expenses()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);
            return View(model);
        }
        public IActionResult JournalEntries()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

        public IActionResult Balances()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

        public IActionResult UpdateBalance()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);

            Organization.FiscalYears.UnPostLedger(model);
            Organization.FiscalYears.Update_AccountsBalance(model);
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult Post()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);

            Organization.FiscalYears.UnPostLedger(model);
            Organization.FiscalYears.PostLedger(model);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult UnPost()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);
            Organization.FiscalYears.Update_AccountsBalance(model);
            Organization.FiscalYears.UnPostLedger(model);

            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }




        public IActionResult CreateNextFiscalYear()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);
            Organization.FiscalYears.Find(model.EndDate.AddDays(1));
            Organization.SaveChanges();
            return Redirect($"/{CompanyId}/Accounting/FiscalYears/{FiscalYearId}");
        }

        public IActionResult Report_Changes_in_Equity()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);
            return View(model);
        }
        public IActionResult Report_BalanceSheet()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);
            return View(model);
        }
        public IActionResult Report_ProfitandLoss_Statement()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

        public IActionResult Report_SupportingDocuments()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);
            return View(model);
        }
        public IActionResult Report_Tax_Calculation()
        {
            var model = Organization.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

    }
}