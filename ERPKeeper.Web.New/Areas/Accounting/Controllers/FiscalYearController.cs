using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Accounting.Controllers
{
    [Route("/{CompanyId}/{area}/FiscalYears/{FiscalYearId:Guid}/{action=Index}")]
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
        public IActionResult Journals()
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
            Organization.FiscalYears.UpdateBalance(model);
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
    }
}
