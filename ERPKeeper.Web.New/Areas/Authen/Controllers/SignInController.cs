using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using ERPKeeper.Node.Models.Security;

namespace ERPKeeper.Web.New.Areas.Authen.Controllers
{

    public class SigninController : BaseController
    {
        public SigninController()
        {
            organizationRepo = new Node.DAL.Organization();
        }

        public IActionResult Index() => View(new LogInModel());


        public async Task<IActionResult> UrlAuthen(Guid userId, string password)
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/Portal");

            var profile = organizationRepo.Profiles.Find(userId);

            if (profile != null)
                return await this.DoSignIn(profile);
            else
                return Content("ERROR !!");
        }


        [HttpPost]
        public async Task<IActionResult> Authen(LogInModel logInModel)
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/Portal");

            var profile = organizationRepo.Profiles.Authen(logInModel);

            if (profile != null)
                return await this.DoSignIn(profile);
            else
            {
                logInModel.ErrorMessage = "Error user not found";
                return View("Index", logInModel);
            }
        }

        private async Task<IActionResult> DoSignIn(Node.Models.Profiles.Profile profile)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,  profile.Name),
                new Claim(ClaimTypes.Email, profile.Email),
                new Claim(ClaimTypes.NameIdentifier,profile.Uid.ToString())
            };
            var modelIdentity = new ClaimsIdentity(claims, "Login");

            ClaimsPrincipal principal = new(modelIdentity);
            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties { IsPersistent = true });

            return Redirect("/Portal");
        }
    }
}