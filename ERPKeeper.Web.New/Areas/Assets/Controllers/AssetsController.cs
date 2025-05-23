﻿using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Assets.Controllers
{

    public class AssetsController : Base_AssetsController
    {
        public IActionResult Index()
        {
            Organization.Assets.Refresh();
            return View();
        }

        public IActionResult Refresh()
        {
            Organization.Assets.Refresh();

            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
