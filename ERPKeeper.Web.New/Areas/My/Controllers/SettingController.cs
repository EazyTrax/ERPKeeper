using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;


namespace ERPKeeperCore.Web.Areas.My.Controllers
{

    [Route("/My/{Controller=Home}/")]
    public class SettingController : _MyBaseController
    {
        [Route("")]
        public IActionResult Index() => View();

        [Route("Cover")]
        public IActionResult Cover() => View();

      

        [Route("EditMode/{mode}")]
        public IActionResult EditMode(string mode)
        {
            Response.Cookies.Delete("EditMode");

            if (mode == "View")
                Response.Cookies.Append("EditMode", "View", new CookieOptions { MaxAge = TimeSpan.FromDays(1) });
            if (mode == "Edit")
                Response.Cookies.Append("EditMode", "Edit", new CookieOptions { MaxAge = TimeSpan.FromDays(1) });

            return Redirect(Request.Headers["Referer"].ToString());
        }


        [Route("Language")]
        public IActionResult Language()
        {
            return View();
        }




       



      
    }
}