using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ERPKeeperCore.Web.API.Accounting.JournalEntryType
{
    [Route("/API/{CompanyId}/Accounting/TransactionEntryTypes/{JournalEntryTypeId:Guid}/{controller}/{action=Index}")]
    public class JournalEntryTypeBaseController : API_BaseController
    {
        public Guid JournalEntryTypeId => Guid.Parse(HttpContext.GetRouteData().Values["JournalEntryTypeId"].ToString());

    }
}
