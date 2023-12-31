﻿using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("/{area}/{controller=Home}/{action=Index}/{id?}")]
    public class HomeController : BaseNodeController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
