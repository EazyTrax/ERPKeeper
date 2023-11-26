using ERPKeeper.Node.Models.Profiles;
using ERPKeeper.Node.Models.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Authen.Controllers
{


    [AllowAnonymous]
    public class SignInController : WebFrontEnd.Controllers._DefaultNodeController
    {
        public ActionResult Home(string ReturnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
            {
                Session.Clear();

                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignOut();
                return Redirect("/");
            }


            var newLogInModel = new LogInModel()
            {
                ReturnUrl = ReturnUrl,
                AppId = Guid.NewGuid().ToString()
            };

            return View(newLogInModel);
        }


        public ActionResult UrlAuthen(Guid userId, string password)
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/Portal");
            var profile = Organization.Profiles.Find(userId);

            if (profile != null)
                return DoSignIn(profile);
            else
                return Content("ERROR !!");
        }

        [HttpPost]
        public ActionResult Authen(LogInModel loginModel)
        {
            var profile = Organization.Profiles.Authen(loginModel);

            if (profile != null)
                return DoSignIn(profile);

            return View("Home", loginModel);
        }


        [AllowAnonymous]
        public ActionResult DoSignIn(Profile profile)
        {
           // loginMember.LastLogin = DateTime.Now;
            Organization.SaveChanges();


            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, profile.Name),
                    new Claim(ClaimTypes.NameIdentifier,profile.Uid.ToString("D")),
                    new Claim(ClaimTypes.Email,profile.Email.ToLower()),
                    new Claim("AccessLevel", profile.AccessLevel.ToString())

            }, DefaultAuthenticationTypes.ApplicationCookie);

            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            var properties = new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddHours(8) };
            authManager.SignIn(properties, identity);

            //Set Localize
            HttpCookie myCookie = new HttpCookie("lang")
            {
                Value = profile.localizedLanguage.ToString(),
                Expires = DateTime.Now.AddDays(100)
            };

            Response.Cookies.Add(myCookie);



            return Redirect("/");
        }
    }
}