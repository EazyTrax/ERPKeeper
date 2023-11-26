using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Accounting.Controllers
{

    public class AccountsController : AccountingBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/{CompanyId}/{area=Dashboard}/{controller=Home}/Update/CurrentBalance")]
        public IActionResult UpdateCurrentBalance()
        {
            Organization.ChartOfAccount.UpdateBalance();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Route("/{CompanyId}/{area=Dashboard}/{controller=Home}/Update/HistoryBalance")]
        public IActionResult UpdateHistoryBalance()
        {
            Organization.ChartOfAccount.UpdateBalance();
            Organization.ChartOfAccount.GenerateHistoryBalance();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Route("/{CompanyId}/{area=Dashboard}/{controller=Home}/Update/Number")]
        public IActionResult UpdateNumber()
        {
            Organization.ChartOfAccount.ReAssignNumber();
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
