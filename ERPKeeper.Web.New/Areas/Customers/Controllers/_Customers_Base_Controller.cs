using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Routing;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{
    [Area("Customers")]
    [Route("/{CompanyId}/Customers/{controller=Home}/{action=Index}/{id?}")]
    public class _Customers_Base_Controller : DefaultController
    {
  
        public _Customers_Base_Controller() : base()
        {

        }
    }
}

