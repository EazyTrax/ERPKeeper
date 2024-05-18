using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;


namespace ERPKeeperCore.Web.Areas.My.Controllers
{
    [Route("/My/{Controller}/{Action}")]
    public class LocalizeController : _MyBaseController
    {
        public IActionResult Update(string culture)
        {
            //var coreRepo = new EazyTrax.Core.DataAccess.CoreRepository();
            //var coreUser = coreRepo.Users.Find(AuthorizeUserId);

            //coreUser.Lanugage = culture;
            //coreRepo.SaveChanges();

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}