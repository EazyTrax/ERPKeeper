using ERPKeeperCore.Enterprise.Models.Profiles;
using ERPKeeperCore.Enterprise.Models.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Authen.Controllers
{

    public class SigninController : BaseController
    {
        public SigninController()
        {
            organizationRepo = new ERPKeeperCore.Enterprise.EnterpriseRepo();
        }

        public IActionResult Index()
        {

            return View(new LogInModel());
        }

        [HttpPost]
        public async Task<IActionResult> Authen(LogInModel logInModel)
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

            var profile = organizationRepo.Profiles.Authen(logInModel);
            organizationRepo.SaveChanges();

            if (profile != null)
                return await this.DoSignIn(profile);
            else
            {
                logInModel.ErrorMessage = "Error user not found";
                return View("Index", logInModel);
            }
        }

        private async Task<IActionResult> DoSignIn(ERPKeeperCore.Enterprise.Models.Profiles.Profile profile)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,  profile.Name),
                new Claim(ClaimTypes.Email, profile.Email),
                new Claim(ClaimTypes.NameIdentifier,profile.Id.ToString())
            };



            var modelIdentity = new ClaimsIdentity(claims, "Login");

            ClaimsPrincipal principal = new(modelIdentity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30) // Explicit 30-day expiration for persistent login
            };

            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    authProperties);

            return Redirect("/");
        }
    }
}