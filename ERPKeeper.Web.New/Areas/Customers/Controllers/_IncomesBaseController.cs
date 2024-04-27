using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Customers.Controllers
{
    [Area("Customers")]
    public class Base_CustomersController : BaseNodeController
    {
        public Base_CustomersController() : base()
        {

        }
    }
}

