using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;


namespace ERPKeeperCore.Web.Areas.Accountant.Controllers
{

    [Route("/Accountant/")]
    public class HomeController : _AccountantBaseController
    {
        public IActionResult Index() => View();

       
    }
}