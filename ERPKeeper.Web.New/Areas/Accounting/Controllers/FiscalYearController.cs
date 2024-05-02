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
            var model = EnterpriseRepo.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

        public IActionResult TrialBalance()
        {
            var model = EnterpriseRepo.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

        public IActionResult Incomes()
        {
            var model = EnterpriseRepo.FiscalYears.Find(FiscalYearId);
            return View(model);
        }
        public IActionResult Expenses()
        {
            var model = EnterpriseRepo.FiscalYears.Find(FiscalYearId);
            return View(model);
        }
        public IActionResult Journals()
        {
            var model = EnterpriseRepo.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

        public IActionResult Balances()
        {
            var model = EnterpriseRepo.FiscalYears.Find(FiscalYearId);
            return View(model);
        }

        public IActionResult UpdateBalance()
        {
            var model = EnterpriseRepo.FiscalYears.Find(FiscalYearId);
            EnterpriseRepo.FiscalYears.UpdateBalance(model);
            EnterpriseRepo.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult CreateNextFiscalYear()
        {
            var model = EnterpriseRepo.FiscalYears.Find(FiscalYearId);
            EnterpriseRepo.FiscalYears.Find(model.EndDate.AddDays(1));
            EnterpriseRepo.SaveChanges();
            return Redirect($"/{CompanyId}/Accounting/FiscalYears/{FiscalYearId}");
        }
    }
}
