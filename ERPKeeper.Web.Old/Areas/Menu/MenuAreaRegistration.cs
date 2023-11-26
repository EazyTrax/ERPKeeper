using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Menu
{
    public class MenuAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Menu";


        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Menu_default",
                "Menu/{controller}/{action}/{id}",
                new { action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}