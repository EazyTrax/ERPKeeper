using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Accounting.Controllers
{
    public class JournalEntriesController : AccountingBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Refresh()
        {
            OrganizationCore.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
