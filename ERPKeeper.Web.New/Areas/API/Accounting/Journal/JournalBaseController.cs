using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ERPKeeper.Web.New.API.Accounting.Journal
{
    [Route("/API/{CompanyId}/Accounting/Journals/{JournalId:Guid}/{controller}/{action=Index}")]
    public class JournalBaseController : APIBaseController
    {
        public Guid JournalId => Guid.Parse(HttpContext.GetRouteData().Values["JournalId"].ToString());

    }
}
