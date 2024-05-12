using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ERPKeeperCore.Web.Areas.Accounting.Controllers
{

    public class TransactionsController : AccountingBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Post()
        {
            OrganizationCore.Transactions.PostLedgers();
            OrganizationCore.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}