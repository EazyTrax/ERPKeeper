using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{
    [Area("Suppliers")]
    [Route("/{CompanyId}/Suppliers/{controller=Home}/{action=Index}/{id?}")]
    public class _Suppliers_Base_Controller : BaseNodeController
    {
      

        public _Suppliers_Base_Controller() : base()
        {

        }
    }
}

