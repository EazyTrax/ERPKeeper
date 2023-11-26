using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ERPKeeper.Web.New.API.Accounting.Account
{
    [Route("/API/{CompanyId}/Accounting/Accounts/{AccountId:Guid}/{controller}/{action=Index}")]
    public class AccountBaseController : APIBaseController
    {
        public Guid AccountId => Guid.Parse(HttpContext.GetRouteData().Values["AccountId"].ToString());

    }
}
