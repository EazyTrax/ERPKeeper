using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ERPKeeper.Web.New.API.Accounting.FiscalYear
{
    [Route("/API/{CompanyId}/Accounting/FiscalYears/{FiscalYearId:Guid}/{controller}/{action=Index}")]
    public class FiscalYearBaseController : APIBaseController
    {
        public Guid FiscalYearId => Guid.Parse(HttpContext.GetRouteData().Values["FiscalYearId"].ToString());

    }
}
