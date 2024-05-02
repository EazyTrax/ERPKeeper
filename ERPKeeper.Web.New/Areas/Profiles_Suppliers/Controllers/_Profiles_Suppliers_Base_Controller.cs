using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Profiles_Suppliers.Controllers
{
    [Area("Profiles_Suppliers")]
    [Route("/{CompanyId}/Profiles/Suppliers/{controller=Home}/{action=Index}/{id?}")]
    public class _Profiles_Suppliers_Base_Controller : BaseNodeController
    {
      

        public _Profiles_Suppliers_Base_Controller() : base()
        {

        }
    }
}

