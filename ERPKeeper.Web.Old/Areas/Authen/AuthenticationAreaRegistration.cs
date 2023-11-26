using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Authen
{
    public class AuthenAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Authen";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Authen_default",
                "Authen/{controller}/{action}/{id}",
                  defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );

            context.MapRoute(
               "signin-facebook",
               "signin-facebook",
                 defaults: new { controller = "Oauth", action = "FacebookCallBack" }
           );
        }
    }
}