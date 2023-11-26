using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Accounting.Controllers
{
    public class JournalEntryTypesController : AccountingBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Refresh()
        {
            Organization.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
