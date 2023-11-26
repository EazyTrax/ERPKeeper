using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.HelpCenter
{
    public class HelpCenterAreaRegistration : AreaRegistration
    {
        public override string AreaName => "HelpCenter";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpCenter_default",
                "HelpCenter/{controller}/{action}/{id}",
                new { action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}