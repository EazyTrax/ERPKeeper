using ERPKeeper.WebFrontEnd.Authorize;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Authen.Controllers
{

    [AllowAnonymous]
    [ERPAuthorize(ERPKeeper.Node.Models.Security.Enums.AccessLevel.None)]
    public class UnAuthorizeController : WebFrontEnd.Controllers._DefaultNodeController
    {
        [AllowAnonymous]
        public ActionResult AccessLevel()
        {
            return Content("Access Level Error, Contact Admin.");
        }
    }
}