
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.Logistic.Controllers
{

    public class LogisticProvidersController : _Logistic_BaseController
    {
        public IActionResult Index()
        {

            return View();
        }

    }
}
