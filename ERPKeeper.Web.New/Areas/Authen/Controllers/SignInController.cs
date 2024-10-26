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
using ERPKeeperCore.Enterprise.Models.Security;
using Microsoft.EntityFrameworkCore;

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
            string[] Enterprises = new string[] {
               "tec",
                "bit"
            };

            foreach (var enterpriseDB in Enterprises)
            {
                var newOrganization = new ERPKeeperCore.Enterprise.EnterpriseRepo(enterpriseDB, true);
                newOrganization.ErpCOREDBContext.Database.Migrate();
            }

            return View(new LogInModel());
        }

        [HttpPost]
        public async Task<IActionResult> Authen(LogInModel logInModel)
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/Portal");

            var member = organizationRepo.Members.Authen(logInModel);

            if (member != null)
                return await this.DoSignIn(member);
            else
            {
                logInModel.ErrorMessage = "Error user not found";
                return View("Index", logInModel);
            }
        }

        private async Task<IActionResult> DoSignIn(Member member)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,  member.Name),
                new Claim(ClaimTypes.Email, member.Email),
                new Claim(ClaimTypes.NameIdentifier,member.Id.ToString())
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