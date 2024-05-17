using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ERPKeeperCore.Web.New.API.Financials.Lend
{
    [Route("/API/{CompanyId}/Financial/Lends/{Id:guid}/{controller}/{action=Index}")]
    public class API_Financials_Lend_BaseController : API_BaseController
    {
        public Guid LendId => Guid.Parse(HttpContext.GetRouteData().Values["Id"].ToString());


    }
}
