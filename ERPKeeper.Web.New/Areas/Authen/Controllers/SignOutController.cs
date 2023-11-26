using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeper.Web.New.Areas.Authen.Controllers
{
    [AllowAnonymous]
    public class SignOutController : BaseController
    {
        public IActionResult Index() => RedirectToAction("Process");

        public async Task<IActionResult> Process()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [Route("/signout")]
        [Route("/authen/signout")]
        public async Task<IActionResult> ProcessSignout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}