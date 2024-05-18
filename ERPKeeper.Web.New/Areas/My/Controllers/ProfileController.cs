using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.My.Controllers
{
    public class ProfileController : _MyBaseController
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}