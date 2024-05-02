using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Customers.Estimate
{
    [Route("/API/{CompanyId}/Profiles/Customers/Estimates/{EstimateId:Guid}/{controller}/{action=Index}")]
    public class _EstimateBaseController : API_BaseController
    {

        public Guid EstimateId => Guid.Parse(RouteData.Values["EstimateId"].ToString());


    }
}
