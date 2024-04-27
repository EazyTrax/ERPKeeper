using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Accounting.Controllers
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
            EnterpriseRepo.ChartOfAccount.UpdateBalance();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Route("/{CompanyId}/{area=Dashboard}/{controller=Home}/Update/HistoryBalance")]
        public IActionResult UpdateHistoryBalance()
        {
            EnterpriseRepo.ChartOfAccount.UpdateBalance();
            EnterpriseRepo.ChartOfAccount.GenerateHistoryBalance();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Route("/{CompanyId}/{area=Dashboard}/{controller=Home}/Update/Number")]
        public IActionResult UpdateNumber()
        {
            EnterpriseRepo.ChartOfAccount.ReAssignNumber();
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
