using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ERPKeeperCore.Enterprise;
using Microsoft.EntityFrameworkCore;

namespace ERPKeeperCore.Web.Areas.Accounting.Controllers
{
    [Route("/{CompanyId}/Accounting/Accounts/{AccountId:Guid}/{action=Index}/{id?}")]
    public class AccountController : AccountingBaseController
    {
        public Guid AccountId => Guid.Parse(HttpContext.GetRouteData().Values["AccountId"].ToString());


        public IActionResult Index()
        {
            var model = Organization.ChartOfAccount.Find(AccountId);



            return View(model);
        }
        public IActionResult Refresh()
        {
            var model = Organization.ChartOfAccount.Find(AccountId);
            model.Refresh();

            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Ledgers()
        {
            var model = Organization.ChartOfAccount.Find(AccountId);
            return View(model);
        }
        public IActionResult Balances()
        {
            var model = Organization.ChartOfAccount.Find(AccountId);
            return View(model);
        }
        public IActionResult UpdateCurrentBalance()
        {
            var model = Organization.ChartOfAccount.Find(AccountId);
            Organization.ChartOfAccount.Refresh_CurrentBalance();
            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult UpdateHistoryBalance()
        {
            var model = Organization.ChartOfAccount.Find(AccountId);
            model.CreateHostoriesBalances();
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Chart()
        {
            var model = Organization.ChartOfAccount.Find(AccountId);
            return View(model);
        }








        [HttpPost]
        public IActionResult Update(ERPKeeperCore.Enterprise.Models.Accounting.Account accountItem)
        {
            var model = Organization.ChartOfAccount.Find(AccountId);
            model.No = accountItem.No;
            model.Name = accountItem.Name;
            model.Type = accountItem.Type;
            model.SubType = accountItem.SubType;
            model.IsLiquidity = accountItem.IsLiquidity;
            model.IsCashEquivalent = accountItem.IsCashEquivalent;
            model.Description = accountItem.Description;

            Organization.SaveChanges();

            return View("Index", model);
        }


    }
}
