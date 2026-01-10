using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ERPKeeperCore.Web.API.Accounting.JournalEntry
{
    [Route("/API/Accounting/Transactions/{JournalEntryId:Guid}/{controller}/{action=Index}")]
    public class JournalEntryBaseController : API_BaseController
    {
        public Guid JournalEntryId => Guid.Parse(HttpContext.GetRouteData().Values["JournalEntryId"].ToString());

    }
}
