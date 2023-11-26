using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ERPKeeper.Node;

namespace ERPKeeper.Web.New.Areas.Accounting.Controllers
{
    [Route("/{CompanyId}/{area}/Accounts/{AccountId:Guid}/{action=Index}/{id?}")]
    public class AccountController : AccountingBaseController
    {
        public Guid AccountId => Guid.Parse(HttpContext.GetRouteData().Values["AccountId"].ToString());


        public IActionResult Index()
        {
            var model = Organization.ChartOfAccount.Find(AccountId);
            return View(model);
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
        public IActionResult Chart()
        {
            var model = Organization.ChartOfAccount.Find(AccountId);
            return View(model);
        }



        public IActionResult Update(ERPKeeper.Node.Models.Accounting.AccountItem accountItem)
        {
            var model = Organization.ChartOfAccount.Find(AccountId);
            model.No = accountItem.No;
            model.Name = accountItem.Name;
            model.IsLiquidity = accountItem.IsLiquidity;
            model.IsCashEquivalent = accountItem.IsCashEquivalent;
            model.IsFocus = accountItem.IsFocus;

            Organization.SaveChanges();

            return View("Index", model);
        }

     
    }
}
