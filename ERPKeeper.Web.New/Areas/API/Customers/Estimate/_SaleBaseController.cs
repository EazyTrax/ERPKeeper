using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.API.Customers.Estimates.Estimate
{
    [Route("/API/{CompanyId}/Customers/Estimates/{EstimateId:Guid}/{controller}/{action=Index}")]
    public class _EstimateBaseController : API_BaseController
    {
 
        public Guid EstimateId => Guid.Parse(RouteData.Values["EstimateId"].ToString());


    }
}
