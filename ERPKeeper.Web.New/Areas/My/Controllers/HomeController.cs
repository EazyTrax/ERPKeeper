using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

using Microsoft.AspNetCore.Http;


namespace ERPKeeperCore.Web.Areas.My.Controllers
{
    public class HomeController : _MyBaseController
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}