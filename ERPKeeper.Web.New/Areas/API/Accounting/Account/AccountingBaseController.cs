using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ERPKeeperCore.Web.API.Accounting.Account
{
    [Route("/API/{CompanyId}/Accounting/Accounts/{AccountId:Guid}/{controller}/{action=Index}")]
    public class AccountBaseController : API_BaseController
    {
        public Guid AccountId => Guid.Parse(HttpContext.GetRouteData().Values["AccountId"].ToString());

    }
}
