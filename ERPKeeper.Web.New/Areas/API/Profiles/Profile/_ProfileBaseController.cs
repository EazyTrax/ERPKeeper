﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.API.Profiles.Profile
{
    [Route("/api/{CompanyId}/Profiles/{ProfileId:Guid}/{controller}/{action=Index}")]
    public class _ProfileBaseController : API_BaseController
    {
 
        public Guid ProfileId => Guid.Parse(RouteData.Values["ProfileId"].ToString());


    }
}
