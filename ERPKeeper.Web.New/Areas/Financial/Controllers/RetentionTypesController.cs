using ERPKeeperCore.Web.Areas.Financial.Controllers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Financial.Controllers
{
    public class RetentionTypesController : Financial_BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpdateBalance()
        {
            var retentionTypes = Organization.RetentionTypes
                .GetAll();

            var retentionTypeAccounts = retentionTypes
            .Select(a => a.RetentionToAccount)
            .ToList();

            Organization.ChartOfAccount.Update_CurrentBalance(retentionTypeAccounts);

            retentionTypes.ForEach(a =>
            {
                a.RetentionAccountBalance = a.RetentionToAccount.CurrentBalance;
            });

            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
