﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeperCore.Web.API.Suppliers.Supplyer
{
    [Route("/API/{CompanyId}/Suppliers/Suppliers/{ProfileId:Guid}/{controller}/{action=Index}")]
    public class Base_API_Suppliers_Supplier_Controller : API_BaseController
    {
        public Guid ProfileId => Guid.Parse(RouteData.Values["ProfileId"].ToString());


    }
}