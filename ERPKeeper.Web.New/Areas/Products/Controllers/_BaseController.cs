using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERPKeeper.Web.New.Controllers;

namespace ERPKeeper.Web.New.Areas.Products.Controllers
{
    [Authorize]
    [Area("Products")]
    public class Base_ProductsController : BaseNodeController
    {
        public Base_ProductsController()
        {

        }
    }
}

