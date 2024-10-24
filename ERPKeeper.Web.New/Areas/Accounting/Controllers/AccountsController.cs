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
            Organization.ChartOfAccount.Refresh_CurrentBalance();
            return Redirect(Request.Headers["Referer"].ToString());
        }

      
        public IActionResult UpdateHistoryBalance()
        {
            Organization.ChartOfAccount.Refresh_CurrentBalance();
            Organization.ChartOfAccount.GenerateHistoryBalance();
            return Redirect(Request.Headers["Referer"].ToString());
        }


  
        public IActionResult UpdateNumber()
        {
            Organization.ChartOfAccount.ReAssignNumber();
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
