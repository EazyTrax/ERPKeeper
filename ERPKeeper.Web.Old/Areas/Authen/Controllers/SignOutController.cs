using ERPKeeper.WebFrontEnd.Controllers;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Authen.Controllers
{
    public class SignOutController : _DefaultNodeController
    {
        [AllowAnonymous]
        public ActionResult Home()
        {
            Session.Clear();
            Session.Abandon();

            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();

            return Redirect("/");
        }
    }
}