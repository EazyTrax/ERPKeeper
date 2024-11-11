
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Enterprise_Setting.Controllers
{

    public class MemoTemplatesController : _Enterprise_Setting_BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
