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


        public IActionResult UpdateCurrentBalance()
        {
            OrganizationCore.ChartOfAccount.RefreshCurrentBalance();
            return Redirect(Request.Headers["Referer"].ToString());
        }

      
        public IActionResult UpdateHistoryBalance()
        {
            OrganizationCore.ChartOfAccount.RefreshCurrentBalance();
            OrganizationCore.ChartOfAccount.GenerateHistoryBalance();
            return Redirect(Request.Headers["Referer"].ToString());
        }


  
        public IActionResult UpdateNumber()
        {
            OrganizationCore.ChartOfAccount.ReAssignNumber();
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
