using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Setup
{
    public class SetupAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Initial";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              name: "Initial_default",
              url: "Initial/{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
             );
        }
    }
}