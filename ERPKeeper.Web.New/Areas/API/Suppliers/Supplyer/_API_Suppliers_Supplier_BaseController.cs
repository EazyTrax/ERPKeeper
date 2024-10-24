﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Web.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Suppliers.Supplyer
{
    [Route("/API/{CompanyId}/Suppliers/Suppliers/{ProfileId:Guid}/{controller}/{action=Index}")]
    public class _API_Suppliers_Supplier_BaseController : API_BaseController
    {
        public Guid ProfileId => Guid.Parse(RouteData.Values["ProfileId"].ToString());


    }
}
