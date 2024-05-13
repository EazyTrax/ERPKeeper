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
            var model = OrganizationCore.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

        public IActionResult TrialBalance()
        {
            var model = OrganizationCore.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

        public IActionResult Incomes()
        {
            var model = OrganizationCore.FiscalYears.Find(FiscalYearId);
            return View(model);
        }
        public IActionResult Expenses()
        {
            var model = OrganizationCore.FiscalYears.Find(FiscalYearId);
            return View(model);
        }
        public IActionResult JournalEntries()
        {
            var model = OrganizationCore.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

        public IActionResult Balances()
        {
            var model = OrganizationCore.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

        public IActionResult UpdateBalance()
        {
            var model = OrganizationCore.FiscalYears.Find(FiscalYearId);
            OrganizationCore.FiscalYears.UpdateAccountBalance(model);
            OrganizationCore.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult CreateNextFiscalYear()
        {
            var model = OrganizationCore.FiscalYears.Find(FiscalYearId);
            OrganizationCore.FiscalYears.Find(model.EndDate.AddDays(1));
            OrganizationCore.SaveChanges();
            return Redirect($"/{CompanyId}/Accounting/FiscalYears/{FiscalYearId}");
        }

         public IActionResult Report_Changes_in_Equity()
        {
            var model = OrganizationCore.FiscalYears.Find(FiscalYearId);
            return View(model);
        }
        public IActionResult Report_BalanceSheet()
        {
            var model = OrganizationCore.FiscalYears.Find(FiscalYearId);
            return View(model);
        }
        public IActionResult Report_ProfitandLoss_Statement()
        {
            var model = OrganizationCore.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

        public IActionResult Report_SupportingDocuments()
        {
            var model = OrganizationCore.FiscalYears.Find(FiscalYearId);
            return View(model);
        }


        
    }
}