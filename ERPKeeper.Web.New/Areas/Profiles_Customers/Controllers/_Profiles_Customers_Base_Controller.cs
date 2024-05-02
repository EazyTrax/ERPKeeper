using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Profiles_Customers.Controllers
{
    [Area("Profiles_Customers")]
    [Route("/{CompanyId}/Profiles/Customers/{controller=Home}/{action=Index}/{id?}")]
    public class _Profiles_Customers_Base_Controller : BaseNodeController
    {
        public _Profiles_Customers_Base_Controller() : base()
        {

        }
    }
}

