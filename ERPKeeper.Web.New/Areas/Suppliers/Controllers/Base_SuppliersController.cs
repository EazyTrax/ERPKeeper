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
    public class Base_SuppliersController : BaseNodeController
    {
      

        public Base_SuppliersController() : base()
        {

        }
    }
}

