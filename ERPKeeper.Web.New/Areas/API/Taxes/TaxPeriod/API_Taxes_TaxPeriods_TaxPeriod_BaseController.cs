using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
 
namespace ERPKeeperCore.Web.API.Taxes.TaxPeriod
{
    [Route("/API/Taxes/TaxPeriods/{TaxPeriodId:Guid}/{controller}/{action=Index}")]
    public class API_Taxes_TaxPeriods_TaxPeriod_BaseController : API_BaseController
    {
        public Guid TaxPeriodId => Guid.Parse(HttpContext.GetRouteData().Values["TaxPeriodId"].ToString());

    }
}
