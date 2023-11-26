using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Profiles
{
    public class ProfilesAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Profiles";

        public override void RegisterArea(AreaRegistrationContext context)
        {


            context.MapRoute(
                "Profiles_default",
                url: "Profiles/{controller}/{action}/{id}",
               defaults: new { controller = "DashBoard", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}