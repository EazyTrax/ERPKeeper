using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERPKeeperCore.Web.Controllers;

namespace ERPKeeperCore.Web.Areas.Products.Controllers
{
    [Authorize]
    [Area("Products")]
    public class Base_ProductsController : _BaseController
    {
        public Base_ProductsController()
        {

        }
    }
}

