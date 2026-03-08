using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace ERPKeeperCore.Web.Areas.Setting.Controllers
{
    public class LocalizeController : _SettingBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Update(string culture)
        {
            var profile = _dbContext.Profiles.Find(AuthorizeUserId);
            if (profile != null)
            {
                // You could save user preference to database if needed
                // member.Language = culture;
                // _dbContext.SaveChanges();
            }

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}