using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeper.Web.New.API.Suppliers.Estimates.Estimate
{
    [Route("/API/{CompanyId}/Suppliers/Estimates/{EstimateId:Guid}/{controller}/{action=Index}")]
    public class _EstimateBaseController : APIBaseController
    {
 
        public Guid EstimateId => Guid.Parse(RouteData.Values["EstimateId"].ToString());


    }
}
