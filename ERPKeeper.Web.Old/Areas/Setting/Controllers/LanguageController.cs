using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Setting.Controllers
{
    public class LanguageController : WebFrontEnd.Controllers._DefaultNodeController
    {

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        [AllowAnonymous]
        public ActionResult Change(string id)
        {
            HttpCookie myCookie = new HttpCookie("lang")
            {
                Value = id,
                Expires = DateTime.Now.AddDays(100)
            };
            Response.Cookies.Add(myCookie);


            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


    }
}