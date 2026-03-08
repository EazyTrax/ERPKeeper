using ERPKeeperCore.Enterprise.Models.Profiles;
using ERPKeeperCore.Enterprise.Models.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Authen.Controllers
{

    public class SigninController : AuthenArea_BaseController
    {

        private readonly ILogger<SigninController> _logger;

        public SigninController(ILogger<SigninController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new LogInModel());
        }

        [HttpPost]
        public async Task<IActionResult> Authen(LogInModel logInModel)
        {
            _logger.LogInformation("=== DEBUG LOGIN ===");
            _logger.LogInformation("Email: {Email}", logInModel.Email);
            _logger.LogInformation("Password: {Password}", logInModel.Password);
            _logger.LogInformation("===================");

            if (User.Identity.IsAuthenticated)
                return Redirect("/");

            var profile = organizationRepo.Profiles.Authen(logInModel);

            organizationRepo.SaveChanges();

            if (profile != null)
            {
                _logger.LogInformation("Authentication SUCCESS - Profile found: {ProfileName} (ID: {ProfileId})", profile.Name, profile.Id);
                return await this.DoSignIn(profile);
            }
            else
            {
                _logger.LogWarning("Authentication FAILED - No profile found for email: {Email}", logInModel.Email);
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
                new Claim(ClaimTypes.NameIdentifier,profile.Id.ToString()),
            };

            if (profile.IsAccountant)
                claims.Add(new Claim(ClaimTypes.Role, "Accountant"));
            if (profile.IsAdministrator)
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            if (profile.IsEmployee)
                claims.Add(new Claim(ClaimTypes.Role, "Employee"));


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