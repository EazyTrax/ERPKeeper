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
    public class RetentionGroupsController : Financial_BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AutoCreate()
        {
            Organization.ReceivePayments.UpdateRetentionGroups();
            Organization.SupplierPayments.UpdateRetentionGroups();


            Organization.RetentionGroups.UpdateCountAndBalance();

            return Redirect(Request.Headers["Referer"].ToString());
        }


    }
}
