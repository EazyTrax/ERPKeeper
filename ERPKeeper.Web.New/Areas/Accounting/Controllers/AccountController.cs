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

namespace ERPKeeperCore.Web.Areas.Accounting.Controllers
{
    [Route("/{CompanyId}/Accounting/Accounts/{AccountId:Guid}/{action=Index}/{id?}")]
    public class AccountController : AccountingBaseController
    {
        public Guid AccountId => Guid.Parse(HttpContext.GetRouteData().Values["AccountId"].ToString());


        public IActionResult Index()
        {
            var model = EnterpriseRepo.ChartOfAccount.Find(AccountId);
            return View(model);
        }
        public IActionResult Ledgers()
        {
            var model = EnterpriseRepo.ChartOfAccount.Find(AccountId);
            return View(model);
        }
        public IActionResult Balances()
        {
            var model = EnterpriseRepo.ChartOfAccount.Find(AccountId);
            return View(model);
        }
        public IActionResult Chart()
        {
            var model = EnterpriseRepo.ChartOfAccount.Find(AccountId);
            return View(model);
        }



        public IActionResult Update(ERPKeeperCore.Enterprise.Models.Accounting.Account accountItem)
        {
            var model = EnterpriseRepo.ChartOfAccount.Find(AccountId);
            model.No = accountItem.No;
            model.Name = accountItem.Name;
            model.IsLiquidity = accountItem.IsLiquidity;
            model.IsCashEquivalent = accountItem.IsCashEquivalent;

            EnterpriseRepo.SaveChanges();

            return View("Index", model);
        }

     
    }
}
