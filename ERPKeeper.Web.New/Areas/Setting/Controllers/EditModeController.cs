using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ERPKeeperCore.Web.Areas.Setting.Controllers
{
    [Route("/Setting/EditMode")]
    public class EditModeController : _SettingBaseController
    {
        [Route("{mode}")]
        public IActionResult Index(string mode)
        {
            Response.Cookies.Delete("EditMode");

            if (mode == "View")
                Response.Cookies.Append("EditMode", "View", new CookieOptions { MaxAge = TimeSpan.FromDays(1) });
            if (mode == "Edit")
                Response.Cookies.Append("EditMode", "Edit", new CookieOptions { MaxAge = TimeSpan.FromDays(1) });

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
