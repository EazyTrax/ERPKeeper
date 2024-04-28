﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ERPKeeperCore.Web.API.Accounting.Journal
{
    [Route("/API/{CompanyId}/Accounting/Transactions/{TransactionId:Guid}/{controller}/{action=Index}")]
    public class API_Accounting_Transaction_BaseController : API_BaseController
    {
        public Guid TransactionId => Guid.Parse(HttpContext.GetRouteData().Values["TransactionId"].ToString());

    }
}
