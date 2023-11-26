using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Accounting.Controllers
{
    [Route("/{CompanyId}/{area}/JournalEntryTypes/{JournalEntryTypeId:Guid}/{action=Index}")]
    public class JournalEntryTypeController : AccountingBaseController
    {
        public Guid JournalEntryTypeId => Guid.Parse(HttpContext.GetRouteData().Values["JournalEntryTypeId"].ToString());


        public IActionResult Index()
        {
            var model = Organization.JournalEntryTypes.Find(JournalEntryTypeId);
            return View(model);
        }

        

    
    }
}
