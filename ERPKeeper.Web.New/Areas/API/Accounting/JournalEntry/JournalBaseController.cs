using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ERPKeeper.Web.New.API.Accounting.JournalEntry
{
    [Route("/API/{CompanyId}/Accounting/JournalEntries/{JournalEntryId:Guid}/{controller}/{action=Index}")]
    public class JournalEntryBaseController : APIBaseController
    {
        public Guid JournalEntryId => Guid.Parse(HttpContext.GetRouteData().Values["JournalEntryId"].ToString());

    }
}
