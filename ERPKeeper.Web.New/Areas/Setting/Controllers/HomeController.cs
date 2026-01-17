using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;


namespace ERPKeeperCore.Web.Areas.Setting.Controllers
{

    [Route("/Setting/")]
    public class HomeController : _SettingBaseController
    {
        [Route("")]
        public IActionResult Index() => View();

        [Route("Cover")]
        public IActionResult Cover() => View();

        [Route("Language")]
        public IActionResult Language()
        {
            return View();
        }
    }
}